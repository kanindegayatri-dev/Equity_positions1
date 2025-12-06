using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityPositions.Core.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TradeId { get; set; }
        public int Version { get; set; }
        public string SecurityCode { get; set; }
        public int Quantity { get; set; }
        public string Action { get; set; }
        public string BuySell { get; set; }
    }
}
