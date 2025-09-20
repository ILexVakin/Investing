namespace Investing.Models
{
    public class FutureSecurity
    {
        public string SECID { get; set; }
        public string SHORTNAME { get; set; }
        public string BOARDID { get; set; }
    }

    public class FutureMarketdata
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public decimal? LAST { get; set; }
        public string TRADINGSTATUS { get; set; }
    }
}


