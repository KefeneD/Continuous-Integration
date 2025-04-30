using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab5Blazor.Models;
using Lab5Blazor.Services;
using System.Linq;
using System.Collections.Generic;

namespace Lab5Blazor.Tests
{
    [TestClass]
    public class LibraryServiceTests
    {
        private LibraryService _libraryService;

        [TestInitialize]
        public void Setup()
        {
            _libraryService = new LibraryService();
        }

        [TestMethod]
        public void AddBook_ShouldAddBook_WhenBookIsValid()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Author = "Test Author",
                ISBN = "1234567890"
            };

            _libraryService.AddBook(book);
            var addedBook = _libraryService.Books.FirstOrDefault(b => b.Id == book.Id);

            Assert.IsNotNull(addedBook);
            Assert.AreEqual("Test Book", addedBook.Title);
            _libraryService.DeleteBook(book.Id);
        }

        [TestMethod]
        public void EditBook_ShouldUpdateBook_WhenBookExists()
        {
            var book = new Book { Id = 100, Title = "Old Title", Author = "Author", ISBN = "123" };
            _libraryService.AddBook(book);

            book.Title = "New Title";
            _libraryService.EditBook(book);

            var updatedBook = _libraryService.Books.FirstOrDefault(b => b.Id == 100);
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("New Title", updatedBook.Title);
            _libraryService.DeleteBook(book.Id);
        }

        [TestMethod]
        public void DeleteBook_ShouldRemoveBook_WhenBookExists()
        {
            var book = new Book { Id = 101, Title = "Delete Me", Author = "Author", ISBN = "456" };
            _libraryService.AddBook(book);
            _libraryService.DeleteBook(book.Id);

            var deletedBook = _libraryService.Books.FirstOrDefault(b => b.Id == book.Id);
            Assert.IsNull(deletedBook);
        }

        [TestMethod]
        public void BorrowBook_ShouldAddBookToUser_WhenBookIsBorrowed()
        {
            var user = new User { Id = 1, Name = "User 1", Email = "user1@example.com" };
            var book = new Book { Id = 200, Title = "Borrow Me", Author = "Author", ISBN = "789" };

            _libraryService.AddUser(user);
            _libraryService.AddBook(book);
            _libraryService.BorrowBook(user, book);

            var borrowed = _libraryService.BorrowedBooks.GetValueOrDefault(user);
            Assert.IsNotNull(borrowed);
            Assert.IsTrue(borrowed.Any(b => b.Title == "Borrow Me"));

            _libraryService.DeleteBook(book.Id);
            _libraryService.DeleteUser(user.Id);
        }

        [TestMethod]
        public void ReturnBook_ShouldRemoveBookFromUser_WhenBookIsReturned()
        {
            var user = new User { Id = 2, Name = "User 2", Email = "user2@example.com" };
            var book = new Book { Id = 201, Title = "Return Me", Author = "Author", ISBN = "789" };

            _libraryService.AddUser(user);
            _libraryService.AddBook(book);
            _libraryService.BorrowBook(user, book);

            _libraryService.ReturnBook(user, book);
            var borrowed = _libraryService.BorrowedBooks.GetValueOrDefault(user);

            Assert.IsTrue(borrowed == null || !borrowed.Any(b => b.Id == book.Id));

            _libraryService.DeleteBook(book.Id);
            _libraryService.DeleteUser(user.Id);
        }

        [TestMethod]
        public void TriggerFailure_TestFailsOnPurpose()
        {
            // This test will fail intentionally for Lab 7 CI workflow
            Assert.AreEqual(1, 2);
        }
    }
}
