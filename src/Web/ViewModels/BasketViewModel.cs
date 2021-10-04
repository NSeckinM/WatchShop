using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketItemViewModel> Items { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
