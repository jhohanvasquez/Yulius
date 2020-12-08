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

namespace Yulius.Api.Controllers
{
    /// <summary>
    /// Login and register service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]    
    public class UserController : ControllerBase
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
        public UserController
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

        /// <summary>
        /// This method is used to register end user
        /// </summary>
        /// <param name="userForRegisterDTO">This parameter must be filled to register</param>
        /// <returns>This API method return 200 for each case. Developer should check the ResultCode in Result object</returns>
        [Authorize("Bearer")]
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

                result = await _repo.Register(user, userForRegisterDTO.Password);

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
