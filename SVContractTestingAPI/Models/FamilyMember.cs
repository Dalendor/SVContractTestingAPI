using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SVContractTestingAPI.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int FamilyId { get; set; }
        [JsonIgnore]
        public Family? Family { get; set; }
    }
}