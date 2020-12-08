using AutoMapper;
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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Yulius.Api.Controllers
{
    /// <summary>
    /// Login and register service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]   
    [EnableCors("AllowAll")]    
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


        // https://localhost:44358/api/auth/login
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
        [AllowAnonymous]
        [HttpPost("Login")]        
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {            
            try
            {
                          
                //---------------------------- login ---
                Result result = new Result();

                result = await _repo.Login(userForLoginDTO.Username,
                                           userForLoginDTO.Password);

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

                    var token = TokenGenerator.GenerateTokenJwt(userForLoginDTO.Username, userForLoginDTO.Password);
                    //mapper could be used
                    //var user = _mapper.Map<UserForListDTO>(UserFromRepo);
                    return base.Ok(ResultMethod(result, userForListDTO, token));

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

        private static ResultDTO ResultMethod(Result result, UserForListDTO userForListDTO, string token)
        {
            return new ResultDTO
            {
                ResultCode = result.ResultCode.ToString(),
                ResultMessage = result.ResultMessage,
                ResultJSONobj = new
                {
                    token,
                    userForListDTO
                }
            };
        }
    }
}
