using Library_Project.Data;
using Library_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Project.Controllers;

public class BorrowingController : Controller
{
    private readonly AppDbContext _context;

    public BorrowingController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Borrowing
    public async Task<IActionResult> Index()
    {
        var borrowings = await _context.Borrowings
            .Include(br => br.Book)
            .Include(br => br.User)
            .ToListAsync();

        return View(borrowings);
    }

    // GET: /Borrowing/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var borrowing = await _context.Borrowings
            .Include(br => br.Book)
            .Include(br => br.User)
            .FirstOrDefaultAsync(br => br.Id == id);

        if (borrowing == null) return NotFound();

        return View(borrowing);
    }

    // GET: /Borrowing/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Books = await _context.Books.ToListAsync();
        ViewBag.Users = await _context.Users.ToListAsync();
        return View();
    }

    // POST: /Borrowing/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Borrowing borrowing)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Books = await _context.Books.ToListAsync();
            ViewBag.Users = await _context.Users.ToListAsync();
            return View(borrowing);
        }

        borrowing.BorrowDate = DateTime.UtcNow;
        borrowing.Status = BorrowingStatus.Active;

        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Borrowing/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);

        if (borrowing == null) return NotFound();

        ViewBag.Books = await _context.Books.ToListAsync();
        ViewBag.Users = await _context.Users.ToListAsync();

        return View(borrowing);
    }

    // POST: /Borrowing/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Borrowing borrowing)
    {
        if (id != borrowing.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Books = await _context.Books.ToListAsync();
            ViewBag.Users = await _context.Users.ToListAsync();
            return View(borrowing);
        }

        _context.Borrowings.Update(borrowing);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Borrowing/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var borrowing = await _context.Borrowings
            .Include(br => br.Book)
            .Include(br => br.User)
            .FirstOrDefaultAsync(br => br.Id == id);
        if (borrowing == null) return NotFound();
        return View(borrowing);
    }

    // POST: /Borrowing/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);
        if (borrowing == null) return NotFound();
        _context.Borrowings.Remove(borrowing);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}