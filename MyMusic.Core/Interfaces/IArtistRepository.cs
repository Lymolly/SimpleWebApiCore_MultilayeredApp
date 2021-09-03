using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core.Entities;

namespace MyMusic.Core.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<IEnumerable<Artist>> GetAllWithMusicsAsync();
        Task<Artist> GetWithMusicsByIdAsync(int id);
    }
}