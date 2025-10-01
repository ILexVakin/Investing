namespace Investing.Models
{
    public class Futures
    {
        public FuturesSecurity Securities { get; set; }
        public FuturesMarketdata Marketdata { get; set; }
    }


    public class FuturesSecurity
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public string SHORTNAME { get; set; }
        public string SECNAME { get; set; }
        public float PREVSETTLEPRICE { get; set; }
        public int DECIMALS { get; set; }
        public float MINSTEP { get; set; }
        public string LASTTRADEDATE { get; set; }
        public string LASTDELDATE { get; set; }
        public string SECTYPE { get; set; }
        public string LATNAME { get; set; }
        public string ASSETCODE { get; set; }
        public int? PREVOPENPOSITION { get; set; }
        public int LOTVOLUME { get; set; }
        public float INITIALMARGIN { get; set; }
        public float HIGHLIMIT { get; set; }
        public float LOWLIMIT { get; set; }
        public float STEPPRICE { get; set; }
        public float LASTSETTLEPRICE { get; set; }
        public decimal? PREVPRICE { get; set; }
        public string IMTIME { get; set; }
        public float BUYSELLFEE { get; set; }
        public float SCALPERFEE { get; set; }
        public float NEGOTIATEDFEE { get; set; }
        public float EXERCISEFEE { get; set; }
    }

    public class FuturesMarketdata
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public object BID { get; set; }
        public object OFFER { get; set; }
        public float SPREAD { get; set; }
        public float OPEN { get; set; }
        public float HIGH { get; set; }
        public float LOW { get; set; }
        public decimal? LAST { get; set; }
        public int QUANTITY { get; set; }
        public float LASTCHANGE { get; set; }
        public float SETTLEPRICE { get; set; }
        public float SETTLETOPREVSETTLE { get; set; }
        public int OPENPOSITION { get; set; }
        public int NUMTRADES { get; set; }
        public int VOLTODAY { get; set; }
        public long VALTODAY { get; set; }
        public int VALTODAY_USD { get; set; }
        public string UPDATETIME { get; set; }
        public float LASTCHANGEPRCNT { get; set; }
        public object BIDDEPTH { get; set; }
        public object BIDDEPTHT { get; set; }
        public object NUMBIDS { get; set; }
        public object OFFERDEPTH { get; set; }
        public object OFFERDEPTHT { get; set; }
        public object NUMOFFERS { get; set; }
        public string TIME { get; set; }
        public float SETTLETOPREVSETTLEPRC { get; set; }
        public long SEQNUM { get; set; }
        public string SYSTIME { get; set; }
        public string TRADEDATE { get; set; }
        public decimal? LASTTOPREVPRICE { get; set; }
        public int OICHANGE { get; set; }
        public float OPENPERIODPRICE { get; set; }
        public float SWAPRATE { get; set; }
        public string TRADE_SESSION_DATE { get; set; }
    }

}


