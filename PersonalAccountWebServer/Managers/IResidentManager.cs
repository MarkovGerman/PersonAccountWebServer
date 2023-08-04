using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Managers
{
    public interface IResidentManager
    {
        Task<List<Resident>> Get(int idPersonalAccount);
        Task<Resident?> GetById(int id);
        Task<List<Resident>> GetAll();
        Task<Resident?> Delete(int id);
        void Create(Resident resident);
        Task<Resident> Update(Resident resident);
        Task<bool> ExistResident(int id);
    }
}