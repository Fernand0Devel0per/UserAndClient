using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Helpers.Data;
using CoreLiveCode.Helpers.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private const string ColumnAtCreated = "AtCreated";


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

            command.CommandText = @"INSERT INTO Users (Id, Name, Email, AtCreated) OUTPUT INSERTED.ID 
                                  VALUES (NEWID(), @name, @email, @atCreated)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@atCreated", DateTime.UtcNow);

            user.AtCreated = DateTime.UtcNow;
            user.Id = (Guid)await command.ExecuteScalarAsync();

            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Name, Email, AtCreated
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
                    AtCreated = reader.GetDateTime(reader.GetOrdinal(ColumnAtCreated)),
                };
                return user;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = new SqlConnection(_stringConnection);
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Users WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new UserInUseException("Unable to delete. The user is associated with one or more clients.");
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = @"
                UPDATE Users
                SET Name = @name, Email = @email
                WHERE Id = @id;";

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);



            return await command.ExecuteNonQueryAsync() > 0; ;
        }
    }
}
