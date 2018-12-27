namespace NativeCode.Node.Media
{
    using Data.Library.Plex;
    using Data.Movies;
    using Data.Series;
    using Data.Storage;
    using Microsoft.EntityFrameworkCore;
    using NativeCode.Core.Data;
    using NativeCode.Core.Data.Extensions;

    public class MediaDataContext : DataContext
    {
        /// <inheritdoc />
        public MediaDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Mount> Mounts { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieCollection> MovieCollections { get; set; }

        public DbSet<PlexLibrarySource> PlexLibrarySources { get; set; }

        public DbSet<PlexServerInfo> PlexServerInfo { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Series> Series { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedJsonDataFromManifest<Mount>("NativeCode.Node.Media.Seed.Mount.json");
            modelBuilder.SeedJsonDataFromManifest<MountPath>("NativeCode.Node.Media.Seed.MountPath.json");

            base.OnModelCreating(modelBuilder);
        }
    }
}
