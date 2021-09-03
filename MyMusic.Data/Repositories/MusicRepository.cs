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
    class MusicRepository : BaseRepository<Music>,IMusicRepository
    {
        public MusicRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await MyContext.Musics.Include(m => m.Artist).ToListAsync();
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await MyContext.Musics.Include(m => m.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MyContext.Musics.Include(m => m.Artist)
                .Where(m => m.ArtistId == artistId).ToListAsync();
        }
        private MyDbContext MyContext => Context as MyDbContext;
    }
}
