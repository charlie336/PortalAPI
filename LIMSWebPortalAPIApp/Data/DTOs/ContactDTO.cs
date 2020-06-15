using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIMSWebPortalAPIApp.Data.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LocalTimeZone { get; set; }
        public int CreatedBy { get; set; }
        public bool IsReportDelivery { get; set; }
        public bool IsReportPrimary { get; set; }
        public bool IsInvoiceDelivery { get; set; }
        public bool IsSensitiveReportDelvery { get; set; }
        public int LIMSPerson_number { get; set; }
        public string Note { get; set; }
    }
}
