using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.AuthEntities;
using MyMusic.Core.Entities;
using MyMusic.Data.EntitiesConfig;

namespace MyMusic.Data.Context
{
    public class MyDbContext : IdentityDbContext<User,Role,Guid>
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MusicConfig());
            builder.ApplyConfiguration(new ArtistConfig());
        }
    }
}
