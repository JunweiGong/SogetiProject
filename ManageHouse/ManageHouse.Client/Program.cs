using ManageHouse.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace ManageHouse.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:5000/api/house/list").Result;
            
            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;

                var houses = JsonConvert.DeserializeObject<List<House>>(body);

                foreach (var item in houses)
                {
                    Console.WriteLine($"{item.Object}- {item.ObjectDescription}" );
                }
            }
            else
            {
                Console.WriteLine("Unable to read from server");
            }

            Console.ReadKey();
        }
    }
}
