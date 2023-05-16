using AutoMapper;
using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Dtos.User;
using CoreLiveCode.Helpers.Exceptions;

namespace CoreLiveCode.BLL
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

    }
}
