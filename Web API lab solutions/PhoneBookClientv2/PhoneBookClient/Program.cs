// client for phone book RESTful web service v2 (CRUD version)

using PhoneBookv2.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PhoneBookClient
{
    class Program
    {
        static async Task DoWork()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:45640/");                             // base URL for API Controller i.e. RESTFul service

                    // Accept JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // 1
                    // POST /api/phonebook with an entry serialised in request body
                    // create a new entry
                    PhoneBookEntry newEntry = new PhoneBookEntry() { Number = "01 4443333", Name = "John Bull", Address = "No. Bev Hills" };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/phonebook", newEntry);
                    if (response.IsSuccessStatusCode)
                    {
                        Uri uri = response.Headers.Location;
                        Console.WriteLine("URI for new resource: " + uri.ToString());
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // 2
                    // update by Put to /api/phonebook/ 
                    //newEntry.Number = "01 4444444";
                    //HttpResponseMessage response = await client.PutAsJsonAsync("api/phonebook/01 4443333", newEntry);
                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    //}

                    // 3
                    // Delete/api/phonebook/01 4444444

                    //HttpResponseMessage response = await client.DeleteAsync("api/phonebook/01 4444444");
                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    //}


                    // GET ../api/phonebook?name=John Bull
                    response = await client.GetAsync("api/phonebook?name=John Bull");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var entries = await response.Content.ReadAsAsync<IEnumerable<PhoneBookEntry>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.Name + " " + entry.Address + " " + entry.Number);
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

        static void Main(string[] args)
        {
            DoWork().Wait();
        }
    }
}
