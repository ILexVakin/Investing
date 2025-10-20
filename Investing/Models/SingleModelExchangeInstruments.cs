using Investing.Services.MoexData;
using System;
using static Investing.Services.MoexData.FullModelInstrumentsMoex;

namespace Investing.Models
{
    public class SingleModelExchangeInstruments
    {  
        public string? SecId { get; set; } // тикер
        public string? SecName { get; set; }
        //public string? ShortName { get; set; }
        public string? Isin { get; set; }

        // тип инструмента (акции, облигации и тд)
        public TypeInstrument TypeInstrument {  get; set; }
        public string TypeInstrumentRussian { get; set; }
        public decimal? PriceChange {  get; set; }
        public byte[]? ImageIcon { get; set; }
    }
}
