﻿using AutoMapper;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        
        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginVm);
            AuthenticationResponse authenticationResponse = await _accountService.AuthenticateAsync(loginRequest);
            return authenticationResponse;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel userVm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(userVm);
            return await _accountService.RegisterAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordVm, string origin)
        {
            ForgotPasswordRequest forgotPasswordRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordVm);
            return await _accountService.ForgotPasswordAsync(forgotPasswordRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordVm, string origin)
        {
            ResetPasswordRequest resetPasswordRequest = _mapper.Map<ResetPasswordRequest>(resetPasswordVm);
            return await _accountService.ResetPasswordAsync(resetPasswordRequest, origin);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SingOutAsync();
        }
    }
}
