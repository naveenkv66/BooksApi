using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Entities;

namespace Books.Api.Services
{
    public interface IBooksRepository
    {
        Task<Book> GetBookAsync(Guid Id);
        Task<IEnumerable<Book>> GetBooksAsync();

        Book GetBook(Guid Id);
        IEnumerable<Book> GetBooks();

    }
}
