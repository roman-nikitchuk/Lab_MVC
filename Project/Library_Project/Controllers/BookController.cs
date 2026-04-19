using Library_Project.Data;
using Library_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Project.Controllers;

public class BookController : Controller
{
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Book
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();

        return View(books);
    }

    // GET: /Book/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Borrowings)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return NotFound();

        return View(book);
    }

    // GET: /Book/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Authors = await _context.Authors.ToListAsync();
        ViewBag.Genres = await _context.Genres.ToListAsync();
        return View();
    }

    // POST: /Book/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Genres = await _context.Genres.ToListAsync();
            return View(book);
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null) return NotFound();

        ViewBag.Authors = await _context.Authors.ToListAsync();
        ViewBag.Genres = await _context.Genres.ToListAsync();

        return View(book);
    }

    // POST: /Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Genres = await _context.Genres.ToListAsync();
            return View(book);
        }

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}