﻿using backend.Configurations;
using backend.Core.Dtos.User;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email already exists in the database
            var user_already_exists = await _userManager.FindByEmailAsync(requestDto.Email);

            if(user_already_exists != null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exists"
                    }
                });
            }

            // Create new user
            var new_user = new IdentityUser()
            {
                Email = requestDto.Email,
                UserName = requestDto.Username
            };

            var user_created = await _userManager.CreateAsync(new_user, requestDto.Password);

            if(user_created.Succeeded)
            {
                // Generate JWT
                var token = GenerateJWT(new_user);
                return Ok(new AuthResult()
                {
                    Token = token,
                    Result = true
                });
            }

            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                    {
                        "Server error"
                    }
            });
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }

            // Check if the user exists
            var existing_user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if(existing_user == null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }

            var user_credentials_correct = await _userManager.CheckPasswordAsync(existing_user, loginRequestDto.Password);

            if(!user_credentials_correct)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Invalid credentials"
                    }
                });
            }

            var JWT = GenerateJWT(existing_user);
            return Ok(new AuthResult()
            {
                Token = JWT,
                Result = true
            });
        }

        private string GenerateJWT(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Create the token descriptor 
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
