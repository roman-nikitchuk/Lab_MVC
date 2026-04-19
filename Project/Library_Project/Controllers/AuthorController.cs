using Library_Project.Data;
using Library_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Project.Controllers;

public class AuthorController : Controller
{
    private readonly AppDbContext _context;

    public AuthorController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Author
    public async Task<IActionResult> Index()
    {
        var authors = await _context.Authors.ToListAsync();
        return View(authors);
    }

    // GET: /Author/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null) return NotFound();

        return View(author);
    }

    // GET: /Author/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Author/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Author/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null) return NotFound();

        return View(author);
    }

    // POST: /Author/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Author author)
    {
        if (id != author.Id) return NotFound();

        if (!ModelState.IsValid) return View(author);

        _context.Authors.Update(author);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Author/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null) return NotFound();

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}