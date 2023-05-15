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

            command.CommandText = @"INSERT INTO Clients (Id, Name, UserId) OUTPUT INSERTED.ID 
                                  VALUES (NEWGUID, @name, @userId)";
            command.Parameters.AddWithValue("@name", client.Name);
            command.Parameters.AddWithValue("@userId", client.UserId);

            client.Id = (Guid)await command.ExecuteScalarAsync();

            return client;
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_stringConnection);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Name, UserId
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
                    UserId = reader.GetGuid(reader.GetOrdinal(ColumnUserId)),

                };
                return client;
            }

            return null;
        }
    }
}
