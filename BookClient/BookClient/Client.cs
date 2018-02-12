using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PhoneBookService.Models;

namespace BookClient
{
    class Client
    {
        // RunAsync awaits results from async methods so must be async itself
        static async Task RunAsync()                         // async methods return Task or Task<T>
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:45640/");                             // base URL for API Controller i.e. RESTFul service

                    // Accept JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    // GET ../phonebook/name/John Doe
                    HttpResponseMessage response = await client.GetAsync("phonebook/name/John Doe");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var entries = await response.Content.ReadAsAsync<IEnumerable<PhoneBook>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.Address + " " + entry.PhoneNumber);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    // GET ../phonebook/number/01 3333333
                    response = await client.GetAsync("phonebook/number/01 3333333");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var result = await response.Content.ReadAsAsync<PhoneBook>();
                        Console.WriteLine(result.Name + " " + result.Address);
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

        // kick off
        static void Main()
        {
            Task result = RunAsync();               // convention is for async methods to finish in Async
            result.Wait();                          // block, not the same as await
        }
    }
}
