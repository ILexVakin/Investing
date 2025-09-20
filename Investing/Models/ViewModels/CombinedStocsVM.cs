using System.Collections.Generic;

namespace Investing.Models.ViewModels
{
    public class CombinedStocsVM
    {
        public MarketdataStock Marketdata { get; set; }
        public SecurityStock Security { get; set; }
    }
}
