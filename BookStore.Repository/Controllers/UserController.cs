using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repository.IRepository;
using BookStore.Repository.Entities;
using BookStore.Repository.Repository;
using System.Net.Http;
using System.Net;

namespace BookStore.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // new UserRepository(new Context.DataContext(new Microsoft.EntityFrameworkCore.DbContextOptions<Context.DataContext>()));

        readonly IRepository<UserDTO> UserRepo;
        readonly IAuthenticate<UserDTO> UserAuth;
        public UserController(
        IRepository<UserDTO> _UserRepo, IAuthenticate<UserDTO> _UserAuth)
        {
            UserRepo = _UserRepo;
            UserAuth = _UserAuth;
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody] UserDTO UserDTO)
        {
            if (string.IsNullOrEmpty(UserDTO.Password) || string.IsNullOrEmpty(UserDTO.UserName))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            UserDTO User = UserAuth.AuthenticateUser(UserDTO.UserName, UserDTO.Password);
            if (User != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        [HttpPost]
        public HttpResponseMessage Register([FromBody] UserDTO UserDTO)
        {
            if (string.IsNullOrEmpty(UserDTO.Password) || string.IsNullOrEmpty(UserDTO.UserName) || string.IsNullOrEmpty(UserDTO.FirstName))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (UserRepo.GetById(UserDTO.Id) == null)
            {
                UserRepo.Insert(UserDTO);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else return new HttpResponseMessage(HttpStatusCode.Found);

        }
    }
}
