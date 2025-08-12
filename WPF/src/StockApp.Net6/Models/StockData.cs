using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Net6.Models;

public class StockData
{
    public DateTime TimeStamp { get; set; }
    public decimal Price { get; set; }
    public string Color { get; set; }
}
