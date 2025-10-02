using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Redis.Services
{
    internal class ReadingIcon
    {
        public async Task<Dictionary<string, byte[]>> GetAllIconsCampany()
        {
            var listIconsComany = await ReadImageFromFileArray();
            var listUniqueIcons = await FindUniqueIcons(listIconsComany);
            return listUniqueIcons;
        }

        public async Task<Dictionary<string, byte[]>> ReadImageFromFileArray()
        {
            string pathToFolder = @"C:\Users\Alex\Desktop\IconsComp";
            string[] arrayIcons = Directory.GetFiles(pathToFolder);
            Dictionary<string, byte[]> iconsCompanyDictionary = new Dictionary<string, byte[]>();
            foreach (var icon in arrayIcons)
            {
                var byteIcon = File.ReadAllBytes(icon);
                var isinIcon = icon.Replace(pathToFolder, "").Replace(".png", "").Replace("\\", "");
                iconsCompanyDictionary.Add(isinIcon, byteIcon);
            }

            return iconsCompanyDictionary;

        }

        public async Task<Dictionary<string, byte[]>> FindUniqueIcons(Dictionary<string, byte[]> dictionaryAllIcons)
        {
            Dictionary<string, byte[]> listUniqueIcons = new Dictionary<string, byte[]>();
            try
            {
                var t = dictionaryAllIcons.GroupBy(x => Convert.ToBase64String(x.Value))
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.Select(x => x.Key));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return listUniqueIcons;
        }
    }
}
