using Microsoft.EntityFrameworkCore;
using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class ObjectViewService : IObjectViewService
    {
        private readonly ViewerDbCtx Ctx;
        public ObjectViewService(ViewerDbCtx ctx)
        {
            Ctx = ctx;
        }

        public async Task RecordPageViewAsync(string pageName)
        {
            var newRecord = new PageView()
            {
                PageName = pageName,
                Timestamp = DateTime.Now
            };
            await Ctx.PageViews.AddAsync(newRecord);
            await Ctx.SaveChangesAsync();
        }

        public async Task RecordImageViewAsync(string imageID)
        {
            var newRecord = new ImageView()
            {
                ImageID = imageID,
                Timestamp = DateTime.Now
            };
            await Ctx.ImageViews.AddAsync(newRecord);
            await Ctx.SaveChangesAsync();
        }

        public Task<int> GetPageTotalViewsAsync(string pageName)
        {
            return Ctx.PageViews.CountAsync(view => view.PageName == pageName);
        }

        public Task<int> GetImageTotalViewsAsync(string imageID)
        {
            return Ctx.ImageViews.CountAsync(img => img.ImageID == imageID);
        }
    }
}
