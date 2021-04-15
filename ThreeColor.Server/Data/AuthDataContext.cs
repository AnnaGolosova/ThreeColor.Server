using Microsoft.AspNet.Identity.EntityFramework;

namespace ThreeColor.Server.Data
{
    public class AuthDataContext : IdentityDbContext<IdentityUser>
    {
        public AuthDataContext()
#if DEBUG
            : base("TestConnection") { }
#else
            : base("ProdConnection") { }
#endif
    }
}