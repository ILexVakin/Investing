using Investing.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    public class Currency
    {
        public CurrencySecurity Security { get; set; }
        public CurrencyMarketdata Marketdata { get; set; }
    }

    public class CurrencySecurity : UnionModelInstrumentSecurity
    {
        public int LOTSIZE { get; set; }
        public string SETTLEDATE { get; set; }
        public int FACEVALUE { get; set; }
        public string MARKETCODE { get; set; }
        public string PREVDATE { get; set; }
        public object REMARKS { get; set; }
        public string STATUS { get; set; }
        public string FACEUNIT { get; set; }
        public decimal? PREVWAPRICE { get; set; }
        public string CURRENCYID { get; set; }
        public int LOTDIVIDER { get; set; }
    }

    public class CurrencyMarketdata : UnionModelInstrumentMarketdata
    {
        public object HIGHBID { get; set; }
        public decimal LASTCNGTOLASTWAPRICE { get; set; }
        public decimal? WAPRICE { get; set; }
        public float WAPTOPREVWAPRICE { get; set; }
        public float? CLOSEPRICE { get; set; }
        public string TRADINGSTATUS { get; set; }
        public float WAPTOPREVWAPRICEPRCNT { get; set; }
        public float? CHANGE { get; set; }
        public float VALUE { get; set; }
        public float? VALUE_USD { get; set; }
        public int QTY { get; set; }
        public decimal? PRICEMINUSPREVWAPRICE { get; set; }
        public long? VALTODAY_RUR { get; set; }
        public decimal? MARKETPRICE { get; set; }
        public decimal? MARKETPRICETODAY { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public object ADMITTEDQUOTE { get; set; }
        public decimal? LOPENPRICE { get; set; }
    }
}



