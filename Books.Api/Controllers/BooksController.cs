using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Entities;
using Books.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksRepository booksRepository;
        public BooksController(IBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }


        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
           var books = await this.booksRepository.GetBooksAsync();
            return Ok(books);
        }
        [HttpGet]
        [Route("GetBook")]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await this.booksRepository.GetBookAsync(id);
            return Ok(book);
        }

        [HttpGet]
        [Route("GetBooksSynchronus")]
        public IActionResult GetBooksSynchronus()
        {
            var books =  this.booksRepository.GetBooks();
            return Ok(books);
        }
        [HttpGet]
        [Route("GetBookSynchronus")]
        [Route("{id}")]
        public IActionResult GetBookSynchronus(Guid id)
        {
            var book =  this.booksRepository.GetBook(id);
            return Ok(book);
        }

        [HttpGet]
        [Route("GetBookFireAndForget")]
        [Route("{id}")]
        public IActionResult GetBookFireAndForget(Guid id)
        {
            var book = this.booksRepository.GetBook(id);
            Task.Factory.StartNew(() => FireandForget());           
            return Ok(book);
        }


        private async Task FireandForget()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            using (StreamWriter sw = System.IO.File.AppendText(@"D:\mystuff\WriteLines.txt"))
            {
                foreach (string line in lines)
                    sw.WriteLineAsync(line);
            }
           
          
           // System.Threading.Thread.Sleep(10000);
        }

    }
}