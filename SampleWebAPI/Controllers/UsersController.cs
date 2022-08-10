using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;
using SampleWebAPI.Services;
using WebApi.Models.Users;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        //private readonly IMapper _mapper;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        //[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        //[AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {

            try
            {
                _userService.Register(model);
                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //_userService.Register(model);
            
        }

        //[Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }



    }
}
