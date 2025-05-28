using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SVContractTestingAPI.Models
{
    public class Family
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<FamilyMember> Members { get; set; } = new List<FamilyMember>();
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}