namespace Investing.Models
{
    public class Bonds
    {
        public BondSecurity[] BondSecurities { get; set; }
        public BondMarketdata[] BondMarketdata { get; set; }
        public BondDataversion[] dataversion { get; set; }
        public BondMarketdata_Yields[] marketdata_yields { get; set; }
    }


    public class BondSecurity
    {
        public string? SECID { get; set; }
        public string BOARDID { get; set; }
        public string SHORTNAME { get; set; }
        public float? PREVWAPRICE { get; set; }
        public float YIELDATPREVWAPRICE { get; set; }
        public float COUPONVALUE { get; set; }
        public string NEXTCOUPON { get; set; }
        public float ACCRUEDINT { get; set; }
        public decimal? PREVPRICE { get; set; }
        public int? LOTSIZE { get; set; }
        public double? FACEVALUE { get; set; }
        public string BOARDNAME { get; set; }
        public string STATUS { get; set; }
        public string MATDATE { get; set; }
        public int DECIMALS { get; set; }
        public int COUPONPERIOD { get; set; }
        public long ISSUESIZE { get; set; }
        public decimal? PREVLEGALCLOSEPRICE { get; set; }
        public string PREVDATE { get; set; }
        public string SECNAME { get; set; }
        public string REMARKS { get; set; }
        public string MARKETCODE { get; set; }
        public string INSTRID { get; set; }
        public object SECTORID { get; set; }
        public float MINSTEP { get; set; }
        public string FACEUNIT { get; set; }
        public float? BUYBACKPRICE { get; set; }
        public string BUYBACKDATE { get; set; }
        public string ISIN { get; set; }
        public string LATNAME { get; set; }
        public string REGNUMBER { get; set; }
        public string CURRENCYID { get; set; }
        public long? ISSUESIZEPLACED { get; set; }
        public int LISTLEVEL { get; set; }
        public string SECTYPE { get; set; }
        public float? COUPONPERCENT { get; set; }
        public string OFFERDATE { get; set; }
        public string SETTLEDATE { get; set; }
        public float LOTVALUE { get; set; }
        public float? FACEVALUEONSETTLEDATE { get; set; }
        public string CALLOPTIONDATE { get; set; }
        public string PUTOPTIONDATE { get; set; }
        public string DATEYIELDFROMISSUER { get; set; }
    }

    public class BondMarketdata
    {
        public string? SECID { get; set; }
        public float? BID { get; set; }
        public object BIDDEPTH { get; set; }
        public float? OFFER { get; set; }
        public object OFFERDEPTH { get; set; }
        public float SPREAD { get; set; }
        public int BIDDEPTHT { get; set; }
        public int OFFERDEPTHT { get; set; }
        public float? OPEN { get; set; }
        public float? LOW { get; set; }
        public float? HIGH { get; set; }
        public decimal? LAST { get; set; }
        public float LASTCHANGE { get; set; }
        public float LASTCHANGEPRCNT { get; set; }
        public int QTY { get; set; }
        public float VALUE { get; set; }
        public decimal? YIELD { get; set; }
        public float VALUE_USD { get; set; }
        public float? WAPRICE { get; set; }
        public float LASTCNGTOLASTWAPRICE { get; set; }
        public float WAPTOPREVWAPRICEPRCNT { get; set; }
        public float WAPTOPREVWAPRICE { get; set; }
        public float YIELDATWAPRICE { get; set; }
        public float YIELDTOPREVYIELD { get; set; }
        public int CLOSEYIELD { get; set; }
        public object CLOSEPRICE { get; set; }
        public object MARKETPRICETODAY { get; set; }
        public decimal? MARKETPRICE { get; set; }
        public decimal? LASTTOPREVPRICE { get; set; }
        public int NUMTRADES { get; set; }
        public int VOLTODAY { get; set; }
        public int VALTODAY { get; set; }
        public int VALTODAY_USD { get; set; }
        public string BOARDID { get; set; }
        public string TRADINGSTATUS { get; set; }
        public string UPDATETIME { get; set; }
        public int DURATION { get; set; }
        public object NUMBIDS { get; set; }
        public object NUMOFFERS { get; set; }
        public float? CHANGE { get; set; }
        public string TIME { get; set; }
        public object HIGHBID { get; set; }
        public object LOWOFFER { get; set; }
        public float? PRICEMINUSPREVWAPRICE { get; set; }
        public object LASTBID { get; set; }
        public object LASTOFFER { get; set; }
        public float? LCURRENTPRICE { get; set; }
        public object LCLOSEPRICE { get; set; }
        public decimal? MARKETPRICE2 { get; set; }
        public float? OPENPERIODPRICE { get; set; }
        public long SEQNUM { get; set; }
        public string SYSTIME { get; set; }
        public int VALTODAY_RUR { get; set; }
        public object IRICPICLOSE { get; set; }
        public object BEICLOSE { get; set; }
        public object CBRCLOSE { get; set; }
        public float? YIELDTOOFFER { get; set; }
        public float? YIELDLASTCOUPON { get; set; }
        public string TRADINGSESSION { get; set; }
        public float? CALLOPTIONYIELD { get; set; }
        public float? CALLOPTIONDURATION { get; set; }
    }

    public class BondDataversion
    {
        public int data_version { get; set; }
        public long seqnum { get; set; }
        public string trade_date { get; set; }
        public string trade_session_date { get; set; }
    }

    public class BondMarketdata_Yields
    {
        public string SECID { get; set; }
        public string BOARDID { get; set; }
        public float PRICE { get; set; }
        public string YIELDDATE { get; set; }
        public string ZCYCMOMENT { get; set; }
        public string YIELDDATETYPE { get; set; }
        public float? EFFECTIVEYIELD { get; set; }
        public int? DURATION { get; set; }
        public int? ZSPREADBP { get; set; }
        public int? GSPREADBP { get; set; }
        public float? WAPRICE { get; set; }
        public float? EFFECTIVEYIELDWAPRICE { get; set; }
        public int? DURATIONWAPRICE { get; set; }
        public object IR { get; set; }
        public object ICPI { get; set; }
        public object BEI { get; set; }
        public object CBR { get; set; }
        public float? YIELDTOOFFER { get; set; }
        public float? YIELDLASTCOUPON { get; set; }
        public string TRADEMOMENT { get; set; }
        public long SEQNUM { get; set; }
        public string SYSTIME { get; set; }
    }
}


