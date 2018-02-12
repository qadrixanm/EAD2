// model class in service

namespace StockPrice.Models                         // dictates XML namespace, must match in service
{
    // a listing for a stock on the stock market
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        public string TickerSymbol
        {
            get;
            set;
        }

        // price last trade
        public double Price
        {
            get;
            set;
        }
    }
}
