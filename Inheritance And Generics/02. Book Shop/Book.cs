using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class Book
{
    private string author { get; set; }
    private string title { get; set; }
    private decimal price { get; set; }


    public string Author
    {
        get
        {
            return this.author;
        }
        set
        {
            var splitedName = value.Split();
            string sureName = " ";

            if (splitedName.Length == 2)
            {
                sureName = splitedName[1];
            }

            if (char.IsDigit(sureName[0]))
                throw new ArgumentException("Author not valid!");
            else
                this.author = value;
        }
    }

    public string Title
    {
        get
        {
            return this.title;
        }
        set
        {
            if (value.Count() >= 3)
            {
                this.title = value;
            }
            else
                throw new ArgumentException("Title not valid!");
        }
    }

    public virtual decimal Price
    {
        get
        {
            return this.price;
        }
        set
        {
            if (value > 0)
            {
                this.price = value;
            }
            else
                throw new ArgumentException("Price not valid!");

            price = decimal.Round(value, 2);
        }
    }

    public Book(string author, string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name}" + Environment.NewLine +
            $"Title: {this.Title}" + Environment.NewLine +
            $"Author: {this.Author}" + Environment.NewLine +
            $"Price: {this.Price:f2}";
    }

}

