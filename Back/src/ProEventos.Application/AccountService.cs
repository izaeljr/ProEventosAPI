using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Contracts;
using ProEventos.Application.DTOs;
using ProEventos.Domain.Identity;
using AutoMapper;
using ProEventos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Application
{
    public  class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserPersist userPersist)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDto.Username.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Invalid password. Error: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error when creating account. Error: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string username)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(username);
                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);

                return userUpdateDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error when retrieving username. Error: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.Username);
                if (user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userReturn = await _userPersist.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userReturn);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error while updating account. Error: {ex.Message}");
            }
        }

        public async Task<bool> UserExist(string username)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error while verifying if user exists. Error: {ex.Message}");
            }
        }
    }
}