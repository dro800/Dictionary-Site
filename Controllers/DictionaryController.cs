using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dictionary_Site.Data;
using Dictionary_Site.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dictionary_Site.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DictionaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dictionary
        public async Task<IActionResult> Index()
        {
              return _context.Dictionary != null ? 
                          View(await _context.Dictionary.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Dictionary'  is null.");
        }

        // GET: Dictionary/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
              return View();
        }

        // Post: Dictionary/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchWord)
        {
              return View("Index", await _context.Dictionary.Where(d => d.Term.Contains(SearchWord)).ToListAsync());
        }

        // GET: Dictionary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dictionary == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictionary == null)
            {
                return NotFound();
            }

            return View(dictionary);
        }

        // GET: Dictionary/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dictionary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Term,Definition")] Dictionary dictionary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dictionary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictionary);
        }

        // GET: Dictionary/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dictionary == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionary.FindAsync(id);
            if (dictionary == null)
            {
                return NotFound();
            }
            return View(dictionary);
        }

        // POST: Dictionary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Term,Definition")] Dictionary dictionary)
        {
            if (id != dictionary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictionary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictionaryExists(dictionary.Id))
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
            return View(dictionary);
        }

        // GET: Dictionary/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dictionary == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictionary == null)
            {
                return NotFound();
            }

            return View(dictionary);
        }

        // POST: Dictionary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dictionary == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Dictionary'  is null.");
            }
            var dictionary = await _context.Dictionary.FindAsync(id);
            if (dictionary != null)
            {
                _context.Dictionary.Remove(dictionary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictionaryExists(int id)
        {
          return (_context.Dictionary?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
