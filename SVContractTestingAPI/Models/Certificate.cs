using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SVContractTestingAPI.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [Required]
        public int FamilyId { get; set; }
        [JsonIgnore]
        public Family? Family { get; set; }
    }
}