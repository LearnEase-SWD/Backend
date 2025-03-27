using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.Entities
{
    public class PaymentInformation
    {
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
