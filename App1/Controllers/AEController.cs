using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App1.Data;
using App1.Models;
using Microsoft.AspNetCore.Authorization;

namespace App1.Controllers
{
    [Authorize]
    public class AEController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AEController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AE
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utb.ToListAsync());
        }

        // GET: AE/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEntity = await _context.Utb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aEntity == null)
            {
                return NotFound();
            }

            return View(aEntity);
        }

        // GET: AE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] AEntity aEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aEntity);
        }

        // GET: AE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEntity = await _context.Utb.FindAsync(id);
            if (aEntity == null)
            {
                return NotFound();
            }
            return View(aEntity);
        }

        // POST: AE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] AEntity aEntity)
        {
            if (id != aEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AEntityExists(aEntity.Id))
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
            return View(aEntity);
        }

        // GET: AE/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEntity = await _context.Utb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aEntity == null)
            {
                return NotFound();
            }

            return View(aEntity);
        }

        // POST: AE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aEntity = await _context.Utb.FindAsync(id);
            if (aEntity != null)
            {
                _context.Utb.Remove(aEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AEntityExists(int id)
        {
            return _context.Utb.Any(e => e.Id == id);
        }
    }
}
