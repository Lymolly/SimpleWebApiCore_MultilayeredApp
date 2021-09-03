using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMusic.Core.Entities;
using MyMusic.Core.Interfaces;
using MyMusic.Services.Interfaces;

namespace MyMusic.Services.Services
{
    public class MusicService : IMusicService
    {
        private readonly IUnitOfWork Database;

        public MusicService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<IEnumerable<Music>> GetAllWithArtist()
        {
            return await Database.Musics.GetAllWithArtistAsync();
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await Database.Musics.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId)
        {
            return await Database.Musics.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public async Task<Music> CreateMusic(Music newMusic)
        {
            await Database.Musics.AddAsync(newMusic);
            await Database.SaveAsync();
            return newMusic;
        }

        public async Task UpdateMusic(Music musicToBeUpdated, Music music)
        {
            musicToBeUpdated.Name = music.Name;
            musicToBeUpdated.ArtistId = music.ArtistId;
            await Database.SaveAsync();
        }

        public async Task DeleteMusic(Music music)
        { 
            Database.Musics.Remove(music);
            await Database.SaveAsync();
        }
    }
}
