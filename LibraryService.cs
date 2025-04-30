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
            var existingBook = Books.FirstOrDefault(b =>
