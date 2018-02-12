using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using SMSGateway.Models;

namespace SMSGatewayClient
{
    class Client
    {
        static async Task RunAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:11982/");                             // base URL for API Controller i.e. RESFul service

                    // add an Accept header 
                    client.DefaultRequestHeaders.
                            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));         // or application/xml or application/bson


                    // POST /api/SMSGateway with a txt message serialised in request body
                    // send a txt msg
                    TextMessage txt = new TextMessage() { FromNumber = "087 1111111", Content = "Hello :-)", ToNumber = "087 2222222" };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/SMSGateway", txt);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent ok");
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
            Task result = RunAsync();               
            result.Wait();                          
        }
    }
}
