using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIMSWebPortalAPIApp.Data.DTOs
{
    public class ProjectDTO
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LocalTimezone { get; set; }
        public string CustomerProjectName { get; set; }
        public string CustomerProjectRef { get; set; }
        public int ContractId { get; set; }
        public string ReceivingLab { get; set; }
        public int Status { get; set; }
        public string AemtekProjectId { get; set; }
        public string Comment { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string CustomerPo { get; set; }
        public DateTime? LabDueOn { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
