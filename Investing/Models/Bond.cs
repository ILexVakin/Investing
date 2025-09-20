namespace Investing.Models
{
    public class BondSecurity
    {
        public string SECID { get; set; }
        public string SHORTNAME { get; set; }
        public float? PREVPRICE { get; set; }
        public string BOARDID { get; set; }
        public string ISIN { get; set; }
        public float FACEVALUE { get; set; }
        public string FACEUNIT { get; set; }
    }

    public class BondMarketdata
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public decimal? LAST { get; set; }
        public decimal? YIELD { get; set; }
        public string TRADINGSTATUS { get; set; }
    }
}


