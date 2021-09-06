using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MyMusic.Api.Models.ViewModels;
using MyMusic.Api.Validators;
using MyMusic.Core.Entities;
using MyMusic.Services.Interfaces;

namespace MyMusic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize/*(Policy ="Test")*/]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService service, IMapper mapper)
        {
            _artistService = service;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistViewModel>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();

            var res = _mapper.Map<IEnumerable<ArtistViewModel>>(artists);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistViewModel>> GetArtistById(int? id)
        {
            if (id.HasValue)
            {
                var artist = await _artistService.GetArtistById(id.Value);
                if (artist is null)
                    return NotFound();
                var res = _mapper.Map<ArtistViewModel>(artist);
                return Ok(res);
            }

            return BadRequest();
        }
        [HttpPost("")]
        public async Task<ActionResult<ArtistViewModel>> CreateArtist([FromBody] SaveArtistViewModel model)
        {
            var validator = new SaveArtistValidator();
            var validatorRes = await validator.ValidateAsync(model);
            if (!validatorRes.IsValid)
                return BadRequest(validatorRes.Errors);
            var artistToCreate = _mapper.Map<Artist>(model);
            var newArtist = await _artistService.CreateArtist(artistToCreate);

            var artistRes = _mapper.Map<ArtistViewModel>(await _artistService.GetArtistById(newArtist.Id));
            return Ok(artistRes);
        }

        //TODO Add update and delete actions!
    }
}
