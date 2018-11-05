using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Pizza
{
    class Program
    {

        static  void Main(string[] args)
        {
            GetClient();
            Console.ReadLine();
        }


        public static  void GetClient()
        {
            int counter = 0;
            var json = string.Empty;
            List<Pizza> pizza = new List<Pizza>();
            string url = "http://files.olo.com/pizzas.json";

            using (var httpClient = new HttpClient())
            {
                json =  httpClient.GetStringAsync(url).Result;
                
            }

            List<dynamic> list1 = JsonConvert.DeserializeObject<List<dynamic>>(json);

            foreach (var ar in list1)
            {
                foreach(string s in ar["toppings"])
                {
                    pizza.Add(new Pizza { names = s});
                }
                    Console.WriteLine(ar["toppings"]);
                    ++counter;
                    if (counter == 20) break;
            }

            var query = pizza.GroupBy(x => x.names)
              .Where(g => g.Count() >= 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList().OrderByDescending(z => z.Counter);

            Console.WriteLine("---------------Report-----------\n");
            foreach (var v in query)
            {
                Console.WriteLine(v.Element + " : " + v.Counter + " orderes");
            }

            Console.WriteLine("--------------------------");
        }
    }
}
