using System;

namespace Investing.Models
{
    public class Funds
    {
        public FundSecurity Security { get; set; }
        public FundMarketdata Marketdata { get; set; }
    }

    public class FundSecurity
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public string SHORTNAME { get; set; }
        public decimal? PREVPRICE { get; set; }
        public int? LOTSIZE { get; set; }
        public int? FACEVALUE { get; set; }
        public string STATUS { get; set; }
        public string BOARDNAME { get; set; }
        public int DECIMALS { get; set; }
        public string SECNAME { get; set; }
        public object REMARKS { get; set; }
        public string MARKETCODE { get; set; }
        public string INSTRID { get; set; }
        public object SECTORID { get; set; }
        public float MINSTEP { get; set; }
        public float PREVWAPRICE { get; set; }
        public string FACEUNIT { get; set; }
        public string PREVDATE { get; set; }
        public int ISSUESIZE { get; set; }
        public string ISIN { get; set; }
        public string LATNAME { get; set; }
        public string REGNUMBER { get; set; }
        public decimal? PREVLEGALCLOSEPRICE { get; set; }
        public string CURRENCYID { get; set; }
        public string SECTYPE { get; set; }
        public int LISTLEVEL { get; set; }
        public string SETTLEDATE { get; set; }
    }

    public class FundMarketdata
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public float BID { get; set; }
        public object BIDDEPTH { get; set; }
        public float OFFER { get; set; }
        public object OFFERDEPTH { get; set; }
        public float SPREAD { get; set; }
        public long BIDDEPTHT { get; set; }
        public long OFFERDEPTHT { get; set; }
        public float? OPEN { get; set; }
        public float? LOW { get; set; }
        public float? HIGH { get; set; }
        public float? LAST { get; set; }
        public float LASTCHANGE { get; set; }
        public float LASTCHANGEPRCNT { get; set; }
        public int QTY { get; set; }
        public float VALUE { get; set; }
        public float VALUE_USD { get; set; }
        public float? WAPRICE { get; set; }
        public float LASTCNGTOLASTWAPRICE { get; set; }
        public float WAPTOPREVWAPRICEPRCNT { get; set; }
        public float WAPTOPREVWAPRICE { get; set; }
        public object CLOSEPRICE { get; set; }
        public object MARKETPRICETODAY { get; set; }
        public decimal? MARKETPRICE { get; set; }
        public decimal? LASTTOPREVPRICE { get; set; }
        public int NUMTRADES { get; set; }
        public long VOLTODAY { get; set; }
        public long VALTODAY { get; set; }
        public int VALTODAY_USD { get; set; }
        public object ETFSETTLEPRICE { get; set; }
        public string TRADINGSTATUS { get; set; }
        public string UPDATETIME { get; set; }
        public object LASTBID { get; set; }
        public object LASTOFFER { get; set; }
        public object LCLOSEPRICE { get; set; }
        public float LCURRENTPRICE { get; set; }
        public object MARKETPRICE2 { get; set; }
        public object NUMBIDS { get; set; }
        public object NUMOFFERS { get; set; }
        public float? CHANGE { get; set; }
        public string TIME { get; set; }
        public object HIGHBID { get; set; }
        public object LOWOFFER { get; set; }
        public float? PRICEMINUSPREVWAPRICE { get; set; }
        public float? OPENPERIODPRICE { get; set; }
        public long SEQNUM { get; set; }
        public string SYSTIME { get; set; }
        public object CLOSINGAUCTIONPRICE { get; set; }
        public object CLOSINGAUCTIONVOLUME { get; set; }
        public object ISSUECAPITALIZATION { get; set; }
        public object ISSUECAPITALIZATION_UPDATETIME { get; set; }
        public object ETFSETTLECURRENCY { get; set; }
        public long VALTODAY_RUR { get; set; }
        public string TRADINGSESSION { get; set; }
        public object TRENDISSUECAPITALIZATION { get; set; }
    }

}