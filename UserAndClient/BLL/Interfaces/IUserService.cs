using UserAndClient.Dtos.User;

namespace UserAndClient.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserCreateResponse> CreateAsync(UserCreateRequest request);
        Task<UserSearchResponse> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UserUpdateRequest request);
    }
}
