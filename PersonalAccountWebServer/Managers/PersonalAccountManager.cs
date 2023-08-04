using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Contexts;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Managers
{
    public class PersonalAccountManager : IPersonalAccountManager
    {
        private readonly PersonalAccountContext context;

        public PersonalAccountManager(PersonalAccountContext context)
        {
            this.context = context;
        }

        public async Task<List<PersonalAccount>> GetAll()
        {
            return await context.PersonalAccounts.ToListAsync();
        }

        public async Task<PersonalAccount?> Get(int id)
        {
            return await context.PersonalAccounts.FindAsync(id);
        }

        public async Task<PersonalAccount?> Delete(int id)
        {
            var personalAccount = context.PersonalAccounts.FirstOrDefault(u => u.Id == id);
            context.PersonalAccounts.Remove(personalAccount);
            await context.SaveChangesAsync();
            return personalAccount;
        }

        public async void Create(PersonalAccount personalAccount)
        {
            await context.PersonalAccounts.AddAsync(personalAccount);
            await context.SaveChangesAsync();
        }

        public async Task<PersonalAccount> Update(PersonalAccount personalAccount)
        {
            var serviceInfoEntityEntry = context.PersonalAccounts.Update(personalAccount);
            await context.SaveChangesAsync();
            return serviceInfoEntityEntry.Entity;
        }
    }
}