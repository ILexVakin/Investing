namespace Investing.Models
{
    public class FundSecurity
    {
        public string SECID { get; set; }
        public string SHORTNAME { get; set; }
        public float? PREVPRICE { get; set; }
        public string BOARDID { get; set; }
    }

    public class FundMarketdata
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public decimal? LAST { get; set; }
        public string TRADINGSTATUS { get; set; }
    }
}


