using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using parcialherramientas.Data;
using parcialherramientas.Models;

namespace parcialherramientas.Controllers
{
    public class ActorController : Controller
    {
        private readonly MovieContext _context;

        public ActorController(MovieContext context)
        {
            _context = context;
        }

        // GET: Actor
        public async Task<IActionResult> Index()
        {
              return _context.ActorDbSet != null ? 
                          View(await _context.ActorDbSet.ToListAsync()) :
                          Problem("Entity set 'MovieContext.Actor'  is null.");
        }

        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActorDbSet == null)
            {
                return NotFound();
            }

            var actor = await _context.ActorDbSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actor/Create
        public IActionResult Create()
        {  
            ViewData["Movies"] = new SelectList(_context.Set<Movie>(), "Id", "Id");
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Country, MovieId")] Actor actor)
        {   
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            

            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActorDbSet == null)
            {
                return NotFound();
            }

            var actor = await _context.ActorDbSet.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Country")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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
            return View(actor);
        }

        // GET: Actor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActorDbSet == null)
            {
                return NotFound();
            }

            var actor = await _context.ActorDbSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActorDbSet == null)
            {
                return Problem("Entity set 'MovieContext.Actor'  is null.");
            }
            var actor = await _context.ActorDbSet.FindAsync(id);
            if (actor != null)
            {
                _context.ActorDbSet.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
          return (_context.ActorDbSet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
