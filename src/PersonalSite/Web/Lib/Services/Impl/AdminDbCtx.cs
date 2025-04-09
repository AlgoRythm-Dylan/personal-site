namespace Web.Lib.Services.Impl
{
    public class AdminDbCtx : AppDbCtx
    {
        public AdminDbCtx(AppSettings settings) : base(settings.AdminConnectionString) { }
    }
}
