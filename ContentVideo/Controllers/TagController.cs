using ContentVideo.Models.Domain;
using ContentVideo.Models.Dtos;
using ContentVideo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContentVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpPost]
        public async Task<ActionResult<TagDTO>> CreateTag(CreateTagDTO createTagDTO)
        {

            var tag = new Tag
            {
                Id = createTagDTO.Id,
                Title = createTagDTO.Title
            };

            var createdTag = await _tagRepository.CreateTag(tag);

            var tagDTO = new TagDTO
            {
                Title = createdTag.Title
            };

           
            return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, tagDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTagById(Guid id)
        {
            var tag = await _tagRepository.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            var tagDTO = new TagDTO
            {
                Title = tag.Title
            };

            return Ok(tagDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();

            var tagDTOs = tags.Select(tag => new TagDTO
            {
                Title = tag.Title,
                
            }).ToList();

            return Ok(tagDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, CreateTagDTO tagUpdateDTO)
        {
            var existingTag = await _tagRepository.GetTagById(id);
            if (existingTag == null)
            {
                return NotFound($"Aucun tag trouvé avec l'ID {id}.");
            }

            existingTag.Title = tagUpdateDTO.Title;
            await _tagRepository.UpdateTag(id, existingTag);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var success = await _tagRepository.DeleteTag(id);
            if (!success)
            {
                return NotFound($"Aucun tag trouvé avec l'ID {id}.");
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> SearchTagsByTitle(string title)
        {
            var tags = await _tagRepository.SearchTagsByTitle(title);
            var tagDTOs = tags.Select(tag => new TagDTO { Id = tag.Id, Title = tag.Title }).ToList();

            return Ok(tagDTOs);
        }

    }
}
