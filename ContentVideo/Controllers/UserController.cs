using ContentVideo.Models.Domain;
using ContentVideo.Models.Dtos;
using ContentVideo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContentVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var role = await _roleRepository.GetRoleByTitle(createUserDTO.RoleTitle);
                if (role == null)
                {
                    return BadRequest("Le rôle spécifié n'existe pas.");
                }

                var user = new User
                {
                    Username = createUserDTO.Username,
                    Password = createUserDTO.Password, 
                    RoleId = role.Id
                };

                var createdUser = await _userRepository.CreateUser(user);

                var userDTO = new UserDTO
                {
                    Username = createdUser.Username,
                    RoleTitle = role.Title
                };

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne est survenue : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            var role = await _roleRepository.GetRoleByTitle(updateUserDTO.RoleTitle);
            if (role == null)
            {
                return BadRequest("Le rôle spécifié n'existe pas.");
            }

            user.Username = updateUserDTO.Username;
            user.Password = updateUserDTO.Password;

            await _userRepository.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUsersByRoleId(id);
                if (user == null)
                {
                    return NotFound($"Aucun utilisateur trouvé avec l'ID {id}.");
                }

                await _userRepository.DeleteUser(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne est survenue : {ex.Message}");
            }
        }

        [HttpGet("searchByUsername")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> SearchByUsername(string username)
        {
            var users = await _userRepository.SearchUsersByUsername(username);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user == null)
                {
                    return NotFound("Utilisateur non trouvé.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne est survenue : {ex.Message}");

            }
        }

        [HttpGet("ByRole/{roleId}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByRoleId(Guid roleId)
        {
            var users = await _userRepository.GetUsersByRoleId(roleId);

            return Ok(users);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.Authenticate(loginDTO.Username, loginDTO.Password);
            if (user == null)
            {
                return Unauthorized("Informations d'identification non valides.");
            }

            // Générer un token JWT
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }
}
