﻿using AutoMapper;
using UserAndClient.BLL.Interfaces;
using UserAndClient.DAL.Interfaces;
using UserAndClient.Domain;
using UserAndClient.Dtos.Client;
using UserAndClient.Dtos.User;
using UserAndClient.Helpers.Exceptions;

namespace UserAndClient.BLL
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
            var user = await _userDao.GetByIdAsync(client.UserId);
            if (user == null) 
            {
                throw new UserNotFoundException($"The user id {request.UserId} not found.");
            }
            var userReturn = _mapper.Map<UserClientResponse>(user);
            var newClient = await _clientDao.CreatAsync(client);
            var clientReturn = _mapper.Map<ClientCreateResponse>(newClient);
            clientReturn.User = userReturn;
            return clientReturn;
        }

        public async Task<ClientSearchResponse> GetByIdAsync(Guid id)
        {
            var client = await _clientDao.GetByIdAsync(id);
            if (client is null)
            {
                throw new ClientNotFoundException($"The client id {id} not found.");
            }

            var user = await _userDao.GetByIdAsync(client.UserId);
            var userReturn = _mapper.Map<UserClientResponse>(user);
            var clientReturn = _mapper.Map<ClientSearchResponse>(client);
            clientReturn.User = userReturn;
            return clientReturn;
        }

        public async Task<IEnumerable<ClientSearchResponse>> GetByUserIdAsync(Guid userId)
        {
            var clients = await _clientDao.GetByUserIdAsync(userId);
            if (clients == null || !clients.Any())
            {
                throw new ClientNotFoundException($"No clients found for user id {userId}.");
            }

            var clientResponses = new List<ClientSearchResponse>();
            var user = await _userDao.GetByIdAsync(userId);
            var userReturn = _mapper.Map<UserClientResponse>(user);

            foreach (var client in clients)
            {
                
                var clientReturn = _mapper.Map<ClientSearchResponse>(client);
                clientReturn.User = userReturn;

                clientResponses.Add(clientReturn);
            }

            return clientResponses;
        }

        public async Task<bool> UpdateAsync(Guid id, ClientUpdateRequest request)
        {
            var existingClient = await _clientDao.GetByIdAsync(id);
            if (existingClient is null)
            {
                throw new ClientNotFoundException($"The client id {id} not found.");
            }

            var existingUser = await _userDao.GetByIdAsync(request.UserId);
            if (existingUser is null)
            {
                throw new UserNotFoundException($"The user id {request.UserId} not found.");
            }

            var clientToUpdate = _mapper.Map<Client>(request);
            clientToUpdate.Id = id;

            return await _clientDao.UpdateAsync(clientToUpdate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingClient = await _clientDao.GetByIdAsync(id);
            if (existingClient is null)
            {
                throw new ClientNotFoundException($"The client id {id} not found.");
            }

            return await _clientDao.DeleteAsync(id);
        }
    }

}

