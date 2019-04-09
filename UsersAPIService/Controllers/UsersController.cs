


namespace UsersAPIService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccesLayer.Models;
    using UsersAPIService.Helpers;
    using Microsoft.Extensions.Options;
    using UsersAPIService.Dto;
    using Microsoft.AspNetCore.Authorization;
    using DataAccesLayer.Repository.Intefaces;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        #region Fields

        private readonly IUserRepository _userBusinessService;
        // private readonly ILogger _logger;

        private IMapper _mapper;

        private readonly AppSettings _appSettings;

        #endregion
        public UsersController(IUserRepository userBusinessService, IMapper mapper,
            IOptions<AppSettings> appSettings/*, ILogger logger*/)
        {
            _userBusinessService = userBusinessService;
            //_logger = logger;

            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #region Test Mx

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userBusinessService.Authenticate(userDto.Username, userDto.UserPassword);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            });
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<Users>(userDto);

            try
            {
                // save 
                _userBusinessService.Create(user, userDto.UserPassword);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userBusinessService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        #endregion


        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userBusinessService.GetAllUsersAsync());
            }
            catch (Exception ex)
            {
                //_logger.LogError("error in UsersController -> GetAllUsers: ", ex);
                throw;
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                return Ok(await _userBusinessService.GetByIdAsync(userId));
            }
            catch (Exception ex)
            {
                //_logger.LogError("error in UsersController -> GetUserById: ", ex);
                throw;
            }
        }

        [HttpPut("add")]
        public void InsertUser(Users user)
        {
            try
            {
                _userBusinessService.Insert(user);
            }
            catch (Exception ex)
            {
                //Logger...
                throw;
            }
        }

        [HttpPut("update")]
        public void UpdateUser(Users user)
        {
            try
            {
                _userBusinessService.Update(user);
            }
            catch (Exception ex)
            {
                //Logger...
                throw;
            }
        }

    }
}
