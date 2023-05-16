using AutoMapper;
using UserAndClient.BLL.Interfaces;
using UserAndClient.DAL.Interfaces;
using UserAndClient.Domain;
using UserAndClient.Dtos.User;
using UserAndClient.Helpers.Exceptions;

namespace UserAndClient.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly IMapper _mapper;

        public UserService(IUserDao userDao, IMapper mapper)
        {
            _userDao = userDao;
            _mapper = mapper;
        }

        public async Task<UserCreateResponse> CreateAsync(UserCreateRequest request)
        {
            var user = _mapper.Map<User>(request);
            var newUser = await _userDao.CreatAsync(user);
            var userReturn = _mapper.Map<UserCreateResponse>(newUser);
            return userReturn;
        }

        public async Task<UserSearchResponse> GetByIdAsync(Guid id)
        {
            var user = await _userDao.GetByIdAsync(id);
            if (user is null) 
            {
                throw new UserNotFoundException($"The user id {id} not found.");
            }

            var userReturn = _mapper.Map<UserSearchResponse>(user);
            return userReturn;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingUser = await _userDao.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new UserNotFoundException($"The user id {id} not found.");
            }

            return await _userDao.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, UserUpdateRequest request)
        {
            var existingUser = await _userDao.GetByIdAsync(id);
            if (existingUser is null)
            {
                throw new UserNotFoundException($"The user id {id} not found.");
            }

            var userToUpdate = _mapper.Map<User>(request);
            userToUpdate.Id = id;

            return await _userDao.UpdateAsync(userToUpdate);
        }

    }
}
