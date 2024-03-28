using Microsoft.AspNetCore.Mvc;
using ContentVideo.Models.Domain;
using ContentVideo.Repositories;
using ContentVideo.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ContentVideo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> CreateRole(CreateRoleDTO createRoleDTO)
        {
            var role = new Role
            {
                Title = createRoleDTO.Title,
                Description = createRoleDTO.Description
            };

            var createdRole = await _roleRepository.CreateRole(role);

            var roleDTO = new RoleDTO
            {
                Title = createdRole.Title,
                Description = createdRole.Description
            };

            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, roleDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<RoleDTO>> GetRoleById(Guid id)
        {
            var role = await _roleRepository.GetRoleById(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();


            return Ok(roles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, CreateRoleDTO roleUpdateDTO)
        {
            var roleToUpdate = await _roleRepository.GetRoleById(id);
            if (roleToUpdate == null)
            {
                return NotFound();
            }

            roleToUpdate.Title = roleUpdateDTO.Title;
            roleToUpdate.Description = roleUpdateDTO.Description;

            await _roleRepository.UpdateRole(id, roleToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var success = await _roleRepository.DeleteRole(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
