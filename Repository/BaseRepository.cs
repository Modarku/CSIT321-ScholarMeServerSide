using RestTest;

namespace ScholarMeServer.Repository
{
    public abstract class BaseRepository
    {
        protected readonly ScholarMeDbContext _scholarmeDbContext;

        public BaseRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }

        // Check if there are any changes in the database (Use for success/failure operations)
        public bool HasChanges()
        {
            return _scholarmeDbContext.ChangeTracker.HasChanges();
        }
    }
}
