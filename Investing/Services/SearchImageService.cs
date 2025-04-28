using System.Net.Http;
using System.Threading.Tasks;
using System;
using HtmlAgilityPack;
using static System.Net.Mime.MediaTypeNames;

namespace Investing.Services
{
    public class SearchImageService
    {
        public async Task DownloadImage(string shortName)
        {
            using (var httpClient = new HttpClient())
            {
                var imageBytes = await httpClient.GetByteArrayAsync("https://mybroker.storage.bcs.ru/FinInstrumentLogo/" + shortName + ".png");
                var base64String = Convert.ToBase64String(imageBytes); // Преобразование в Base64

                //var image = new Image
                //{
                //    Name = imageName,
                //    DataBase64 = base64String
                //};

                //context.Images.Add(image);
                //await context.SaveChangesAsync();
            }
        }
    }
}
