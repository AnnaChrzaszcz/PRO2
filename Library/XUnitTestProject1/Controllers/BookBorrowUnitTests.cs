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
    public class BookBorrowUnitTests
    {
        [Fact]
        public async Task AddBookBorrow_201CreatedAtRoute()
        {
            //AAA
            //Arrange
            var m = new Mock<IBookBorrowRepository>();
            BookBorrowDto borrow = new BookBorrowDto {IdUser=1, IdBook=1, Comment="warzywka owocki" };
            BookBorrow newBookBorrow = new BookBorrow
            {
                IdUser = borrow.IdUser,
                IdBook = borrow.IdBook,
                BorrowDate = DateTime.Now,
                Comments = borrow.Comment
            };

            m.Setup(c => c.AddBookBorrow(borrow)).Returns(Task.FromResult(newBookBorrow));
            var controller = new BookBorrowsController(m.Object);

            //Act
            var result = await controller.AddBookBorrow(borrow);
            //Assert
            Assert.True(result is CreatedAtRouteResult);
            var r = result as CreatedAtRouteResult;
            Assert.True((r.Value as BookBorrow).Comments == "warzywka owocki");
        }


        [Fact] 
        public async Task UpdateBookBorrow_204NoContent()
        {
            //AAA
            //Arrange
            var m = new Mock<IBookBorrowRepository>();
            UpdateBookBorrowDto updatedBook = new UpdateBookBorrowDto {IdBookBorrow=1, IdBook=1, IdUser=1, Comments="ziemniaki", DateFrom=new DateTime()};
           
            m.Setup(c => c.ChangeBookBorrow(updatedBook)).Returns(Task.FromResult(true));
            var controller = new BookBorrowsController(m.Object);

            //Act
            var result = await controller.UpdateBookBorrow(updatedBook);
            //Assert
            Assert.True(result is OkResult);
            var r = result as OkResult;
        }






    }
}
