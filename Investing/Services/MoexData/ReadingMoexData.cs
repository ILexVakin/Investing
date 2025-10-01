using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Investing.Models;
using System.Collections.Generic;
using Investing.Models.ViewModels;
using System.Linq;
using System.Diagnostics;
using Investing.Services.Interfaces;

namespace Investing.Services.MoexData
{
    public class ReadingMoexData : IReadingMoexData
    {
        public async Task<JsonElement> GetAllRowsByExchange(string url)
        {
            HttpClient httpClient = new HttpClient();
            JsonElement root = new JsonElement();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseData))
                {
                    root = doc.RootElement.Clone();
                }
            }
            return root;
        }
    }
}