namespace NativeCode.Media.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Media.Data.Media;

    public interface IMediaDataContext
    {
        DbSet<Episode> Episodes { get; set; }

        DbSet<MediaFile> MediaFile { get; set; }

        DbSet<MediaLink> MediaLinks { get; set; }

        DbSet<Movie> Movies { get; set; }

        DbSet<Season> Seasons { get; set; }

        DbSet<Series> Series { get; set; }
    }
}