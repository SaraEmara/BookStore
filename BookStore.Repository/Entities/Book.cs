using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [MaxLength(25)]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Isbn13 { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }

        public List<User> Users { get; set; }
        public Book()
        {
            Users = new List<User>();
        }
    }
}
