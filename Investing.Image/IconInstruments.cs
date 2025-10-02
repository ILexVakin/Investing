using Investing.Services;
using Investing.Services.Interfaces;
using System.Diagnostics;
namespace Investing.Image
{
    internal class IconInstruments
    {
        ISearchExchangeInstrumentsService searchExchange = new SearchExchangeInstrumentsService();
        private readonly string savePathFolder = Path.Combine(Path.GetTempPath(), "Images");

        public async Task GetIsinInstruments()
        {
            try
            {
                var fullInstrumentsList = await searchExchange.SearchAllExchangeInstrumentsAsync();
                if (fullInstrumentsList != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        Directory.CreateDirectory(savePathFolder);
                        foreach (var instruments in fullInstrumentsList)
                        {
                            try
                            {
                                if (instruments.Isin != null)
                                {
                                    await DownloadImage(instruments.Isin, httpClient);
                                    await Task.Delay(500);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        async Task DownloadImage(string isin, HttpClient httpClient)
        {
            string tempFilePath = Path.Combine(savePathFolder, isin);
            try
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync($"https://mybroker.storage.bcs.ru/FinInstrumentLogo/{isin}.png");
                await File.WriteAllBytesAsync(tempFilePath + ".png", imageBytes);
            }
            catch
            {
                throw;
            }
        }
    }
}
