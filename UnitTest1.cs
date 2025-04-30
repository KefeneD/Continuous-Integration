using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab5Blazor.Models;
using Lab5Blazor.Services;
using System.Collections.Generic;
using System.Linq;

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
        public void EditBook(Book book)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                SaveBooks();
            }
        }

        public void DeleteBook(int bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                Books.Remove(book);
                SaveBooks();
            }
        }

        public async void SaveBooks()
        {
            using (var writer = new StreamWriter(booksFilePath))
            {
                foreach (Book book in Books)
                {
                    string str = $"{book.Id}, {book.Title}, {book.Author}, {book.ISBN}";
                    await writer.WriteLineAsync(str);
                }
            }
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveUsers();
        }

        public void EditUser(User user)
        {
            var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                SaveUsers();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                Users.Remove(user);
            }
            SaveUsers();
        }

        public async void SaveUsers()
        {
            using (var writer = new StreamWriter(usersFilePath))
            {
                foreach (User user in Users)
                {
                    string str = $"{user.Id}, {user.Name}, {user.Email}";
                    await writer.WriteLineAsync(str);
                }
            }
        }

        public void BorrowBook(User user, Book book)
        {
            if (!BorrowedBooks.ContainsKey(user))
                BorrowedBooks[user] = new List<Book>();

            BorrowedBooks[user].Add(book);
        }

        public void ReturnBook(User user, Book book)
        {
            if (BorrowedBooks.ContainsKey(user))
            {
                BorrowedBooks[user].Remove(book);
            }
        }
    }

    public interface ILibraryService
    {
        List<Book> Books { get; set; }
        List<User> Users { get; set; }
        Dictionary<User, List<Book>> BorrowedBooks { get; set; }

        void ReadBooks();
        void ReadUsers();
        void AddBook(Book book);
        void EditBook(Book book);
        void DeleteBook(int bookId);
        void SaveBooks();
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int userId);
        void SaveUsers();
        void BorrowBook(User user, Book book);
        void ReturnBook(User user, Book book);
    }
}
        public void EditBook(Book book)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                SaveBooks();
            }
        }

        public void DeleteBook(int bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                Books.Remove(book);
                SaveBooks();
            }
        }

        public async void SaveBooks()
        {
            using (var writer = new StreamWriter(booksFilePath))
            {
                foreach (Book book in Books)
                {
                    string str = $"{book.Id}, {book.Title}, {book.Author}, {book.ISBN}";
                    await writer.WriteLineAsync(str);
                }
            }
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveUsers();
        }

        public void EditUser(User user)
        {
            var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                SaveUsers();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                Users.Remove(user);
            }
            SaveUsers();
        }

        public async void SaveUsers()
        {
            using (var writer = new StreamWriter(usersFilePath))
            {
                foreach (User user in Users)
                {
                    string str = $"{user.Id}, {user.Name}, {user.Email}";
                    await writer.WriteLineAsync(str);
                }
            }
        }

        public void BorrowBook(User user, Book book)
        {
            if (!BorrowedBooks.ContainsKey(user))
                BorrowedBooks[user] = new List<Book>();

            BorrowedBooks[user].Add(book);
        }

        public void ReturnBook(User user, Book book)
        {
            if (BorrowedBooks.ContainsKey(user))
            {
                BorrowedBooks[user].Remove(book);
            }
        }
    }

    public interface ILibraryService
    {
        List<Book> Books { get; set; }
        List<User> Users { get; set; }
        Dictionary<User, List<Book>> BorrowedBooks { get; set; }

        void ReadBooks();
        void ReadUsers();
        void AddBook(Book book);
        void EditBook(Book book);
        void DeleteBook(int bookId);
        void SaveBooks();
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int userId);
        void SaveUsers();
        void BorrowBook(User user, Book book);
        void ReturnBook(User user, Book book);
    }
}

// retry after fix
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
            Assert.AreEqual("Test Author", addedBook.Author);
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
    }
}
