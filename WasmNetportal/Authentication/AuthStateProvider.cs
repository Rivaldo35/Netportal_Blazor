﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Netportal.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WasmNetportal.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly IAPIHelper _apihelper;
        private readonly AuthenticationState _anonymous;


        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage, IConfiguration config, IAPIHelper apihelper)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _config = config;
            _apihelper = apihelper;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authTokenStorageKey = _config["authTokenStorageKey"];
            var token = await _localStorage.GetItemAsync<string>(authTokenStorageKey);
            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }

            bool isAuthenticated = await NotifyUserAuthentication(token);

            if (isAuthenticated == false)
            {
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(
                new ClaimsPrincipal
                (new ClaimsIdentity(
                    JwtParser.ParseClaimsFromJWT(token),
                    "jwtAuthType")));
        }
        public async Task<bool> NotifyUserAuthentication(string token)
        {
            bool isAuthenticatedOutput;
            Task<AuthenticationState> authState;
            try
            {
                await _apihelper.GetLoggedInUserInfo(token);
                var authenticatedUser = new ClaimsPrincipal
                                       (new ClaimsIdentity(
                                 JwtParser.ParseClaimsFromJWT(token),
                                       "jwtAuthType"));
                authState = Task.FromResult(new AuthenticationState(authenticatedUser));
                NotifyAuthenticationStateChanged(authState);
                isAuthenticatedOutput = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await NotifyUserLogout();
                isAuthenticatedOutput = false;
                // authState = Task.FromResult(_anonymous);
            }
            return isAuthenticatedOutput;
        }

        public async Task NotifyUserLogout()
        {
            string authTokenStorageKey = _config["authTokenStorageKey"];
            await _localStorage.RemoveItemAsync(authTokenStorageKey);
            var authState = Task.FromResult(_anonymous);
            _apihelper.LoggOffUser();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
