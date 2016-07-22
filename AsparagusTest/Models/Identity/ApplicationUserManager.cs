using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.Entity;

namespace AsparagusTest.Models.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private static ApplicationContext db;
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) 
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                            IOwinContext context)
        {
            db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }

        public async override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            if (user != null && user.Email != null && user.UserName != null)
            {
                try
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    return IdentityResult.Failed(ex.Message);
                }
            }
            else return IdentityResult.Failed("NullReference");
        }

        public async override Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            if (userId != null && login != null)
            {
                try
                {
                    ApplicationUser user = db.Users.Find(userId);
                    if (user != null)
                    {
                        user.Logins.Add(new IdentityUserLogin() { UserId = userId, LoginProvider = login.LoginProvider,
                        ProviderKey = login.ProviderKey});
                        db.Entry(user).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return IdentityResult.Success;
                    }                                       
                }
                catch (Exception ex)
                {
                    return IdentityResult.Failed(ex.Message);
                }
            }
            return IdentityResult.Failed("NullReference");
        }

        public IQueryable<IGrouping<string, AsparagusTest.Models.Food>> GetAllFoods()
        {
            return db.Foods.GroupBy(p => p.User.UserName);
        }

        public void AddFood(Food food)
        {
            db.Foods.Add(food);
            db.SaveChanges();
        }
    }
}