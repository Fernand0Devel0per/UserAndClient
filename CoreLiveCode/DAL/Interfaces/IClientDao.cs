using CoreLiveCode.Domain;

namespace CoreLiveCode.DAL.Interfaces
{
    public interface IClientDao
    {
        Task<Client> CreatAsync(Client client);
        Task<Client> GetByIdAsync(Guid id);
        Task<IEnumerable<Client>> GetByUserIdAsync(Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Client client);
    }
}
