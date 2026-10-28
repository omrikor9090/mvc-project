using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Data;
using mvc_project.Models;

namespace mvc_project.Controllers;

public class LibraryMembersController : Controller
{
    private readonly AppDbContext _context;
    public LibraryMembersController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.LibraryMembers.ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.LibraryMembers.Include(item => item.BorrowedBooks).FirstOrDefaultAsync(item => item.Id == id);
        return member == null ? NotFound() : View(member);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email")] LibraryMember member)
    {
        if (!ModelState.IsValid) return View(member);
        _context.Add(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.LibraryMembers.FindAsync(id);
        return member == null ? NotFound() : View(member);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] LibraryMember member)
    {
        if (id != member.Id) return NotFound();
        if (!ModelState.IsValid) return View(member);
        _context.Update(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.LibraryMembers.FirstOrDefaultAsync(item => item.Id == id);
        return member == null ? NotFound() : View(member);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var member = await _context.LibraryMembers.FindAsync(id);
        if (member != null) _context.LibraryMembers.Remove(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
