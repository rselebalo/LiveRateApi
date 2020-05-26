
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

public static class DataManager
{
    public static List<CurrencyPairModel> GetData()
    {
        try
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create("https://live-rates.com/api/price?rate=EUR_USD,EUR_GBP,GBP_JPY&key=c177a81e69");

            // request json response type
            request.Accept = "application/json";

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var ratesData = new List<CurrencyPairModel>();

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);

                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                ratesData = JsonSerializer.Deserialize<List<CurrencyPairModel>>(responseFromServer);

            }

            // Close the response.
            response.Close();

            return ratesData;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}