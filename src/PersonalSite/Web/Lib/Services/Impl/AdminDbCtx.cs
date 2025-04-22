using Microsoft.EntityFrameworkCore;
using Web.Lib.Models;

namespace Web.Lib.Services.Impl
{
    public class AdminDbCtx : AppDbCtx
    {

        public DbSet<LoginAudit> LoginAudit { get; set; }

        public AdminDbCtx(AppSettings settings) : base(settings.AdminConnectionString) { }
    }
}
