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
    public class BookBorrowsInitTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BookBorrowsInitTests()
        {
            _server = ServerFactory.GetServerInstance();
            _client = _server.CreateClient();

            using (var scope= _server.Host.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                _db.BookBorrow.Add(new BookBorrow
                {
                    IdBookBorrow=6,
                    IdBook=2,
                    IdUser=2,
                    BorrowDate=new DateTime(),
                    Comments="jajko czy kura"
                });

                _db.SaveChanges();
            }
        }

        [Fact] 
        public async Task AddBookBorrow_201Created()
        {
            var newBookBorrow = new BookBorrowDto
            {
                IdUser = 5,
                IdBook = 5,
                Comment = "sialala"
            };
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(newBookBorrow), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows", stringContent);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();            var responseUser = JsonConvert.DeserializeObject<BookBorrow>(content);            Assert.True(responseUser.Comments == "sialala");
        }

        [Fact] 
        public async Task UpdateBookBorrow_200Ok()
        {
            var updateBookBorrow = new UpdateBookBorrowDto
            {
                IdUser = 5,
                IdBook = 5,
                IdBookBorrow = 6,
                DateFrom = new DateTime(),
                Comments = "sialala_zmiana"
            };

            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(updateBookBorrow), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows/" + updateBookBorrow.IdBookBorrow, stringContent);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();            var responseBook = JsonConvert.DeserializeObject<Boolean>(content);            Assert.True(responseBook);
        }




    }
}
