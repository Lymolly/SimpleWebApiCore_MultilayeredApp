using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusic.Core.Entities;

namespace MyMusic.Data.EntitiesConfig
{
    public class ArtistConfig : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(55);

            builder.HasMany(a => a.Musics).WithOne(m => m.Artist);
            builder.ToTable("Artists");
        }
    }
}
