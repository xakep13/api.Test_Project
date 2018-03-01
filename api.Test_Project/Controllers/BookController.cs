using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Test_Project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Test_Project.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        BookContext db;
        public BookController(BookContext bookContext)
        {
            db = bookContext;
            if(!db.Books.Any())
            {
                db.Books.Add(new Book { Name = "Game of Thrones", Author = "Martin" });
                db.Books.Add(new Book { Name = "Harry Potter",    Author = "Rowling" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return db.Books.ToList();
        }

        // GET api/book/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // POST api/book
        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            db.Books.Add(book);
            db.SaveChanges();
            return Ok(book);
        }

        // PUT api/book/
        [HttpPut]
        public IActionResult Put([FromBody]Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            if (!db.Books.Any(x => x.Id == book.Id))
            {
                return NotFound();
            }

            db.Update(book);
            db.SaveChanges();
            return Ok(book);
        }

        // DELETE api/book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return Ok(book);
        }
    }
}
