using System;
using System.Threading.Tasks;

namespace MyMusic.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        Task<int> SaveAsync();
    }
}