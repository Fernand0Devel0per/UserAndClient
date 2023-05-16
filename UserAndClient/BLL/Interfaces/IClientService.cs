using UserAndClient.Dtos.Client;

namespace UserAndClient.BLL.Interfaces
{
    public interface IClientService
    {
        Task<ClientCreateResponse> CreateAsync(ClientCreateRequest request);
        Task<ClientSearchResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<ClientSearchResponse>> GetByUserIdAsync(Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, ClientUpdateRequest request);
    }
}
