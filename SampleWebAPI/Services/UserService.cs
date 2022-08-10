using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleWebAPI.Data;
using SampleWebAPI.Domain;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;
using SampleWebAPI.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Users;
//using BCrypt.Net;

namespace SampleWebAPI.Services
{
    public interface IUserService 
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        private SamuraiContext _context;

        private readonly IMapper _mapper;

        public UserService(IOptions<AppSettings> appSettings, SamuraiContext context, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;
            //var response = _mapper.Map<AuthenticateResponse>(user);
            // authentication successful so generate jwt token
            //var token = generateJwtToken(user);

            //return new AuthenticateResponse(user, token);
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = generateJwtToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new Exception("Username '" + model.Username + "' is already taken");


            try
            {
                var user = _mapper.Map<User>(model);
                //user.PasswordHash = BCrypt.HashPassword(model.Password);
                _context.Users.Add(user);
                _context.SaveChangesAsync();
                // return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }


        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
