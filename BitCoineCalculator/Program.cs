using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoineCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitCoinRate currentBitcoin = GetRates();
            Console.WriteLine($"current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            Console.WriteLine($"{currentBitcoin.disclaimer}");

            Console.WriteLine("Calculate in: EUR/USD/GBP");
            string userChoice = Console.ReadLine();
            Console.WriteLine("Enter the amount of bitcoin");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if (userChoice == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;
            }
            float result = currentRate * userCoins;
            Console.WriteLine($"Your bitcoin are worth {result}{userChoice}");

            
        }

        public static BitCoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            

            BitCoinRate bitCoinData;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitCoinData = JsonConvert.DeserializeObject<BitCoinRate>(response);
            }
            return bitCoinData;


        }
    }
}
