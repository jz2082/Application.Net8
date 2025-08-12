using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Net6.Models;

public class Stock
{
    public string Name { get; set; }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
}
