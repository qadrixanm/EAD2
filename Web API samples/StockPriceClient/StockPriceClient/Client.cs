// client for StockPrice RESTful web service

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StockPriceClient
{
    // test
    class Client
    {
        // RunAsync awaits results from async methods so must be async itself
        static async Task RunAsync()                         // async methods return Task or Task<T>
        {
            try
            {
                using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
                {
                    client.BaseAddress = new Uri("http://localhost:1194/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            // or application/xml

                    // GET ../api/stock
                    // get all stock listings
                    HttpResponseMessage response = await client.GetAsync("api/stock");                  // async call, await suspends until task finished            
                    if (response.IsSuccessStatusCode)                                                   // 200.299
                    {
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<StockPrice.Models.StockListing>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine(listing.TickerSymbol + " " + listing.Price);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    // GET ../api/stock/IBM
                    // get stock price for IBM
                    try
                    {
                        response = await client.GetAsync("api/stock/IBM");
                       
                        // or
                        //Task <HttpResponseMessage> result = client.GetAsync("api/stock/IBM");
                        // do indepedent work in the meantime...
                        //response = await result;

                        response.EnsureSuccessStatusCode();                         // throw exception if not success
                        var price = await response.Content.ReadAsAsync<double>();
                        Console.WriteLine(price);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // kick off
        static void Main()
        {
            Task result = RunAsync();               // convention is for async methods to finish in Async
            result.Wait();                          // block, not the same as await
        }
    }
}
