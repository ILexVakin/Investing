using System;

namespace Investing.Models
{
    public class ClientsTradingHistory
    {
        public long Id { get; set; }
        public string SecIdCompany { get; set; }
        //покупка/продажа
        public string StatusTrading { get; set; }
        public int ScoreId { get; set; } // за счет номера счета будет связь
        public decimal PriceLot { get; set; }
        public int Quantity { get; set; }
        public decimal Summary { get; set; }
        public DateTime dateTimeExecute { get; set; }
    }
}
