using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Data;
using mvc_project.Models;

namespace mvc_project.Controllers;

public class AuthorsController : Controller
{
    private readonly AppDbContext _context;
    public AuthorsController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Authors.ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var author = await _context.Authors.Include(item => item.Books).FirstOrDefaultAsync(item => item.Id == id);
        return author == null ? NotFound() : View(author);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Country")] Author author)
    {
        if (!ModelState.IsValid) return View(author);
        _context.Add(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var author = await _context.Authors.FindAsync(id);
        return author == null ? NotFound() : View(author);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country")] Author author)
    {
        if (id != author.Id) return NotFound();
        if (!ModelState.IsValid) return View(author);
        _context.Update(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var author = await _context.Authors.FirstOrDefaultAsync(item => item.Id == id);
        return author == null ? NotFound() : View(author);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null) _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
