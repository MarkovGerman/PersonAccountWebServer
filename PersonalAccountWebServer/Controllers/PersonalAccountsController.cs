using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Managers;
using PersonalAccountWebServer.Models;

namespace WebApplication2.Controllers
{
    public class PersonalAccountsController : Controller
    {
        private readonly IPersonalAccountManager manager;
        private readonly IResidentManager residentManager;
        public PersonalAccountsController(IPersonalAccountManager manager, IResidentManager residentManager)
        {
            this.manager = manager;
            this.residentManager = residentManager;
        }

        // GET: PersonalAccounts
        public async Task<IActionResult> Index()
        {
            var accounts = await manager.GetAll();
            accounts.ForEach(a => a.ExistResident = !residentManager.ExistResident(a.Id).Result);
              return accounts.Count != 0 ? 
                          View(accounts) :
                          Problem("Количество лицевых счетов пусто");
        }

        // GET: PersonalAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccount = await manager.Get(id.Value);
            if (personalAccount == null)
            {
                return NotFound();
            }
            var residents = await residentManager.Get(personalAccount.Id);
            return View(Tuple.Create(personalAccount, residents));
        }

        // GET: PersonalAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberPA,DateStart,DateEnd,Address,Area")] PersonalAccount personalAccount)
        {
            if (ModelState.IsValid)
            {
                manager.Create(personalAccount);
                return RedirectToAction(nameof(Index));
            }
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccount = await manager.Get(id.Value);
            if (personalAccount == null)
            {
                return NotFound();
            }
            return View(personalAccount);
        }

        // POST: PersonalAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberPA,DateStart,DateEnd,Address,Area")] PersonalAccount personalAccount)
        {
            if (id != personalAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await manager.Update(personalAccount);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await PersonalAccountExists(personalAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccount = await manager.Get(id.Value);
            if (personalAccount == null)
            {
                return NotFound();
            }

            return View(personalAccount);
        }

        // POST: PersonalAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalAccount = await manager.Get(id);
            if (personalAccount != null)
            {
                await manager.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PersonalAccountExists(int id)
        {
            var accounts = await manager.GetAll();
            return (accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
