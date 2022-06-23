using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CurrencyData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies/aud.json";
            var client = new System.Net.WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            byte[] json = client.DownloadData(apiUrl);
            string data = System.Text.Encoding.ASCII.GetString(json);

            //fetching key value
            int startIndex = data.IndexOf('{', data.IndexOf('{') + 1); // second occurence of "{";
            int endIndex = data.IndexOf("}", startIndex);
            string result = data.Substring(startIndex, endIndex - startIndex);
            result = result.Remove(0, 1);

            //store data in linked list
            var KeyValueList = new List<KeyValuePair<string, double>>();
            var curreny = result.Split(',').ToList();

            curreny.ForEach(s =>
            KeyValueList.Add(new KeyValuePair<string, double>(s.Split(':')[0].Trim(), Convert.ToDouble(s.Split(':')[1])))
            );

            //sorting by value then by key using function
            KeyValueList = KeyValueList.OrderByDescending(key => key.Value).ThenBy(key => key.Key).ToList();

            //sorting using bubble sort
            //int n= KeyValueList.Count;
            //for(int i = 0; i < n - 1; i++)
            //{
            //    for(int j = 0; j < n - 1; j++)
            //    {
            //        if (KeyValueList.ElementAt(j).Value < KeyValueList.ElementAt(j + 1).Value)
            //        {
            //            var temp = KeyValueList.ElementAt(j).Value;
            //            KeyValueList.ElementAt(j).Value = KeyValueList.ElementAt(j+1).Value;
            //            KeyValueList.ElementAt(j + 1).Value = temp;
            //        }
            //    }
            //}
            
            Console.WriteLine(KeyValueList.ToString());

            //to export as csv file
            string path = @"D:\currencyData.csv";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            String csv = String.Join(Environment.NewLine,
                KeyValueList.Select(d => $"{d.Key}:{d.Value}"));
            File.WriteAllText(path, csv);
        }
    }
}
