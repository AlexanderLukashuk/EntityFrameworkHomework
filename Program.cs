using System;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Library.Data;
using Library.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var connectionString = configuration.GetSection("connectionStrings")["library"];

            bool result;
            int bookCount = 0;
            int userCount = 0;

            using (ApplicationContext context = new ApplicationContext(connectionString))
            {
                User user1 = new User
                {
                    Login = "Buzan",
                    Password = "Pavel123"
                };
                Profile profile1 = new Profile
                {
                    FullName = "Симоненко Павел",
                    User = user1
                };
                User user2 = new User
                {
                    Login = "Drelanius",
                    Password = "12344321"
                };
                Profile profile2 = new Profile
                {
                    FullName = "Правый Йосеф",
                    User = user2
                };
                User user3 = new User
                {
                    Login = "Fordregelv",
                    Password = "qwerty1234"
                };
                Profile profile3 = new Profile
                {
                    FullName = "Толочко Богдан",
                    User = user3
                };

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Profiles.Add(profile1);
                context.Profiles.Add(profile2);
                context.Profiles.Add(profile3);
                context.SaveChanges();

                Author stephenKing = new Author
                {
                    Name = "Стивен Кинг"
                };
                Author marioPuzo = new Author
                {
                    Name = "Марио Пьюзо"
                };
                Author danielKeyes = new Author
                {
                    Name = "Дэниел Киз"
                };
                Author arthurConanDoyle = new Author
                {
                    Name = "Артур Конан Дойл"
                };
                context.Authors.Add(stephenKing);
                context.Authors.Add(marioPuzo);
                context.Authors.Add(danielKeyes);
                context.Authors.Add(arthurConanDoyle);
                context.SaveChanges();
                
                Book greenMile = new Book
                {
                    Name = "Зеленая миля",
                    ReturnDate = new DateTime(2020, 08, 27),
                    Author = stephenKing
                };
                Book longWalk = new Book
                {
                    Name = "Долгая прогулка",
                    ReturnDate = new DateTime(2020, 10, 27),
                    Author = stephenKing
                };
                Book petCemetery = new Book
                {
                    Name = "Кладбище домашних животных",
                    ReturnDate = new DateTime(2020, 10, 13),
                    Author = stephenKing
                };
                Book godfather = new Book
                {
                    Name = "Крестный отец",
                    ReturnDate = new DateTime(2020, 09, 11),
                    Author = marioPuzo
                };
                Book flowersForAlgernon = new Book
                {
                    Name = "Цветы для Элджернона",
                    ReturnDate = new DateTime(2021, 01, 11),
                    Author = danielKeyes
                };
                Book adventuresOfSherlockHolmes = new Book
                {
                    Name = "Приключения Шерлока Холмса",
                    ReturnDate = new DateTime(2020, 12, 08),
                    Author = danielKeyes
                };
                context.Books.Add(greenMile);
                context.Books.Add(longWalk);
                context.Books.Add(petCemetery);
                context.Books.Add(godfather);
                context.Books.Add(flowersForAlgernon);
                context.Books.Add(adventuresOfSherlockHolmes);

                stephenKing.Books.Add(greenMile);
                stephenKing.Books.Add(longWalk);
                stephenKing.Books.Add(petCemetery);
                marioPuzo.Books.Add(godfather);
                danielKeyes.Books.Add(flowersForAlgernon);
                arthurConanDoyle.Books.Add(adventuresOfSherlockHolmes);
                context.SaveChanges();
                //greenMile.Authors.Add(stephenKing);
                //longWalk.Authors.Add(stephenKing);
                //petCemetery.Authors.Add(stephenKing);

                profile1.Books.Add(greenMile);
                greenMile.Profile = profile1;
                profile2.Books.Add(petCemetery);
                profile2.Books.Add(godfather);
                godfather.Profile = profile2;
                petCemetery.Profile = profile2;
                profile3.Books.Add(longWalk);
                longWalk.Profile = profile3;

                foreach (var user in context.Profiles)
                {
                    result = user.CheckDebtor();
                    if (result)
                    {
                        Console.WriteLine($"{user.FullName} - должник");
                    }
                }

                foreach (var book in context.Books)
                {
                    bookCount++;
                    if (book.Author != null && bookCount == 3)
                    {
                        Console.WriteLine($"{book.Author.Name} - автор книги \"{book.Name}\"");
                    }
                }
                bookCount = 0;

                foreach (var book in context.Books)
                {
                    if (book.Profile == null)
                    {
                        Console.WriteLine($"Книга {book.Name} доступна для прочтения");
                    }
                }

                foreach (var user in context.Profiles)
                {
                    userCount++;
                    if (userCount == 2)
                    {
                        Console.WriteLine("У пользователя №2 сейчас на руках книги:");
                        foreach (var book in user.Books)
                        {
                            Console.WriteLine(book.Name);
                        }
                    }
                }
                userCount = 0;

                foreach (var book in context.Books)
                {
                    book.DebtСancellation();
                }

                foreach (var user in context.Profiles)
                {
                    result = user.CheckDebtor();
                    if (result)
                    {
                        Console.WriteLine($"{user.FullName} - должник");
                    }
                }
            }
        }
    }
}
