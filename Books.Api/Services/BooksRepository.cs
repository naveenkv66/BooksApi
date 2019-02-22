using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Context;
using Books.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext context;
        public BooksRepository(BooksContext context)
        {
            this.context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Book> GetBookAsync(Guid Id)
        {
            return await context.Books.Include("Author").FirstOrDefaultAsync(item=>item.Id == Id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:02';");
            return await context.Books.Include("Author").ToListAsync();             
        }


        public Book GetBook(Guid Id)
        {
            return context.Books.Include("Author").FirstOrDefault(item => item.Id == Id);
        }

        public IEnumerable<Book> GetBooks()
        {
            context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:02';");
            return context.Books.Include("Author").ToList();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }

       
    }
}
