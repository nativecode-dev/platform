namespace NativeCode.Node.Media.Data
{
    using Data.Catalog.Movies;
    using Data.Catalog.Shows;
    using Data.External.LibrarySources.Plex;
    using Data.MediaSources;
    using Data.Storage;
    using Microsoft.EntityFrameworkCore;
    using NativeCode.Core.Data;
    using NativeCode.Core.Data.Extensions;

    public class MediaDataContext : DataContext<MediaDataContext>
    {
        /// <inheritdoc />
        public MediaDataContext(DbContextOptions<MediaDataContext> options)
            : base(options)
        {
        }

        public DbSet<Credential> Credentials { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Mount> Mounts { get; set; }

        public DbSet<MovieCollection> MovieCollections { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<PlexServerInfo> PlexServerInfo { get; set; }

        public DbSet<PlexLibrarySource> PlexSources { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Series> Series { get; set; }

        public DbSet<MediaSourceEpisode> SourceEpisodes { get; set; }

        public DbSet<MediaSourceMovie> SourceMovies { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Episode>()
                .HasOne<Season>()
                .WithOne()
                .HasForeignKey<Season>()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.SeedJsonDataFromManifest<Mount>("NativeCode.Node.Media.Data.Seed.Mount.json");
            builder.SeedJsonDataFromManifest<MountPath>("NativeCode.Node.Media.Data.Seed.MountPath.json");

            base.OnModelCreating(builder);
        }
    }
}
