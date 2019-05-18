using BookStore.Repository.Context;
using BookStore.Repository.Entities;
using BookStore.Repository.IRepository;
using BookStore.Repository.Mapper;
using BookStore.Repository.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Repository
{
    public class UserRepository:IRepository<UserDTO>, IAuthenticate<UserDTO>
    {
        readonly DataContext DataContext;
        IMapper<User, UserDTO> UserMapper;
        public UserRepository(DataContext _DataContext) { DataContext = _DataContext;
            UserMapper = new UserEntitiesMapper(); }
      
        public void Delete(UserDTO UserDTO)
        {
             User User =DataContext.Users.Where(U => U.Id == UserDTO.Id).FirstOrDefault();
            if (User != null)
            {
                DataContext.Users.Remove(User);
                DataContext.SaveChanges();
            }
        }

        public List<UserDTO> GetALL()
        {
            List<UserDTO> Users = new List<UserDTO>();
            foreach(User User in DataContext.Users.ToList())
            {
                Users.Add(UserMapper.MapToDTO(User));
            }

            return Users;
        }
        public UserDTO GetById(string id)
        {
            User User = DataContext.Users.Find(id);
            if (User != null)
            {
                return UserMapper.MapToDTO(User);

            }
            
            return null;

        }
        public void Insert(UserDTO UserDTO)
        {
       
            if (UserDTO != null)
            {
                User User = UserMapper.MapToDbEntity(UserDTO);
                #region Add Password Hash
                byte[] passwordHash, passwordSalt;
                PasswordAuthentication.CreatePasswordHash(UserDTO.Password, out passwordHash, out passwordSalt);
                User.PasswordHash = passwordHash;
                User.PasswordSalt = passwordSalt;
                #endregion
                
                DataContext.Add(User);
                DataContext.SaveChanges();
            }
        }//Regist
        public void Update(UserDTO UserDTO)
        {
            User User = DataContext.Users.Find(UserDTO.Id);

            if (User != null)
            {
                User.LastName = UserDTO.LastName;
                User.FirstName = UserDTO.FirstName;
                User.UserName = UserDTO.UserName;
                #region Add Password Hash
                byte[] passwordHash, passwordSalt;
                PasswordAuthentication.CreatePasswordHash(UserDTO.Password, out passwordHash, out passwordSalt);
                User.PasswordHash = passwordHash;
                User.PasswordSalt = passwordSalt;
                #endregion

                DataContext.Users.Update(User);
                DataContext.SaveChanges();
            }
        }
        public UserDTO AuthenticateUser(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User User = DataContext.Users.FirstOrDefault(x => x.UserName == username);
                if (User != null)
                    if (PasswordAuthentication.VerifyPasswordHash(password, User.PasswordHash, User.PasswordSalt))      // check if password is correct
                    {
                        return UserMapper.MapToDTO(User);
                    }
            }
            return null;

        }
       public void AddToList(string Id, object Obj)
        {
            UserDTO UserDTO = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(Obj));
            if (UserDTO != null)
            {
                User User = DataContext.Users.Find(UserDTO.Id);
                Book Book = DataContext.Books.Find(Id);
                if (Book != null&& User!=null)
                {
                    Book.Users.Add(User);
                    DataContext.SaveChanges();
                }
            }
        }

    }
}
