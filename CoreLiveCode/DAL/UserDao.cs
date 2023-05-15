﻿using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Helpers.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace CoreLiveCode.DAL
{
    public class UserDao : IUserDao
    {

        private const string ColumnId = "Id";
        private const string ColumnName = "Name";
        private const string ColumnEmail = "Email";

        private readonly string _stringConnection;

        public UserDao(IConfiguration configuration)
        {
            _stringConnection = configuration.GetDefaultConnectionString();
        }

        public async Task<User> CreatAsync(User user)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = @"INSERT INTO Users (Id, Name, Email) OUTPUT INSERTED.ID 
                                  VALUES (NEWGUID, @name, @email)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);

            user.Id = (Guid)await command.ExecuteScalarAsync();

            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Name, Email
                    FROM Users 
                    WHERE Id = @id;";

            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var user = new User
                {
                    Id = reader.GetGuid(reader.GetOrdinal(ColumnId)),
                    Name = reader.GetString(reader.GetOrdinal(ColumnName)),
                    Email = reader.GetString(reader.GetOrdinal(ColumnEmail)),

                };
                return user;
            }

            return null;
        }
    }
}