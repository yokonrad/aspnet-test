using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class ActorMoviesController : Controller
    {
        private readonly TestContext _context;

        public ActorMoviesController(TestContext context)
        {
            _context = context;
        }

        // GET: ActorMovies
        public async Task<IActionResult> Index()
        {
            var testContext = _context.ActorMovie.Include(a => a.Actor).Include(a => a.Movie);
            return View(await testContext.ToListAsync());
        }

        // GET: ActorMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActorMovie == null)
            {
                return NotFound();
            }

            var actorMovie = await _context.ActorMovie
                .Include(a => a.Actor)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actorMovie == null)
            {
                return NotFound();
            }

            return View(actorMovie);
        }

        // GET: ActorMovies/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            return View();
        }

        // POST: ActorMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorId,MovieId")] ActorMovie actorMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", actorMovie.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", actorMovie.MovieId);
            return View(actorMovie);
        }

        // GET: ActorMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActorMovie == null)
            {
                return NotFound();
            }

            var actorMovie = await _context.ActorMovie.FindAsync(id);
            if (actorMovie == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", actorMovie.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", actorMovie.MovieId);
            return View(actorMovie);
        }

        // POST: ActorMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,MovieId")] ActorMovie actorMovie)
        {
            if (id != actorMovie.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorMovieExists(actorMovie.ActorId))
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
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", actorMovie.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", actorMovie.MovieId);
            return View(actorMovie);
        }

        // GET: ActorMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActorMovie == null)
            {
                return NotFound();
            }

            var actorMovie = await _context.ActorMovie
                .Include(a => a.Actor)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actorMovie == null)
            {
                return NotFound();
            }

            return View(actorMovie);
        }

        // POST: ActorMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActorMovie == null)
            {
                return Problem("Entity set 'TestContext.ActorMovie'  is null.");
            }
            var actorMovie = await _context.ActorMovie.FindAsync(id);
            if (actorMovie != null)
            {
                _context.ActorMovie.Remove(actorMovie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorMovieExists(int id)
        {
            return _context.ActorMovie.Any(e => e.ActorId == id);
        }
    }
}
