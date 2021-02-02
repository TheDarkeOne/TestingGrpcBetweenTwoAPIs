using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing.Services
{
    public class EFCoreService : IDataService
    {
        private readonly ApplicationDbContext context;

        public EFCoreService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateUsers(Users users)
        {
            context.Users.Add(users);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Users> Users()
        {
            var users = context.Users.ToList();
            return users;
        }

        public Users User(int id)
        {
            return context.Users.Find(id);
        }
    }
}
