using CoreLiveCode.Dtos.User;

namespace CoreLiveCode.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserCreateResponse> CreateAsync(UserCreateRequest request);
        Task<UserSearchResponse> GetByIdAsync(Guid id);
    }
}
