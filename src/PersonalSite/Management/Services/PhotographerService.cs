using Microsoft.EntityFrameworkCore;
using PersonalSiteLib;
using PersonalSiteLib.Entities;

namespace Management.Services
{
    public class PhotographerService : IPhotographerService
    {

        private readonly PersonalSiteCtx Ctx;
        public PhotographerService(PersonalSiteCtx ctx)
        {
            Ctx = ctx;
        }

        public IQueryable<Photographer> GetAll()
        {
            return Ctx.Photographers.AsQueryable();
        }

        public async Task<Photographer> InsertAsync(Photographer photographer)
        {
            Ctx.Photographers.Add(photographer);
            await Ctx.SaveChangesAsync();
            return photographer;
        }
    }
}
