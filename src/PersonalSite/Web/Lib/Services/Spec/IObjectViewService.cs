namespace Web.Lib.Services.Spec
{
    public interface IObjectViewService
    {
        public Task RecordPageViewAsync(string pageName);
        public Task RecordImageViewAsync(string imageID);
        public Task<int> GetPageTotalViewsAsync(string pageName);
        public Task<int> GetImageTotalViewsAsync(string imageID);
    }
}
