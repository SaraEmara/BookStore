using BookStore.Repository.Context;
using BookStore.Repository.Entities;
using BookStore.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Repository
{
    public class BookRepository : IRepository<Book>
    {
        readonly DataContext DataContext;
        public BookRepository(DataContext _DataContext) { DataContext = _DataContext; }


        public void Delete(Book Book)
        {
            if (Book != null)
            {
                DataContext.Books.Remove(Book);
                DataContext.SaveChanges();
            }
        }

        public List<Book> GetALL()
        {
            List<Book> Books = DataContext.Books.ToList();

            return Books;
        }
        public Book GetById(string id)
        {
            return DataContext.Books.Find(id);

        }
        public void Insert(Book Book)
        {
            if (Book != null)
            {
                DataContext.Add(Book);
                DataContext.SaveChanges();
            }
        }
        public void Update(Book Book)
        {
            if (Book != null)
            {
                DataContext.Books.Update(Book);
                DataContext.SaveChanges();
            }
        }
        public void AddToList(string Id, object Obj)
        {
            Book Book = JsonConvert.DeserializeObject<Book>(JsonConvert.SerializeObject(Obj));
            if (Book != null)
            {
                User User = DataContext.Users.Find(Id);
                if (User != null)
                {
                    User.FavoriteBooks.Add(Book);
                    DataContext.SaveChanges();
                }
            }
        }

    }
}
