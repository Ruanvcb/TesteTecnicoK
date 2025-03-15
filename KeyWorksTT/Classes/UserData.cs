using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserData
{
    public decimal Carteira { get; set; } = 10000.00M;
    public List<PortfolioItem> Portfolio { get; set; } = new List<PortfolioItem>();
}