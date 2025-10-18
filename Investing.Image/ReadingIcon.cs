using System.Diagnostics;


namespace Investing.Image
{
    public class ReadingIcon
    {
        Data.Redis redis = new Data.Redis();
        public async Task<Dictionary<string, byte[]>> GetAllIconsCampany()
        {
            var listIconsComany = await ReadImageFromFileArray();
            var redisIcons = await FindUniqueIcons(listIconsComany);
            await GetIconsToRedis(listIconsComany, redisIcons);
            return listIconsComany;
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

        public async Task GetIconsToRedis(Dictionary<string, byte[]> dictionaryAllIcons, Dictionary<string, byte[]> redisIcons)
        {
            Dictionary<string, byte[]> listUniqueIcons = new Dictionary<string, byte[]>();
            try
            {

                #region Redis
                //данные, которые есть в редис нам сейчас не нужны
                var dictionaryWithoutRedis = dictionaryAllIcons.Except(redisIcons).ToArray();

                //нашли первые элементы, дальше их буду джойнить с теми исинами, которые не вошли в спиосок, тк они имеют такие же иконки
                // данная коллекция так же пойдет в редис
                 var duplicateRedisIsin = dictionaryWithoutRedis.GroupBy(x => Convert.ToBase64String(x.Value))
                                                                .Where(g => g.Count() > 1)
                                                                .SelectMany(g => g)
                                                                .GroupBy(x => Convert.ToBase64String(x.Value))
                                                                .Select(g => g.First()); //425

                //итоговая коллекция, которая объединит первые элементы дубликата и те что уникальны - идут в редис
                var unionRedisDictionary = redisIcons.Union(duplicateRedisIsin).ToDictionary(c => c.Key, c=> c.Value);

                await redis.InsertIconsInRedis(unionRedisDictionary);
                #endregion


                #region Postgres


                //коллекция, которая пойдет в 2 модели постгреса (Оригинал и дубликат)

                //Нужно добавить в оригинал все значения duplicateRedisIsin и найти по Convert.ToBase64String(x.Value) такие же сущности в dictionaryAllIcons
                //из dictionaryAllIcons


                //dictionaryWithoutRedis - все иконки, которые имеют еще дубликаты
                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<Dictionary<string, byte[]>> FindUniqueIcons(Dictionary<string, byte[]> dictionaryAllIcons)
        {
            try
            {
             return  dictionaryAllIcons
                    .GroupBy(с => Convert.ToBase64String(с.Value))
                    .Where(с => с.Count() == 1)
                    .SelectMany(g => g)
                    .ToDictionary(с => с.Key, с => с.Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new Dictionary<string, byte[]>();
            }
        }
    }
}
