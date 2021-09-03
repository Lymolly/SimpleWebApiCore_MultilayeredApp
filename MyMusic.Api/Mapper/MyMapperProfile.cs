using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMusic.Api.Models.ViewModels;
using MyMusic.Core.Entities;

namespace MyMusic.Api.Mapper
{
    public class MyMapperProfile : Profile
    {
        public MyMapperProfile()
        {
            CreateMap<Music, MusicViewModel>().ReverseMap();
            CreateMap<Artist, ArtistViewModel>().ReverseMap();

            CreateMap<SaveArtistViewModel, Artist>();
            CreateMap<SaveMusicViewModel, Music>();
        }
    }
}
