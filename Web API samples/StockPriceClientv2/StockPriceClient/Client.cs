// client for StockPricev2 CRUD RESTful web service
// add a listing, update the listing, delete a listing, and show all listings

using StockPrice2.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StockPriceClient2
{
    class Program
    {
        // get all stock listings
        static async Task GetAllAsync()                         
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52272/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            

                    // GET ../api/stock
                    // get all stock listings
                    HttpResponseMessage response = await client.GetAsync("api/stock");                  // async call, await suspends until result available            
                    if (response.IsSuccessStatusCode)                                                   // 200..299
                    {
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<StockListing>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine(listing.TickerSymbol + " " + listing.Price);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // add a stock listing
        static async Task AddAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52272/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));               

                    // POST /api/stock with a listing serialised in request body
                    // create a new listing for Facebook
                    StockListing newListing = new StockListing() { TickerSymbol = "FB", Price = 34.1 };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/stock", newListing);   // or PostAsXmlAsync
                    if (response.IsSuccessStatusCode)                                                       // 200 .. 299
                    {
                        Uri newStockUri = response.Headers.Location;
                        var listing = await response.Content.ReadAsAsync<StockListing>();
                        Console.WriteLine("URI for new resource: " + newStockUri.ToString());
                        Console.WriteLine("resource " + listing.TickerSymbol + " " + listing.Price);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // update a stock listing
        static async Task UpdateAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52272/");                           

                    StockListing listing = new StockListing() { TickerSymbol = "FB", Price = 34.1 };
                    listing.Price = 34.0;                                              // price has dropped for FB

                    // update by Put to /api/stock/FB a listing serialised in request body
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/stock/FB", listing);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // delete a stock listing
        static async Task DeleteAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52272/");                             

                    // de-list Google
                    // Delete/api/FB                                                    
                    HttpResponseMessage response = await client.DeleteAsync("api/stock/FB");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
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
            //AddAsync().Wait();
            //UpdateAsync().Wait();
            //DeleteAsync().Wait();
            GetAllAsync().Wait();
             
        }
    }
}

