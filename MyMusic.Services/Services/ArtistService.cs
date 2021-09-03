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
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork Database;
        public ArtistService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await Database.Artists.GetAllAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await Database.Artists.GetByIdAsync(id);
        }

        public async Task<Artist> CreateArtist(Artist newArtist)
        {
            await Database.Artists.AddAsync(newArtist);
            await Database.SaveAsync();
            return newArtist;
        }

        public async Task UpdateArtist(Artist artistToBeUpdated, Artist artist)
        {
            artistToBeUpdated.Name = artist.Name;
            await Database.SaveAsync();
        }

        public async Task DeleteArtist(Artist artist)
        {
            Database.Artists.Remove(artist);
            await Database.SaveAsync();
        }
    }
}
