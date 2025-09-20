namespace Investing.Models
{
    public class SingleModelExchangeInstruments
    {  
        public string Tiker { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        // тип инструмента (акции, облигации и тд)
        public string TypeExchange {  get; set; }
        public decimal? PriceChange {  get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
