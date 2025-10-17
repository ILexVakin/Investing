using System;

namespace Investing.Models
{
    public class CandlesRequest
    {
        public string SecId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateOver { get; set; }
    }
}
