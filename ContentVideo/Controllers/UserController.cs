using ContentVideo.Models.Domain;
using ContentVideo.Models.Dtos;
using ContentVideo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContentVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository; 

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var role = await _roleRepository.GetRoleByTitle(createUserDTO.RoleTitle);
                if (role == null)
                {
                    return BadRequest("Le rôle spécifié n'existe pas.");
                }

                // Création de l'entité User
                var user = new User
                {
                    Username = createUserDTO.Username,
                    Password = createUserDTO.Password, 
                    RoleId = role.Id
                };

                var createdUser = await _userRepository.CreateUser(user);

                // Conversion en UserDTO pour la réponse, si nécessaire
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

            var userDTOs = users.Select(user => new UserDTO
            {
                Username = user.Username,
                RoleTitle = user.Role.Title
            });

            return Ok(userDTOs);
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

                var userDTO = new UserDTO
                {
                    Username = user.Username,
                    RoleTitle = user.Role.Title
                };

                return Ok(userDTO);
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
            var userDTOs = users.Select(user => new UserDTO
            {
                Username = user.Username,
                RoleTitle = user.Role.Title
            }).ToList();

            return Ok(userDTOs);
        }



    }
}
