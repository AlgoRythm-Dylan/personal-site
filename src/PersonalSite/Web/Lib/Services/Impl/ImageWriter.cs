using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class ImageWriter : IImageWriter
    {
        private readonly AppSettings Settings;
        public ImageWriter(AppSettings settings)
        {
            Settings = settings;
        }
    }
}
