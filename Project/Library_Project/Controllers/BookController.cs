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
    public async Task<IActionResult> Index(string? search)
    {
        var books = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            books = books.Where(b =>
                b.Name.ToLower().Contains(search.ToLower()) ||
                b.Author!.Name.ToLower().Contains(search.ToLower()) ||
                b.Genre!.Name.ToLower().Contains(search.ToLower()));
        }

        ViewBag.Search = search;
        return View(await books.ToListAsync());
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
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: /Book/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books
            .Include(b => b.Borrowings)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return NotFound();

        var hasActiveBorrowings = book.Borrowings != null &&
            book.Borrowings.Any(br => br.Status == BorrowingStatus.Active);

        if (hasActiveBorrowings)
        {
            TempData["Error"] = "Cannot delete book with active borrowings. Please return all copies first.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        if (book.Borrowings != null && book.Borrowings.Any())
        {
            _context.Borrowings.RemoveRange(book.Borrowings);
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}