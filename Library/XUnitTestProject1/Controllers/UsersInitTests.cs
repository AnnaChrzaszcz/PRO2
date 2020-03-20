using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Entities;
using Library.Models.DTO;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTestProject1.Controllers
{
    public class UsersInitTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public UsersInitTests()
        {
            _server = ServerFactory.GetServerInstance();
            _client = _server.CreateClient();


            using (var scope = _server.Host.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                _db.User.Add(new User
                {
                    IdUser = 1,
                    Email = "jd@pja.edu.pl",
                    Name = "Daniel",
                    Surname = "Jabłoński",
                    Login = "jd",
                    Password = "ASNDKWQOJRJOP!JO@JOP"
                });

                _db.SaveChanges();
            }
        }


        [Fact]
        public async Task GetUsers_200Ok()
        {
            //Arrange i Act
            var httpResponse = await _client.GetAsync($"{_client.BaseAddress.AbsoluteUri}api/users");

            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(content);
         
            Assert.True(users.Count() == 1);
            Assert.True(users.ElementAt(0).Login == "jd");
        }

        [Fact]
        public async Task GetUser_200Ok()
        {
            //Arrange i Act
            var httpResponse = await _client.GetAsync($"{_client.BaseAddress.AbsoluteUri}api/users/1");

            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
       
            Assert.True(user.Login == "jd");
        }

        [Fact]         public async Task AddUser_201Created()        {
            //Arrange i Act
            UserDto userToAdd = new UserDto            {                IdUser = 3,                Email = "toty@pja.edu.pl",                Name = "To",                Surname = "Ty",                Login = "pd",                Password = "maslo1234"            };                  HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(userToAdd), Encoding.UTF8, "application/json");            var httpResponse = await _client.PostAsync($"{_client.BaseAddress.AbsoluteUri}api/users", stringContent);            httpResponse.EnsureSuccessStatusCode();                       var content = await httpResponse.Content.ReadAsStringAsync();            var responseUser = JsonConvert.DeserializeObject<User>(content);            Assert.True(responseUser.Login == "pd");                   }
 


    }
}
