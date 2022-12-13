﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Movie_API.Filter;
using Movie_API.Models;
using Movie_API.Models.DTOs;
using Movie_API.Services;
using System.Net.Http.Headers;

namespace Movie_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        private readonly UserService _userService;
        private readonly TokenGeneratorService _tokenGeneratorService;
        
        
        public LoginController(UserService userService, TokenGeneratorService tokenGeneratorService)
        {
            _userService = userService;
            _tokenGeneratorService = tokenGeneratorService;
            
        }


        [ValidationFilter]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO user)
        {
            var user_Id = ObjectId.GenerateNewId().ToString();
            await _userService.AddUser(user, user_Id);
            
            return StatusCode(201);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UserLoginDTO loginUser)
        {
            var user = _userService.GetUser(loginUser);
            user.Token = _tokenGeneratorService.GenerateToken();
            
            return Ok(user);
        }
    }
}