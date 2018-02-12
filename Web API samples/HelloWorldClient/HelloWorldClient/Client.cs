// Console client for HelloWorld RESTful service
// uses ASP.Net Web API Client API libraries installed in the solution using NuGet
// get hello world greeting and display for a specified name

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HelloWorldClient
{
    class Client
    {
        static async Task RunAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1107/");                             // base URL for API Controller i.e. RESFul service

                    // add an Accept header 
                    client.DefaultRequestHeaders.
                            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));         // or application/xml or application/bson

                    HttpResponseMessage response = await client.GetAsync("api/Hello/Gary");        

                    if (response.IsSuccessStatusCode)
                    {
                        // parse result 
                        String message = response.Content.ReadAsAsync<string>().Result;                  // accessing the Result property blocks
                        Console.WriteLine(message);                                                     // the greeting
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
