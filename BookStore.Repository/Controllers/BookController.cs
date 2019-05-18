using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository.Entities;
using BookStore.Repository.IRepository;
using BookStore.Repository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        readonly IRepository<Book> BookRepo;
        readonly IRepository<UserDTO> UserRepo;
        public BookController(IRepository<Book> _BookRepo, IRepository<UserDTO> _UserRepo) {
            UserRepo = _UserRepo; BookRepo = _BookRepo; }
        // GET: api/Book
        [HttpGet]
        public List<Book> ListAllBooks()
        {
            return BookRepo.GetALL();
        }
        
        [HttpGet]
        public List<Book> SearchBookByName([FromQuery]string BookTitle)
        {
            return BookRepo.GetALL().Where(b => b.Title == BookTitle).DefaultIfEmpty().ToList();
        }
        
        // POST: api/Book
        [HttpPost]
        public void AddBookToUserFavorite([FromBody]UserBook UserBook)
        {
            if (UserBook.BookId != null&& UserBook.UserId != null)
                BookRepo.AddToList(UserBook.UserId, BookRepo.GetById(UserBook.BookId));
        }
        [HttpGet]
        public List<Book> ListFavorit(string UserId)
        {
            return UserRepo.GetById(UserId).FavoriteBooks;
        }
    }
}
