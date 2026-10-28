using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Data;
using mvc_project.Models;

namespace mvc_project.Controllers;

public class BooksController : Controller
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .Include(book => book.Author)
            .Include(book => book.Category)
            .Include(book => book.LibraryMember)
            .ToListAsync();
        return View(books);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var book = await _context.Books
            .Include(item => item.Author)
            .Include(item => item.Category)
            .Include(item => item.LibraryMember)
            .FirstOrDefaultAsync(item => item.Id == id);
        return book == null ? NotFound() : View(book);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,PublishedYear,AuthorId,CategoryId,LibraryMemberId")] Book book)
    {
        if (!ModelState.IsValid) return View(book);
        _context.Add(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var book = await _context.Books.FindAsync(id);
        return book == null ? NotFound() : View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PublishedYear,AuthorId,CategoryId,LibraryMemberId")] Book book)
    {
        if (id != book.Id) return NotFound();
        if (!ModelState.IsValid) return View(book);
        try
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(book.Id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var book = await _context.Books
            .Include(item => item.Author)
            .Include(item => item.Category)
            .Include(item => item.LibraryMember)
            .FirstOrDefaultAsync(item => item.Id == id);
        return book == null ? NotFound() : View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null) _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id) => _context.Books.Any(item => item.Id == id);
}
