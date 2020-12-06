﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yulius.Data.DTOs;
using Yulius.Data.Models;
using Yulius.Data.Data.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Yulius.Api.Controllers
{
    /// <summary>
    /// Login and register service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepo _repo;
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// This controller is used for authentication. It includes Login and Register methods
        /// </summary>
        /// <param name="repo">This is injected repo parameter with pre-parameterized in startup</param>
        /// <param name="config">This is injected config parameter</param>
        /// <param name="mapper">This is injected mapper parameter</param>
        public AuthController
         (
             IAuthRepo repo,
             IConfiguration config,
             IMapper mapper
         )
        {
            _repo = repo;
            Configuration = config;
            _mapper = mapper;
        }


        // http://localhost:5000/api/auth/login
        /// <summary>
        /// This method returns JWT so that it can be used in header of requests.         
        /// </summary>
        /// <param name="userForLoginDTO">This parameter should be filled by client and then posted to Login method</param>
        /// <returns>
        /// Although in the case of fail unauthroization could be returned, 200 is returned for all requests.
        /// Developer should check the ResultCode in the result object.
        /// If the ResultCode in Result object is positive it means operation is successfull.
        /// If the ResultCode in Result object is negative it means operation is unsuccessfull.
        /// In failure case developer can show the ResultMessage to the end user
        /// </returns>
        [HttpPost("Login")]        
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {            
            try
            {
                          
                //---------------------------- login ---
                Result result = new Result();

                result = await _repo.Login(userForLoginDTO.Email,
                                           userForLoginDTO.Password);

                //--------------------------- /login ---
           


                if (result.ResultCode < 0)
                {
                    //return Unauthorized();  //core 2.1

                    //return Unauthorized(      //core 2.2
                    //new ResultDTO
                    //{
                    //    resultCode = result.RESULT_CODE,
                    //    resultMessage = result.RESULT_MESSAGE
                    //});


                    return Ok(new ResultDTO
                    {                        
                        ResultCode = result.ResultCode.ToString(),
                        ResultMessage = result.ResultMessage
                    });
                    
                }
                else
                {                   

                    UserForListDTO userForListDTO = new UserForListDTO();
                    var resultUser = (User)result.obj;
                    userForListDTO.UserId = resultUser.UserId;
                    userForListDTO.FirstName = resultUser.FirstName;
                    userForListDTO.LastName = resultUser.LastName;
                    userForListDTO.PasswordHash = resultUser.PasswordHash;
                    userForListDTO.Email = resultUser.Email;
                    userForListDTO.CountryId = resultUser.CountryId;
                    userForListDTO.Role = resultUser.Role;
                    userForListDTO.City = resultUser.City;

                    string UserDatatoCreateToken = userForListDTO.UserId.ToString() + "," +
                                                   userForListDTO.Email.ToString();

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userForListDTO.UserId.ToString()),
                        new Claim(ClaimTypes.Name, userForListDTO.Email),
                        new Claim(ClaimTypes.UserData, UserDatatoCreateToken),
                        new Claim(ClaimTypes.Role, userForListDTO.Role)
                    };

                    var secretKey = Configuration.GetConnectionString("securityKey");
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddHours(1),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    //mapper could be used
                    //var user = _mapper.Map<UserForListDTO>(UserFromRepo);
                  

                    return Ok(new ResultDTO
                    {                        
                        ResultCode = result.ResultCode.ToString(),
                        ResultMessage = result.ResultMessage,
                        ResultJSONobj = new
                        {
                            token = tokenHandler.WriteToken(token),
                            userForListDTO
                        }
                    });


                }//else

            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);// StatusCode could be returned

                return Ok(new ResultDTO
                {
                    ResultCode = "-500",
                    ResultMessage = ex.Message
                });

            }

        }// Login


        /// <summary>
        /// This method is used to register end user
        /// </summary>
        /// <param name="userForRegisterDTO">This parameter must be filled to register</param>
        /// <returns>This API method return 200 for each case. Developer should check the ResultCode in Result object</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {

            try
            {
                Result result = new Result();

                User user = new User()
                {
                    UserId = 0,
                    Email  = userForRegisterDTO.Email,
                    PasswordHash = "",                    
                    FirstName = userForRegisterDTO.FirstName,
                    LastName = userForRegisterDTO.LastName,
                    City = userForRegisterDTO.City,
                    CountryId = userForRegisterDTO.CountryId,
                    Role = userForRegisterDTO.Role
                };

                result = await _repo.Register(user, userForRegisterDTO.Password );

                //--------------------------- /register ---



                if (result.ResultCode < 0)
                {                   
                    return Ok(new ResultDTO
                    {                        
                        ResultCode = result.ResultCode.ToString(),
                        ResultMessage = result.ResultMessage
                    });                    
                }
                else
                {
                    UserForListDTO userForListDTO = new UserForListDTO()
                    {
                        UserId = ((User)result.obj).UserId,
                        Email = ((User)result.obj).Email,
                        FirstName = ((User)result.obj).FirstName,
                        LastName = ((User)result.obj).LastName,
                        Role = ((User)result.obj).Role
                    };

                    return Ok(new ResultDTO
                    {
                        ResultCode = result.ResultCode.ToString(),
                        ResultMessage = result.ResultMessage,
                        ResultJSONobj = new
                        {                           
                            userForListDTO
                        }
                    });


                }//else

            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);

                return Ok(new ResultDTO
                {
                    ResultCode = "-500",
                    ResultMessage = ex.Message
                });

            }

        }// Register





        //// GET: api/Auth
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Auth/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Auth
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Auth/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


    }
}
