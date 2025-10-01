using Investing.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    public class Currency
    {
        public CurrencySecurity securities { get; set; }
        public CurrencyMarketdata marketdata { get; set; }
    }

    public class CurrencySecurity
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public string SHORTNAME { get; set; }
        public int LOTSIZE { get; set; }
        public string SETTLEDATE { get; set; }
        public int DECIMALS { get; set; }
        public int FACEVALUE { get; set; }
        public string MARKETCODE { get; set; }
        public float MINSTEP { get; set; }
        public string PREVDATE { get; set; }
        public string SECNAME { get; set; }
        public object REMARKS { get; set; }
        public string STATUS { get; set; }
        public string FACEUNIT { get; set; }
        public decimal? PREVPRICE { get; set; }
        public decimal? PREVWAPRICE { get; set; }
        public string CURRENCYID { get; set; }
        public string LATNAME { get; set; }
        public int LOTDIVIDER { get; set; }
    }

    public class CurrencyMarketdata
    {
        public object HIGHBID { get; set; }
        public object BIDDEPTH { get; set; }
        public object LOWOFFER { get; set; }
        public object OFFERDEPTH { get; set; }
        public decimal SPREAD { get; set; }
        public decimal? HIGH { get; set; }
        public decimal? LOW { get; set; }
        public decimal? OPEN { get; set; }
        public decimal? LAST { get; set; }
        public decimal LASTCNGTOLASTWAPRICE { get; set; }
        public long VALTODAY { get; set; }
        public decimal VOLTODAY { get; set; }
        public int? VALTODAY_USD { get; set; }
        public decimal? WAPRICE { get; set; }
        public float WAPTOPREVWAPRICE { get; set; }
        public float? CLOSEPRICE { get; set; }
        public int NUMTRADES { get; set; }
        public string TRADINGSTATUS { get; set; }
        public string UPDATETIME { get; set; }
        public string BOARDID { get; set; }
        public string SECID { get; set; }
        public float WAPTOPREVWAPRICEPRCNT { get; set; }
        public object BID { get; set; }
        public object BIDDEPTHT { get; set; }
        public object NUMBIDS { get; set; }
        public object OFFER { get; set; }
        public object OFFERDEPTHT { get; set; }
        public object NUMOFFERS { get; set; }
        public float? CHANGE { get; set; }
        public float LASTCHANGEPRCNT { get; set; }
        public float VALUE { get; set; }
        public float? VALUE_USD { get; set; }
        public long SEQNUM { get; set; }
        public int QTY { get; set; }
        public string TIME { get; set; }
        public decimal? PRICEMINUSPREVWAPRICE { get; set; }
        public decimal LASTCHANGE { get; set; }
        public decimal LASTTOPREVPRICE { get; set; }
        public long? VALTODAY_RUR { get; set; }
        public string SYSTIME { get; set; }
        public decimal? MARKETPRICE { get; set; }
        public decimal? MARKETPRICETODAY { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public object ADMITTEDQUOTE { get; set; }
        public decimal? LOPENPRICE { get; set; }
    }
}



