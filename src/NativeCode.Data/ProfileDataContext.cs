namespace NativeCode.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Data.Profiles;

    public class ProfileDataContext : DataContext
    {
        public DbSet<Profile> Profiles { get; protected set; }

        public DbSet<State> States { get; protected set; }
    }
}
