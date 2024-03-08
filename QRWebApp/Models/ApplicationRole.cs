using Microsoft.AspNetCore.Identity;

namespace QRWebApp.Models
{

    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {

        }
        public string Id { get; set; }

        public virtual ICollection<ApplicationUserRole> Users { get; } = new List<ApplicationUserRole>();

        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool IsSysAdmin { get; set; }
        public bool IsCustomer { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? IsActive { get; set; }


    }
}
