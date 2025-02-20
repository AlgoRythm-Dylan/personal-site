using PersonalSiteLib.Entities;

namespace Management.Services
{
    public interface IPhotographerService
    {
        public IQueryable<Photographer> GetAll();
        public Task<Photographer> InsertAsync(Photographer photographer);
    }
}
