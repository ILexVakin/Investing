using System;

namespace Investing.Models
{
    public class CandlesByDayResponse
    {
            public decimal? Open { get; set; }
            public decimal? Close { get; set; }
            public decimal? High { get; set; }
            public decimal? Low { get; set; }
            public decimal? Value { get; set; }
            public decimal? Volume { get; set; }
            public string? Begin { get; set; }
            public string? End { get; set; }
    }
}
