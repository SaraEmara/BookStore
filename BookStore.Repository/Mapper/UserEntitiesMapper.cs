using BookStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Mapper
{
    public class UserEntitiesMapper : IMapper<User, UserDTO>
    {
        public User MapToDbEntity(UserDTO User)
        {
            if (User != null)
            {
                return new User()
                {
                    FirstName=User.FirstName,
                    LastName=User.LastName,
                    UserName=User.LastName,
                    Id=User.Id,
                    FavoriteBooks=User.FavoriteBooks
                    
                };
            }
            return null;
        }

        public UserDTO MapToDTO(User User)
        {
            if (User != null)
            {
                return new UserDTO()
                {
                    LastName = User.LastName,
                    UserName = User.UserName,
                    FirstName = User.FirstName,
                    Id = User.Id,
                    FavoriteBooks = User.FavoriteBooks

                };
            }
            return null;
        }
    }
}
