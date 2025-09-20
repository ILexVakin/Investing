using Investing.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    public class Currency
    {
        public CurrencySecurity[] securities { get; set; }
        public CurrencyMarketdata[] marketdata { get; set; }
    }
    public class CurrencySecurity
    {
        [Column("SECID")]
        public string? SecId { get; set; }

        [Column("SHORTNAME")]
        public string? ShortName { get; set; }
        public string BoardId { get; set; }
        public string BoardName { get; set; }
        public bool? IsTraded { get; set; }
        public decimal? Precio { get; set; }
        [Column("LOTSIZE")]
        public int? LotSize { get; set; }
        public decimal? FaceValue { get; set; }
        public string FaceUnit { get; set; }
        public decimal? Scale { get; set; }
        public DateTime? MatDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public decimal? InitialMargin { get; set; }
        public string CurrencyId { get; set; }
        public string SecType { get; set; }
        public string SecForm { get; set; }
        public string ListLevel { get; set; }
        public DateTime? SettleDate { get; set; }
        public decimal? MinStep { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? BuyDeposit { get; set; }
        public decimal? SellDeposit { get; set; }
        public decimal? BgoC { get; set; }
        public decimal? BgoNc { get; set; }
        public decimal? BgoBuy { get; set; }
        public decimal? BgoSell { get; set; }
        public string TradingStatus { get; set; }
        public string Engine { get; set; }
        public string Market { get; set; }
        public int? Decimals { get; set; }
        public int? HighLimit { get; set; }
        public int? LowLimit { get; set; }
        public DateTime? PrevDate { get; set; }
        
        [Column("PREVPRICE")]
        public decimal? PrevPrice { get; set; }
        public decimal? PrevWaPrice { get; set; }
        public string? SecName { get; set; }
        public string Remarks { get; set; }
        public string MarketCode { get; set; }
        public string InstrumentId { get; set; }
        public string InstrumentType { get; set; }
        public string Isin { get; set; }
        public string LatName { get; set; }
        public string Sectype { get; set; }
        public int? ListNum { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string SecSubType { get; set; }
        public string BaseActiveSecid { get; set; }
        public string BaseActiveBoardid { get; set; }
        public decimal? BaseActiveScale { get; set; }
        public string CounterActiveSecid { get; set; }
        public string CounterActiveBoardid { get; set; }
        public decimal? CounterActiveScale { get; set; }
        public string UnderlyingAsset { get; set; }
        public decimal? UnderlyingScale { get; set; }
        public string CbrRateType { get; set; }
        public string CbrCurrencyId { get; set; }
        public string CbrBaseCurrencyId { get; set; }
        public string CbrCounterCurrencyId { get; set; }
        public string PriceType { get; set; }
        public string PriceSign { get; set; }
        public string PriceCurr { get; set; }
        public string PriceUnit { get; set; }
        public string PriceRule { get; set; }
        public string PriceRuleValue { get; set; }
        public string PriceRuleCurrency { get; set; }
        public string PriceRuleUnit { get; set; }
        public string SettleCurrency { get; set; }
        public string SettleType { get; set; }
        public string SettleCode { get; set; }
        public string UnderlyingType { get; set; }
        public string UnderlyingCode { get; set; }
        public string UnderlyingCurrency { get; set; }
        public string UnderlyingUnit { get; set; }
        public string UnderlyingRateType { get; set; }
        public string UnderlyingRateCode { get; set; }
        public string UnderlyingRateCurrency { get; set; }
        public string UnderlyingRateUnit { get; set; }
        public string UnderlyingRateRule { get; set; }
        public string UnderlyingRateRuleValue { get; set; }
        public string UnderlyingRateRuleCurrency { get; set; }
        public string UnderlyingRateRuleUnit { get; set; }
        public string UnderlyingRateSettleCurrency { get; set; }
        public string UnderlyingRateSettleType { get; set; }
        public string UnderlyingRateSettleCode { get; set; }
        public int? QtyDecimals { get; set; }
        public int? PriceDecimals { get; set; }
        public int? AmountDecimals { get; set; }
        public string TickSize { get; set; }
        public string TickValue { get; set; }
        public string TickCurrency { get; set; }
        public string TickUnit { get; set; }
        public string TickRule { get; set; }
        public string TickRuleValue { get; set; }
        public string TickRuleCurrency { get; set; }
        public string TickRuleUnit { get; set; }
        public string TickSettleCurrency { get; set; }
        public string TickSettleType { get; set; }
        public string TickSettleCode { get; set; }
        public string MarginCurrency { get; set; }
        public string MarginType { get; set; }
        public string MarginCode { get; set; }
        public string MarginRule { get; set; }
        public string MarginRuleValue { get; set; }
        public string MarginRuleCurrency { get; set; }
        public string MarginRuleUnit { get; set; }
        public string MarginSettleCurrency { get; set; }
        public string MarginSettleType { get; set; }
        public string MarginSettleCode { get; set; }
        public string FeeCurrency { get; set; }
        public string FeeType { get; set; }
        public string FeeCode { get; set; }
        public string FeeRule { get; set; }
        public string FeeRuleValue { get; set; }
        public string FeeRuleCurrency { get; set; }
        public string FeeRuleUnit { get; set; }
        public string FeeSettleCurrency { get; set; }
        public string FeeSettleType { get; set; }
        public string FeeSettleCode { get; set; }
        public string TaxCurrency { get; set; }
        public string TaxType { get; set; }
        public string TaxCode { get; set; }
        public string TaxRule { get; set; }
        public string TaxRuleValue { get; set; }
        public string TaxRuleCurrency { get; set; }
        public string TaxRuleUnit { get; set; }
        public string TaxSettleCurrency { get; set; }
        public string TaxSettleType { get; set; }
        public string TaxSettleCode { get; set; }
    }

    public class CurrencyMarketdata
    {
        public string SecId { get; set; }
        public long? NumTrades { get; set; }
        public decimal? Value { get; set; }
        public decimal? Open { get; set; }
        public decimal? Low { get; set; }
        public decimal? High { get; set; }
        public decimal? LegalClosePrice { get; set; }
        public decimal? Waprice { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? Volume { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? MarketPrice2 { get; set; }
        public decimal? AdmittedQuote { get; set; }
        public decimal? Mp2ValTrd { get; set; }
        public decimal? MarketPrice3 { get; set; }
        public decimal? Mp3ValTrd { get; set; }
        public decimal? Yield { get; set; }
        public decimal? YieldAtWap { get; set; }
        public decimal? YieldClose { get; set; }
        public DateTime? SysTime { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        public decimal? PriceMinusPrevWaPrice { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public decimal? LastChangeBidTime { get; set; }
        public decimal? LastChangeOfferTime { get; set; }
        public decimal? LastChange { get; set; }
        public decimal? LastChangeBid { get; set; }
        public decimal? LastChangeOffer { get; set; }
        public decimal? LastChangePrcnt { get; set; }
        public decimal? LastChangeBidPrcnt { get; set; }
        public decimal? LastChangeOfferPrcnt { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Offer { get; set; }
        public decimal? Spread { get; set; }
        public decimal? Biddepth { get; set; }
        public decimal? Biddeptht { get; set; }
        public decimal? Offerdepth { get; set; }
        public decimal? Offerdeptht { get; set; }
        public int? BidDepth2 { get; set; }
        public int? BidDepth2T { get; set; }
        public int? OfferDepth2 { get; set; }
        public int? OfferDepth2T { get; set; }
        public int? BidDepth3 { get; set; }
        public int? BidDepth3T { get; set; }
        public int? OfferDepth3 { get; set; }
        public int? OfferDepth3T { get; set; }
        public decimal? TrendWap { get; set; }
        public decimal? TrendWapPrcnt { get; set; }
        public decimal? TrendCls { get; set; }
        public decimal? TrendClsPrcnt { get; set; }
        public decimal? TrendLast { get; set; }
        public decimal? TrendLastPrcnt { get; set; }
        public decimal? TrendBid { get; set; }
        public decimal? TrendBidPrcnt { get; set; }
        public decimal? TrendOffer { get; set; }
        public decimal? TrendOfferPrcnt { get; set; }
        public decimal? TrendVal { get; set; }
        public decimal? TrendValPrcnt { get; set; }
        public decimal? TrendVol { get; set; }
        public decimal? TrendVolPrcnt { get; set; }
        public decimal? TrendNumTrades { get; set; }
        public decimal? TrendNumTradesPrcnt { get; set; }
        public decimal? LastBid { get; set; }
        public decimal? LastOffer { get; set; }
        public decimal? Last { get; set; }
        public decimal? LastCng { get; set; }
        public decimal? LastCngPrcnt { get; set; }
        public decimal? LastToprevpriceCng { get; set; }
        public decimal? LastToprevpriceCngPrcnt { get; set; }
        public decimal? LastToWaPriceCng { get; set; }
        public decimal? LastToWaPriceCngPrcnt { get; set; }
        public decimal? LastToprevWaPriceCng { get; set; }
        public decimal? LastToprevWaPriceCngPrcnt { get; set; }
        public decimal? LastBidCng { get; set; }
        public decimal? LastBidCngPrcnt { get; set; }
        public decimal? LastOfferCng { get; set; }
        public decimal? LastOfferCngPrcnt { get; set; }
        public decimal? LastBidToprevpriceCng { get; set; }
        public decimal? LastBidToprevpriceCngPrcnt { get; set; }
        public decimal? LastOfferToprevpriceCng { get; set; }
        public decimal? LastOfferToprevpriceCngPrcnt { get; set; }
        public decimal? LastBidToWaPriceCng { get; set; }
        public decimal? LastBidToWaPriceCngPrcnt { get; set; }
        public decimal? LastOfferToWaPriceCng { get; set; }
        public decimal? LastOfferToWaPriceCngPrcnt { get; set; }
        public decimal? LastBidToprevWaPriceCng { get; set; }
        public decimal? LastBidToprevWaPriceCngPrcnt { get; set; }
        public decimal? LastOfferToprevWaPriceCng { get; set; }
        public decimal? LastOfferToprevWaPriceCngPrcnt { get; set; }
        public decimal? ValToPrevPrice { get; set; }
        public decimal? ValToPrevPricePrcnt { get; set; }
        public decimal? ValToWaPrice { get; set; }
        public decimal? ValToWaPricePrcnt { get; set; }
        public decimal? ValToPrevWaPrice { get; set; }
        public decimal? ValToPrevWaPricePrcnt { get; set; }
        public decimal? VolToPrevPrice { get; set; }
        public decimal? VolToPrevPricePrcnt { get; set; }
        public decimal? VolToWaPrice { get; set; }
        public decimal? VolToWaPricePrcnt { get; set; }
        public decimal? VolToPrevWaPrice { get; set; }
        public decimal? VolToPrevWaPricePrcnt { get; set; }
        public decimal? NumTradesToPrevPrice { get; set; }
        public decimal? NumTradesToPrevPricePrcnt { get; set; }
        public decimal? NumTradesToWaPrice { get; set; }
        public decimal? NumTradesToWaPricePrcnt { get; set; }
        public decimal? NumTradesToPrevWaPrice { get; set; }
        public decimal? NumTradesToPrevWaPricePrcnt { get; set; }
        public string TradingSession { get; set; }
    }

}



