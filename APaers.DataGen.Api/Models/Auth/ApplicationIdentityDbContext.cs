using Microsoft.AspNet.Identity.EntityFramework;

namespace APaers.DataGen.Api.Models.Auth
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext()
            : base("Main", false)
        {
        }

        public static ApplicationIdentityDbContext Create()
        {
            return new ApplicationIdentityDbContext();
        }
    }
}