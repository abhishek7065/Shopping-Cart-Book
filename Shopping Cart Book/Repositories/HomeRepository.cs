using Microsoft.EntityFrameworkCore;

public class HomeRepository : IHomerepository
{
    private readonly ApplicationDbContext _db;

    public HomeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
    {
        sTerm = sTerm.ToLower();

        var booksQuery = from book in _db.Books
                         join genre in _db.Genres
                         on book.GenreId equals genre.Id
                         where (string.IsNullOrWhiteSpace(sTerm) || book.BookName.ToLower().StartsWith(sTerm))
                               && (genreId == 0 || book.GenreId == genreId)
                         select new Book
                         {
                             Id = book.Id,
                             Image = book.Image,
                             AuthorName = book.AuthorName,
                             BookName = book.BookName,
                             Genre = book.Genre,
                             GenreId = book.GenreId,
                             Price = book.Price,
                             GenreName = book.Genre.GenreName,
                         };

        return await booksQuery.ToListAsync();
    }
    public async Task<IEnumerable<Genre>> Genres()
    {
        return await _db.Genres.ToListAsync();
    }

}
