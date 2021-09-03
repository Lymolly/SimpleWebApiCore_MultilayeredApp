using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMusic.Api.Models.ViewModels;
using MyMusic.Api.Validators;
using MyMusic.Core.Entities;
using MyMusic.Services.Interfaces;

namespace MyMusic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private IMapper _mapper;

        public MusicController(IMusicService service,IMapper mapper)
        {
            _musicService = service;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MusicViewModel>>> GetAllMusic()
        {
            var musics = await _musicService.GetAllWithArtist();
            var result = _mapper.Map<IEnumerable<MusicViewModel>>(musics);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<MusicViewModel>>> GetMusicById(int? id)
        {
            if (id.HasValue)
            {
                var music = await _musicService.GetMusicById(id.Value);
                var result = _mapper.Map<MusicViewModel>(music);
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("")]
        public async Task<ActionResult<MusicViewModel>> CreateMusic([FromBody]SaveMusicViewModel model)
        {
            var validator = new SaveMusicValidator();
            var validatorRes = await validator.ValidateAsync(model);
            if (!validatorRes.IsValid)
                return BadRequest(validatorRes.Errors);
            var musicToCreate = _mapper.Map<Music>(model);
            var newMusic = await _musicService.CreateMusic(musicToCreate);
            var music = await _musicService.GetMusicById(newMusic.Id);
            var resultMusic = _mapper.Map<MusicViewModel>(music);

            return Ok(resultMusic);
        }

        [HttpPut("")]
        public async Task<ActionResult<MusicViewModel>> UpdateMusic([FromBody] SaveMusicViewModel model,[FromQuery]int? id)
        {
            var validator = new SaveMusicValidator();
            var validatorRes = await validator.ValidateAsync(model);

            var requestIsInvalid = !validatorRes.IsValid || !id.HasValue;

            if (requestIsInvalid)
                return BadRequest(validatorRes.Errors);
            var musicToUpdate = await _musicService.GetMusicById(id.Value);
            if (musicToUpdate is null)
                return NotFound();

            var music = _mapper.Map<Music>(model);
            await _musicService.UpdateMusic(musicToUpdate, music);

            var updatedMusic = await _musicService.GetMusicById(id.Value);
            var updatedResult = _mapper.Map<MusicViewModel>(updatedMusic);

            return Ok(updatedResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(int? id)
        {
            if (!id.HasValue && id.Value == 0)
                return BadRequest();
            var music = await _musicService.GetMusicById(id.Value);
            if (music is null)
                return NotFound();
            await _musicService.DeleteMusic(music);
            return NoContent();
        }
    }
}
