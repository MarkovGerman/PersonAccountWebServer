using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Contexts;
using PersonalAccountWebServer.Managers;
using PersonalAccountWebServer.Models;

namespace WebApplication2.Controllers
{
    public class ResidentsController : Controller
    {
        private readonly IResidentManager manager;

        public ResidentsController(IResidentManager manager)
        {
            this.manager = manager;
        }

        // GET: Residents
        public async Task<IActionResult> Index()
        {
            var residents = await manager.GetAll();
              return residents.Count !=0 ? 
                          View(residents) :
                          Problem("Количество жильцов = 0");
        }

        // GET: Residents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await manager.GetById(id.Value);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        // GET: Residents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Residents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,IdRoom")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                manager.Create(resident);
                return RedirectToAction(nameof(Index));
            }
            return View(resident);
        }

        // GET: Residents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await manager.GetById(id.Value);
            if (resident == null)
            {
                return NotFound();
            }
            return View(resident);
        }

        public async Task<IActionResult> EmptyRoom()
        {
            return View();
        }

        public async Task<bool> ExistResident(int id)
        {
            return await manager.ExistResident(id);
        }
        
        public async Task<IActionResult> ResidentsByRoom(int? id)
        {
            if (id == null)
                return NotFound();
            var residents = await manager.Get(id.Value);
            return residents.Count !=0 ? 
                View(residents) :
                RedirectToAction(nameof(EmptyRoom));
        }

        // POST: Residents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,MiddleName,IdRoom")] Resident resident)
        {
            if (id != resident.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await manager.Update(resident);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ResidentExists(resident.Id))
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
            return View(resident);
        }

        // GET: Residents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await manager.GetById(id.Value);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resident = await manager.GetById(id);
            if (resident != null)
            {
                await manager.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ResidentExists(int id)
        {
            var residents = await manager.GetAll();
            return (residents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
