namespace Web.Lib.Services.Impl
{
    public class ViewerDbCtx : AppDbCtx
    {
        public ViewerDbCtx(AppSettings settings) : base(settings.ViewerConnectionString) { }
    }
}
