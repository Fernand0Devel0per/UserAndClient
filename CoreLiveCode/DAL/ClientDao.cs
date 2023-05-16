using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Helpers.Data;
using System.Collections;
using System.Data.SqlClient;

namespace CoreLiveCode.DAL
{
    public class ClientDao : IClientDao
    {

        private const string ColumnId = "Id";
        private const string ColumnName = "Name";
        private const string ColumnEmail = "Email";
        private const string ColumnAtCreated = "AtCreated";
        private const string ColumnPhoneNumber = "PhoneNumber";
        private const string ColumnUserId = "UserId";

        private readonly string _stringConnection;

        public ClientDao(IConfiguration configuration)
        {
            _stringConnection = configuration.GetDefaultConnectionString();
        }

        public async Task<Client> CreatAsync(Client client)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = @"INSERT INTO Clients (Id, Name, UserId, Email, PhoneNumber, AtCreated) OUTPUT INSERTED.ID 
                                  VALUES (NEWID(), @name, @userId, @email, @phoneNumber, @atCreated)";
            command.Parameters.AddWithValue("@name", client.Name);
            command.Parameters.AddWithValue("@userId", client.UserId);
            command.Parameters.AddWithValue("@email", client.Email);
            command.Parameters.AddWithValue("@phoneNumber", client.PhoneNumber);
            command.Parameters.AddWithValue("@atCreated", DateTime.UtcNow);

            client.AtCreated = DateTime.UtcNow;
            client.Id = (Guid)await command.ExecuteScalarAsync();

            return client;
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Name, Email, UserId, PhoneNumber, AtCreated
                    FROM Clients 
                    WHERE Id = @id;";

            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var client = new Client
                {
                    Id = reader.GetGuid(reader.GetOrdinal(ColumnId)),
                    Name = reader.GetString(reader.GetOrdinal(ColumnName)),
                    Email = reader.GetString(reader.GetOrdinal(ColumnEmail)),
                    PhoneNumber = reader.GetString(reader.GetOrdinal(ColumnPhoneNumber)),
                    AtCreated = reader.GetDateTime(reader.GetOrdinal(ColumnAtCreated)),
                    UserId = reader.GetGuid(reader.GetOrdinal(ColumnUserId)),

                };
                return client;
            }

            return null;
        }

        public async Task<IEnumerable<Client>> GetByUserIdAsync(Guid userId)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Id, Name, Email, UserId, PhoneNumber, AtCreated
                FROM Clients 
                WHERE UserId = @userId;";

            command.Parameters.AddWithValue("@userId", userId);

            var clients = new List<Client>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var client = new Client
                {
                    Id = reader.GetGuid(reader.GetOrdinal(ColumnId)),
                    Name = reader.GetString(reader.GetOrdinal(ColumnName)),
                    Email = reader.GetString(reader.GetOrdinal(ColumnEmail)),
                    PhoneNumber = reader.GetString(reader.GetOrdinal(ColumnPhoneNumber)),
                    AtCreated = reader.GetDateTime(reader.GetOrdinal(ColumnAtCreated)),
                    UserId = reader.GetGuid(reader.GetOrdinal(ColumnUserId)),
                };

                clients.Add(client);
            }

            return clients;
        }

        public async Task<bool> UpdateAsync(Client client)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = @"
                UPDATE Clients
                SET Name = @name, Email = @email, PhoneNumber = @phoneNumber, UserId = @userId
                WHERE Id = @id;";

            command.Parameters.AddWithValue("@id", client.Id);
            command.Parameters.AddWithValue("@name", client.Name);
            command.Parameters.AddWithValue("@email", client.Email);
            command.Parameters.AddWithValue("@phoneNumber", client.PhoneNumber);
            command.Parameters.AddWithValue("@userId", client.UserId);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Clients WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
