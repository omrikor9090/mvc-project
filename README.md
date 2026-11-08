# Library Project

ASP.NET Core MVC application for managing library information.

## Main parts

- `Models` contains `Book`, `Author`, `Category`, and `LibraryMember`.
- `Data/AppDbContext.cs` connects the models to SQL Server LocalDB.
- `Controllers` contains CRUD actions for all four tables.
- `Views` contains Index, Create, Edit, Details, and Delete pages.

## Relationships

- One author can have many books (`Book.AuthorId`).
- One category can contain many books (`Book.CategoryId`).
- One library member can borrow many books (`Book.LibraryMemberId`).

The Books controller uses `Include` so the Index and Details pages show the related names.

## Running the project

1. Open `mvc project.slnx` in Visual Studio.
2. Build the solution.
3. In Package Manager Console, choose the project and run `Update-Database` if the database is not already created.
4. Run with Ctrl+F5.
5. Create an author, category, and member before creating a book. Use their Id values in the Book form.

The connection string creates a local SQL Server database named `LibraryMvcProjectDb`.



