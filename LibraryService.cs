using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab5Blazor.Models;

namespace Lab5Blazor.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly string booksFilePath = "Books.csv";
        private readonly string usersFilePath = "Users.csv";

        public List<Book> Books { get; set; } = new List<Book>();
        public List<User> Users { get; set; } = new List<User>();
        public Dictionary<User, List<Book>> BorrowedBooks { get; set; } = new Dictionary<User, List<Book>>();

        public LibraryService()
        {
            ReadBooks();
            ReadUsers();
        }

        public void ReadBooks()
        {
            Books.Clear();
            if (File.Exists(booksFilePath))
            {
                var lines = File.ReadLines(booksFilePath);
                foreach (var line in lines)
                {
                    var fields = line.Split(',');
                    if (fields.Length >= 4)
                    {
                        Books.Add(new Book
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Title = fields[1].Trim(),
                            Author = fields[2].Trim(),
                            ISBN = fields[3].Trim()
                        });
                    }
                }
            }
        }

        public void ReadUsers()
        {
            Users.Clear();
            if (File.Exists(usersFilePath))
            {
                var lines = File.ReadLines(usersFilePath);
                foreach (var line in lines)
                {
                    var fields = line.Split(',');
                    if (fields.Length >= 3)
                    {
                        Users.Add(new User
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Name = fields[1].Trim(),
                            Email = fields[2].Trim()
                        });
                    }
                }
            }
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
            SaveBooks();
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
