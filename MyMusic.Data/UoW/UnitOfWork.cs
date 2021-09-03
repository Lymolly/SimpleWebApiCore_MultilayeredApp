using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMusic.Core.Interfaces;
using MyMusic.Data.Context;
using MyMusic.Data.Repositories;

namespace MyMusic.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMusicRepository _musRepo;
        private IArtistRepository _artRepo;
        private readonly MyDbContext _context;

        public UnitOfWork(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IMusicRepository Musics => _musRepo ??= new MusicRepository(_context);
        public IArtistRepository Artists => _artRepo ??= new ArtistRepository(_context);
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
