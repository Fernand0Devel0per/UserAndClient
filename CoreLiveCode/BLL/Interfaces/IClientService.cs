using CoreLiveCode.Dtos.Client;

namespace CoreLiveCode.BLL.Interfaces
{
    public interface IClientService
    {
        Task<ClientCreateResponse> CreateAsync(ClientCreateRequest request);
        Task<ClientSearchResponse> GetByIdAsync(Guid id);
    }
}
