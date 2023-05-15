using CoreLiveCode.Domain;

namespace CoreLiveCode.DAL.Interfaces
{
    public interface IUserDao
    {
        Task<User> CreatAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
