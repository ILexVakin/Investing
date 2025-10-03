using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Image
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

        public async Task<Dictionary<string, byte[]>> FindDublicateIcons(Dictionary<string, byte[]> dictionaryAllIcons)
        {
            Dictionary<string, byte[]> listUniqueIcons = new Dictionary<string, byte[]>();
            try
            {
                //нашли первые элементы, дальше их буду джойнить с теми исинами, которые не вошли в спиосок, тк они имеют такие же иконки
                var dublicatePhotoIsin = dictionaryAllIcons.GroupBy(x => Convert.ToBase64String(x.Value))
               .Where(g => g.Count() > 1)
               .SelectMany(g => g.Select(x => x.Value))
               .GroupBy(arr => Convert.ToBase64String(arr))
               .Select(g => g.First());



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return listUniqueIcons;
        }

        public async Task<Dictionary<string, byte[]>> FindUniqueIcons(Dictionary<string, byte[]> dictionaryAllIcons)
        {
            Dictionary<string, byte[]> listUniqueIcons = new Dictionary<string, byte[]>();
            try
            {
                //нашли оригинальные по байтам изображения
                var originalPhotoIsin = dictionaryAllIcons.GroupBy(x => Convert.ToBase64String(x.Value))
                    .Where(g => g.Count() == 1)
                    .SelectMany(g => g.Select(x => x.Key)); //530

                listUniqueIcons = (Dictionary<string, byte[]>)(from x in dictionaryAllIcons
                                                               where x.Key == originalPhotoIsin.ElementAt(0)
                                                               select x);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return listUniqueIcons;
        }
    }
}
