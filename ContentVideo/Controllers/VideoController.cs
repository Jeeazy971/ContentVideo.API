﻿using ContentVideo.Models.Domain;
using ContentVideo.Models.Dtos;
using ContentVideo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContentVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;

        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpPost]
        public async Task<ActionResult<VideoDTO>> CreateVideo(CreateVideoDTO createVideoDTO)
        {
            var video = new Video
            {
                Id = createVideoDTO.Id,
                Title = createVideoDTO.Title,
                ShortDescription = createVideoDTO.ShortDescription,
                LongDescription = createVideoDTO.LongDescription,
            };

            var createdVideo = await _videoRepository.CreateVideo(video);

            var videoDTO = new VideoDTO
            {
                Title = createdVideo.Title,
                ShortDescription = createdVideo.ShortDescription,
                LongDescription = createdVideo.LongDescription,
                Tags = createdVideo.Tags.Select(t => t.Title).ToList()
            };

            return CreatedAtAction(nameof(GetVideoById), new { id = createdVideo.Id }, videoDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(Guid id)
        {
            var video = await _videoRepository.GetVideoById(id);

            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetAllVideos()
        {
            var videos = await _videoRepository.GetAllVideos();

            return Ok(videos);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateVideo(Guid id, CreateVideoDTO videoUpdateDTO)
        {
            var existingVideo = await _videoRepository.GetVideoById(id);
            if (existingVideo == null)
            {
                return NotFound($"Aucune vidéo trouvée avec l'ID {id}.");
            }

            existingVideo.Title = videoUpdateDTO.Title;
            existingVideo.ShortDescription = videoUpdateDTO.ShortDescription;
            existingVideo.LongDescription = videoUpdateDTO.LongDescription;

            await _videoRepository.UpdateVideo(id, existingVideo);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteVideo(Guid id)
        {
            var success = await _videoRepository.DeleteVideo(id);
            if (!success)
            {
                return NotFound($"Aucune vidéo trouvée avec l'ID {id}.");
            }

            return NoContent();
        }

        [HttpGet("ByTag/{tagTitle}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosByTagTitle(string tagTitle)
        {
            var videos = await _videoRepository.GetVideosByTagTitle(tagTitle);

            return Ok(videos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> SearchVideosByTitleOrShortDescription(string searchText)
        {
            var videos = await _videoRepository.SearchVideosByTitleOrShortDescription(searchText);

            return Ok(videos);
        }


    }
}
