namespace mvc_project.Models;

public class LibraryMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Book> BorrowedBooks { get; set; } = new List<Book>();
}
