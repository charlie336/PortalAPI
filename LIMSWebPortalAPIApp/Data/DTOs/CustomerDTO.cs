using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIMSWebPortalAPIApp.Data.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int LocaltimeZone { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Iscontracted { get; set; }
        public bool Isprospect { get; set; }
        public bool Isactive { get; set; }
        public bool Isremoved { get; set; }
        public bool Isflagged { get; set; }
        public bool Isonhold { get; set; }
        public string Note { get; set; }
        public bool IsSampleRecdNotify { get; set; }
        public bool IsPickUp { get; set; }
        public bool IsPrelimCoA { get; set; }
    }
}
