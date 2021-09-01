using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JckShopping.Data.Entities
{
    public class PaymentGateways
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MERCHANT_KEY { get; set; }
        public string HashSequence { get; set; }
        public string SALT { get; set; }
        public string PAYU_BASE_URL { get; set; }
        public string SUrl { get; set; }
        public string FUrl { get; set; }
        public string CUrl { get; set; }
        public string CustomData1 { get; set; }
        public string CustomData2 { get; set; }        
        public JKCStoreUser User { get; set; }
        public bool Active { get; set; }

    }
}
