using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Helpers.Data;
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
    }
}
