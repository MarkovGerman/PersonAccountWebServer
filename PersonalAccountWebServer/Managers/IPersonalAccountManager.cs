using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Managers
{
    public interface IPersonalAccountManager
    {
        Task<List<PersonalAccount>> GetAll();
        Task<PersonalAccount?> Get(int id);
        Task<PersonalAccount?> Delete(int id);
        void Create(PersonalAccount personalAccount);
        Task<PersonalAccount> Update(PersonalAccount serviceInfo);
    }
}