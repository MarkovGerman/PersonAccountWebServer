using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Contexts;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Managers
{
    public class ResidentManager : IResidentManager
    {
        private readonly ResidentContext context;

        public ResidentManager(ResidentContext context)
        {
            this.context = context;
        }
        public async Task<List<Resident>> GetAll()
        {
            return await context.Residents.ToListAsync();
        }

        public async Task<List<Resident>> Get(int idPersonalAccount)
        {
            return await context.Residents.Where(p => p.IdRoom == idPersonalAccount).ToListAsync();
        }

        public async Task<Resident?> GetById(int id)
        {
            return await context.Residents.FindAsync(id);
        }

        public async Task<Resident?> Delete(int id)
        {
            var resident = context.Residents.FirstOrDefault(u => u.Id == id);
            context.Residents.Remove(resident);
            await context.SaveChangesAsync();
            return resident;
        }

        public async void Create(Resident resident)
        {
            await context.Residents.AddAsync(resident);
            await context.SaveChangesAsync();
        }

        public async Task<Resident> Update(Resident resident)
        {
            var residentEntity = context.Residents.Update(resident);
            await context.SaveChangesAsync();
            return residentEntity.Entity;
        }

        public async Task<bool> ExistResident(int id)
        {
            return await context.Residents.AllAsync(u => u.IdRoom != id);
        }
    }
}