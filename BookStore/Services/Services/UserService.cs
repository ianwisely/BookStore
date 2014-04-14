using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Messages.User.Request;
using BookStore.Services.Messages.User.Response;
using System;

namespace BookStore.Services.Services
{
    public class UserService : ServiceBase
    {
        private UserRepository _userRepository;
        private static UserService _userService = new UserService();

        public UserService() {}

        public new static ServiceBase GetInstance()
        {
            return _userService;
        }

        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            _userRepository = (UserRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.UserRepository);
            //_userRepository = new UserRepository();
            try
            {
                User user = _userRepository.GetUser(request.Email);
                if ((user != null) && (user.Email.ToLower() == request.Email.ToLower() && user.Password == request.Password))
                {
                    response.LoginViewModel.Email = user.Email;
                    response.UserId = user.Id;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                }
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _userRepository.Dispose();
            }
            return response;
        }

        public UpdateUserResponse Register(CreateUpdateUserRequest request)
        {
            UpdateUserResponse response = new UpdateUserResponse();
            _userRepository = (UserRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.UserRepository);
            try
            {
                User user = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    ContactNumber = request.PhoneNumber,
                    LoyaltyCardPoints = 0,
                };
                _userRepository.InsertUser(user);
                //send email
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _userRepository.Dispose();
            }
            return response;
        }

        public UpdateUserResponse UpdateUser(CreateUpdateUserRequest request)
        {
            UpdateUserResponse response = new UpdateUserResponse();
            _userRepository = (UserRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.UserRepository);
            try
            {
                var user = _userRepository.GetUser(request.Id);
                _userRepository.UpdateUser(user);
                response.UpdateUserViewModel = request.UpdateUserViewModel;
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _userRepository.Dispose();
            }
            return response;
        }
    }
}