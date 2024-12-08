namespace Shopping_Cart_Book.Repositories
{
    public interface IHomerepository
    {
        Task<IEnumerable<Book>> GetBooks(string sTerm = "A", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}
