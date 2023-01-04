using AutoMapper;
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
        private readonly EmailService _emailService;

        public LoginController(UserService userService, TokenGeneratorService tokenGeneratorService, EmailService emailService)
        {
            _userService = userService;
            _tokenGeneratorService = tokenGeneratorService;
            _emailService = emailService;
        }


        [ValidationFilter]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            var user_Id = ObjectId.GenerateNewId().ToString();
            var newUser = await _userService.AddUser(user, user_Id);
            _emailService.CreateEmailVerificationToken(user_Id);
            return Ok(newUser);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginUser)
        {
            var user = _userService.GetUser(loginUser);
            user.Token = _tokenGeneratorService.GenerateToken();

            return Ok(user);
        }
        [HttpGet]
        [Route("activatemail/id={Id}&VerificationToken={EmailVerificationToken}")]
        public IActionResult ActivateEmail(string Id, string EmailVerificationToken)
        {
            var emailVerification = _emailService.GetEmailVerification(Id);
            if(emailVerification.EmailVerificationToken.AccessToken == EmailVerificationToken)
            {
                var user = _userService.GetUserById(Id);
                user.isActivatedEmail = true;
                _userService.UpdateUser(user);
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpGet("register/{id}")]
        public IActionResult GetEmailVerificationToken(string id)
        {
            var emailVerification = _emailService.GetEmailVerification(id);
            return Ok(emailVerification);
        }
    }
}
