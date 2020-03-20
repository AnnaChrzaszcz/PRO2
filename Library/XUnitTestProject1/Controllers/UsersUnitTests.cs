using Library.Controllers;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Library.Models.DTO;
using Microsoft.AspNet.Identity;
using Library.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace XUnitTestProject1.Controllers
{
    public class UsersUnitTests
    {
        [Fact]
        public async Task GetUsers_200Ok()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();
            ICollection<User> users = new List<User>
            {
                new User{IdUser=1, Name="kowalski", Email="kowalski@wp.pl"},
                new User{IdUser=2, Name="kowalski2", Email="kowalski2@wp.pl"},
                new User{IdUser=3, Name="kowalski3", Email="kowalski3@wp.pl"}
            };
            m.Setup(c => c.GetUsers()).Returns(Task.FromResult(users));
            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.GetUsers();

            //Assert
            Assert.True(result is OkObjectResult);
            var r = result as OkObjectResult;
            Assert.True((r.Value as ICollection<User>).Count == 3);
            Assert.True((r.Value as ICollection<User>).ElementAt(0).Name == "kowalski");
        }

        [Fact] 
        public async Task AddUser_201CreatedAtRoute()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();
            UserDto userToAdd = new UserDto{Name = "Rafałek", Surname="Jajko", Login="RafalJajko", Email = "Rafalek@wp.pl", Password="szparagiSAfajne" };
            User newUser = new User
            {
                Name = userToAdd.Name,
                Email = userToAdd.Email,
                Login = userToAdd.Login,
                Surname = userToAdd.Surname,
                Password = new PasswordHasher().HashPassword(userToAdd.Password),
                IdUserRoleDict = (int)UserRoleHelper.UserRolesEnum.Reader
            };
            m.Setup(c => c.AddUser(userToAdd)).Returns(Task.FromResult(newUser));
            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.AddUser(userToAdd);

            //Assert
            Assert.True(result is CreatedAtRouteResult);
            var r = result as CreatedAtRouteResult;
            Assert.True((r.Value as User).Login == "RafalJajko");
        }



        [Fact]
        public async Task GetUser_200Ok()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();
            var IdUser = 2;
            ICollection<User> users = new List<User>
            {
                new User{IdUser=1, Name="kowalski", Email="kowalski@wp.pl"},
                new User{IdUser=2, Name="kowalski2", Email="kowalski2@wp.pl"},
                new User{IdUser=3, Name="kowalski3", Email="kowalski3@wp.pl"}
            };
            m.Setup(c => c.GetUser(IdUser)).Returns(Task.FromResult(users.ElementAt(1)));
            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.GetUser(IdUser);

            //Assert
            Assert.True(result is OkObjectResult);
            var r = result as OkObjectResult;
            Assert.True((r.Value as User).Name == "kowalski2");
        }

    }
}
