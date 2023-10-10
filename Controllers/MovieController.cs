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
    public class MovieController : Controller
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            //var movieContext = _context.Movie.Include(m => m.Actores);
            return _context.MovieDbSet != null ? 
                          View(await _context.MovieDbSet.ToListAsync()) :
                          Problem("Entity set 'MovieContext.MovieDbSet'  is null.");
            //return View(await movieContext.ToListAsync());
        }
        

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieDbSet == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieDbSet
                //.Include(m => m.Actores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            //ViewData["ActorId"] = new SelectList(_context.Set<Actor>(), "Id", "Id");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Year,Gender")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ActorId"] = new SelectList(_context.Set<Actor>(), "Id", "Id", movie.ActorId);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieDbSet == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieDbSet.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            //ViewData["ActorId"] = new SelectList(_context.Set<Actor>(), "Id", "Id", movie.ActorId);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Year,Gender")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            //ViewData["ActorId"] = new SelectList(_context.Set<Actor>(), "Id", "Id", movie.ActorId);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieDbSet == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieDbSet
                //.Include(m => m.Actores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieDbSet == null)
            {
                return Problem("Entity set 'MovieContext.Movie'  is null.");
            }
            var movie = await _context.MovieDbSet.FindAsync(id);
            if (movie != null)
            {
                _context.MovieDbSet.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.MovieDbSet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
