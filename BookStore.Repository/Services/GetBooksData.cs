using BookStore.Repository.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Services
{
    public static class GetBooksData
    {
        public static List<Book> IntializeBooks() {
            string json = File.ReadAllText("books-data.json");
         return  JsonConvert.DeserializeObject<List<Book>>(json);
        }


    }
}
