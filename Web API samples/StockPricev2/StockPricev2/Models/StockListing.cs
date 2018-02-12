// a StockListing i.e. ticker and price
// and database context for stock (code first)

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace StockPrice2.Models
{
    // a listing for a stock on the stock market
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        [Required(ErrorMessage = "Invalid Ticker Symbol")]                           // not null or empty string, not enforced automatically
        [Key]                                                                        // Primary Key
        public string TickerSymbol
        {
            get;
            set;
        }

        // price last trade in $
        [Range(0.00001, Double.MaxValue)]
        public double Price
        {
            get;
            set;
        }
    }

    // context for connection to database cf connection string in web.config
    public class StockContext : DbContext             
    {
        public DbSet<StockListing> StockListings { get; set; }
    }
}