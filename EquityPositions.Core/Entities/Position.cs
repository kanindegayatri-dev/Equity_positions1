using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityPositions.Core.Entities
{
    public class Position
    {
        public string SecurityCode { get; set; }
        public int Quantity { get; set; }
    }
}
