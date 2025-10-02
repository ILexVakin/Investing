namespace Investing.Models.Redis
{
    public class DuplicateRedis
    {
        public int Id_duplicate {  get; set; }
        public int Original_id { get; set; }
        public string Isin_duplicate { get; set; }
    }
}
