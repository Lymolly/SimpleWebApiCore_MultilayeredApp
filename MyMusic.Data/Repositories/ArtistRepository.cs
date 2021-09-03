using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Entities;
using MyMusic.Core.Interfaces;
using MyMusic.Data.Context;

namespace MyMusic.Data.Repositories
{
    public class ArtistRepository :BaseRepository<Artist>,IArtistRepository
    {
        public ArtistRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await MyContext.Artists.Include(a => a.Musics).ToListAsync();
        }

        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return await MyContext.Artists.Include(a => a.Musics)
                .SingleOrDefaultAsync(a => a.Id == id);
        }
        private MyDbContext MyContext => Context as MyDbContext;
    }
}
