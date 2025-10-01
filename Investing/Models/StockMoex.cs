namespace Investing.Models
{
    public class Stocks
    {
        public SecurityStock[] securities { get; set; }
        public MarketdataStock[] marketdata { get; set; }
    }

    public class SecurityStock
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public string SHORTNAME { get; set; }
        public float? PREVPRICE { get; set; }
        public int LOTSIZE { get; set; }
        public float FACEVALUE { get; set; }
        public string STATUS { get; set; }
        public string BOARDNAME { get; set; }
        public int DECIMALS { get; set; }
        public string SECNAME { get; set; }
        public object REMARKS { get; set; }
        public string MARKETCODE { get; set; }
        public string INSTRID { get; set; }
        public object SECTORID { get; set; }
        public float MINSTEP { get; set; }
        public float? PREVWAPRICE { get; set; }
        public string FACEUNIT { get; set; }
        public string PREVDATE { get; set; }
        public long ISSUESIZE { get; set; }
        public string ISIN { get; set; }
        public string LATNAME { get; set; }
        public string REGNUMBER { get; set; }
        public float? PREVLEGALCLOSEPRICE { get; set; }
        public string CURRENCYID { get; set; }
        public string SECTYPE { get; set; }
        public int LISTLEVEL { get; set; }
        public string SETTLEDATE { get; set; }
    }

    public class MarketdataStock
    {
        public string? SECID { get; set; }
        public string? BOARDID { get; set; }
        public float? BID { get; set; }
        public int? BIDDEPTH { get; set; }
        public float? OFFER { get; set; }
        public int? OFFERDEPTH { get; set; }
        public float? SPREAD { get; set; }
        public long? BIDDEPTHT { get; set; }
        public long? OFFERDEPTHT { get; set; }
        public float? OPEN { get; set; }
        public float? LOW { get; set; }
        public float? HIGH { get; set; }
        public decimal? LAST { get; set; }
        public int? LASTCHANGE { get; set; }
        public decimal? LASTCHANGEPRCNT { get; set; }
        public decimal? QTY { get; set; }
        public decimal? VALUE { get; set; }
        public float? VALUE_USD { get; set; }
        public float? WAPRICE { get; set; }
        public decimal? LASTCNGTOLASTWAPRICE { get; set; }
        public float? WAPTOPREVWAPRICEPRCNT { get; set; }
        public float? WAPTOPREVWAPRICE { get; set; }
        public decimal? CLOSEPRICE { get; set; }
        public float? MARKETPRICETODAY { get; set; }
        public decimal? MARKETPRICE { get; set; }
        public decimal? LASTTOPREVPRICE { get; set; }
        public long? NUMTRADES { get; set; }
        public long? VOLTODAY { get; set; }
        public long? VALTODAY { get; set; }
        public int? VALTODAY_USD { get; set; }
        public float? ETFSETTLEPRICE { get; set; }
        public string? TRADINGSTATUS { get; set; }
        public string? UPDATETIME { get; set; }
        public float? LASTBID { get; set; }
        public float? LASTOFFER { get; set; }
        public float? LCLOSEPRICE { get; set; }
        public float? LCURRENTPRICE { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public int? NUMBIDS { get; set; }
        public int? NUMOFFERS { get; set; }
        public decimal? CHANGE { get; set; }
        public string? TIME { get; set; }
        public float? HIGHBID { get; set; }
        public float? LOWOFFER { get; set; }
        public float? PRICEMINUSPREVWAPRICE { get; set; }
        public float? OPENPERIODPRICE { get; set; }
        public long? SEQNUM { get; set; }
        public string? SYSTIME { get; set; }
        public float? CLOSINGAUCTIONPRICE { get; set; }
        public int? CLOSINGAUCTIONVOLUME { get; set; }
        public decimal? ISSUECAPITALIZATION { get; set; }
        public string? ISSUECAPITALIZATION_UPDATETIME { get; set; }
        public object? ETFSETTLECURRENCY { get; set; }
        public long? VALTODAY_RUR { get; set; }
        public string? TRADINGSESSION { get; set; }
        public float? TRENDISSUECAPITALIZATION { get; set; }
    }
}

