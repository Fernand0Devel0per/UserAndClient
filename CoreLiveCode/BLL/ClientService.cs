using AutoMapper;
using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Domain;
using CoreLiveCode.Dtos.Client;
using CoreLiveCode.Dtos.User;
using CoreLiveCode.Helpers.Exceptions;

namespace CoreLiveCode.BLL
{
    public class ClientService : IClientService
    {
        private readonly IUserDao _userDao;
        private readonly IClientDao _clientDao;
        private readonly IMapper _mapper;

        public ClientService(IClientDao clientDao, IMapper mapper, IUserDao userDao)
        {
            _clientDao = clientDao;
            _mapper = mapper;
            _userDao = userDao;
        }

        public async Task<ClientCreateResponse> CreateAsync(ClientCreateRequest request)
        {
            var client = _mapper.Map<Client>(request);
            var user = await _userDao.GetUserByIdAsync(client.UserId);
            if (user == null) 
            {
                throw new UserNotFoundException($"The user id {request.UserId} not found.");
            }

            var newClient = await _clientDao.CreatAsync(client);
            var clientReturn = _mapper.Map<ClientCreateResponse>(newClient);
            return clientReturn;
        }

        public async Task<ClientSearchResponse> GetUserById(Guid id)
        {
            var client = _clientDao.GetClientByIdAsync(id);
            if (client is null)
            {
                throw new ClientNotFoundException($"The client id {id} not found.");
            }

            var clientReturn = _mapper.Map<ClientSearchResponse>(client);
            return clientReturn;
        }

    }
}
