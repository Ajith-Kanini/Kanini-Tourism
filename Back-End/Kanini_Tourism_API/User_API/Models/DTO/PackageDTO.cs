using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.DTO
{
    public class PackageDTO
    {
        [Key]
        public int PackageId { get; set; }


        public string? PackageName { get; set; }

    }
}
