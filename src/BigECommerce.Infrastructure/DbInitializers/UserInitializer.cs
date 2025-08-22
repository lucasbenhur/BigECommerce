using BigECommerce.Domain.Entities;
using BigECommerce.Infrastructure.DbContexts;

namespace BigECommerce.Infrastructure.DbInitializers
{
    public static class UserInitializer
    {
        public static void Initialize(BigECommerceDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User("admin@bigecommerce.com", "admin123", "Admin"),
                    new User("client@bigecommerce.com", "client123", "Client")
                };

                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
        }
    }
}
