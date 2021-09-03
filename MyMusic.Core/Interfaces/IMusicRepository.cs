using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core.Entities;

namespace MyMusic.Core.Interfaces
{
    public interface IMusicRepository : IRepository<Music>
    {
        Task<IEnumerable<Music>> GetAllWithArtistAsync();
        Task<Music> GetWithArtistByIdAsync(int id);
        Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId);

    }
}