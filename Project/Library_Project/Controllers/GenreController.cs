using Library_Project.Data;
using Library_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Project.Controllers;

public class GenreController : Controller
{
    private readonly AppDbContext _context;

    public GenreController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Genre
    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genres.ToListAsync();
        return View(genres);
    }

    // GET: /Genre/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null) return NotFound();

        return View(genre);
    }

    // GET: /Genre/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Genre/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Genre/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null) return NotFound();

        return View(genre);
    }

    // POST: /Genre/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genre genre)
    {
        if (id != genre.Id) return NotFound();

        if (!ModelState.IsValid) return View(genre);

        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Genre/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return NotFound();
        return View(genre);
    }

    // POST: /Genre/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return NotFound();
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}