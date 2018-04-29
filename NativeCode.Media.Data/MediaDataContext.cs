namespace NativeCode.Media.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Data;
    using NativeCode.Media.Data.Media;

    public class MediaDataContext : DataContext, IMediaDataContext
    {
        public DbSet<Episode> Episodes { get; set; }

        public DbSet<MediaFile> MediaFile { get; set; }

        public DbSet<MediaLink> MediaLinks { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Series> Series { get; set; }
    }
}
