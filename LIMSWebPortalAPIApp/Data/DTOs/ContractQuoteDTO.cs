using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIMSWebPortalAPIApp.Data.DTOs
{
    public class ContractQuoteDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime? QuoteExpiresOn { get; set; }
        public DateTime? ContractStartsOn { get; set; }
        public DateTime? ContractExpiresOn { get; set; }
        public bool IsContracted { get; set; }
        public string QuoteFileName { get; set; }
        public int BillingCycle { get; set; }
        public int Contract_quote_no { get; set; }
        public int Quote_type { get; set; }
        public string Note { get; set; }
    }
}
