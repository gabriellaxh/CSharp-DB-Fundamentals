namespace BookShop
{
    using System;
    using System.Linq;
    using BookShop.Data;
    using BookShop.Models;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
               
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int age = -1;

            switch (command.ToLower())
            {
                case "minor":
                    age = 0;
                    break;
                case "teen":
                    age = 1;
                    break;
                case "adult":
                    age = 2;
                    break;
            }

            var titles = context.Books
                .Where(b => (int)b.AgeRestriction == age)
                .Select(b => b.Title)
                .OrderBy(x => x)
                .ToList();

            var result = string.Join(Environment.NewLine, titles);
            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context.Books
                .Where(b => b.EditionType == EditionType.Gold &&
                b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, titles);
            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)

        {
            var titles = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .ToList();

            var prices = context.Books.Select(x => x.Price).ToList();
            var result = string.Join(Environment.NewLine, titles.Select(x => $"{x.Title} - ${x.Price:f2}"));
            return result;

        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var titles = context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title);

            var result = string.Join(Environment.NewLine, titles);
            return result;
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToArray();

            var titles = context.Books
                .Where(x => x.BookCategories
                .Any(s => categories.Contains(s.Category.Name.ToLower())))
                .Select(s => s.Title)
                .OrderBy(t => t)
                .ToList();

            string result = string.Join(Environment.NewLine, titles);

            return result;


        }

        public static string GetBooksReleasedBefore(BookShopContext context, string input)
        {
            string format = "dd-MM-yyyy";
            var date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
            var titles = context.Books
                .Where(x => x.ReleaseDate.Value < date)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new
                {
                    x.Title,
                    x.Price,
                    x.EditionType
                })
               .ToList();

            var result = string.Join(Environment.NewLine, titles.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
            return result;
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var pattern = $@"\b.*{input}\b";

            var authors = context.Authors
                .Where(z => Regex.Match(z.FirstName, pattern).Success)
                                                             .Select(z => z.FirstName + " " + z.LastName)
                .OrderBy(x => x)
                .ToList();

            var result = string.Join(Environment.NewLine, authors);
            return result;

        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string pattern = $@"^.*{input}.*$";
            var titles = context.Books
                .Where(b => Regex.IsMatch(b.Title, pattern, RegexOptions.IgnoreCase))
                .Select(b => b.Title)
                .OrderBy(x => x)
                .ToList();

            return string.Join(Environment.NewLine, titles);
           
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var pattern = $@"^{input}.*$";
            var titles = context.Books
                .Where(z => Regex.IsMatch(z.Author.LastName, pattern, RegexOptions.IgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(z => new
                {
                    z.Title,
                    author = z.Author.FirstName + " " + z.Author.LastName
                })
                .ToList();

            var result = string.Join(Environment.NewLine, titles.Select(x => $"{x.Title} ({x.author})"));
            return result;
        }

        public static int CountBooks(BookShopContext context, int len)
        {
            var num = context.Books
                .Where(x => x.Title.Length > len)
                .Count();
            
            return num;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copies = context.Books
                .Select(x => new
                {
                    AuthorName = x.Author.FirstName + " " + x.Author.LastName,
                    Copies = x.Copies
                })
                .GroupBy(a => a.AuthorName)
                .OrderByDescending(z => z.Sum(x => x.Copies))
                .ToList();

            var result = string.Join(Environment.NewLine, copies.Select(x => x.Key + " - " + x.Sum(b => b.Copies)));
            return result;



        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    profit = x.CategoryBooks.Sum(t => t.Book.Copies * t.Book.Price)
                })
                .OrderByDescending(x => x.profit)
                .ThenBy(x => x.Name);

            return string.Join(Environment.NewLine, categories.Select(x => $"{x.Name} ${x.profit:f2}"));

        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    Name = x.Name,
                    Count = x.CategoryBooks.Count(),
                    CategoryBooks = x.CategoryBooks
                                       .OrderByDescending(a => a.Book.ReleaseDate.Value)
                                       .Take(3)
                                       .Select(s => new
                                       {
                                           s.Book.Title,
                                           s.Book.ReleaseDate.Value.Year
                                       })
                                    .OrderByDescending(z => z.Year)
                })
                .OrderBy(x => x.Name)
                .ToList();

            return string.Join(Environment.NewLine, categories.Select(x => $"--{x.Name}"
            + Environment.NewLine + string.Join(Environment.NewLine, x.CategoryBooks.Select(y => $"{y.Title} ({y.Year})"))));

        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var toDel = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();

            foreach (var book in toDel)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return toDel.Count();
        }
    }
}
