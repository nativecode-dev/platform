namespace NativeCode.Clients.Radarr.Responses
{
    public enum CommandKind
    {
        None = 0,

        CutOffUnmetMoviesSearch,

        DownloadedMoviesScan,

        MissingMoviesSearch,

        MoviesSearch,

        NetImportSync,

        RefreshMovie,

        RenameFiles,

        RenameMovies,

        RescanMovie,

        RssSync
    }
}
