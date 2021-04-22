using System;
using System.Collections.Generic;
using System.Text;

namespace DSSGBOAdmin.Models.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public long IdOrganization { get; set; }
        public string Role { get; set; }
        public string OrganisationLogoUser { get; set; }
        public string AffiliationLogoUser { get; set; }
        public string EmailOrganisation { get; set; }
        public string PasswordOrganisation { get; set; }
        public string OrganizationSystemPrefix { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public User() { }
        public User(long IdUser, long IdOrganizationUser, string NameUser,
                           string EmailUser, string PasswordUser, string RoleUser,
                           DateTime AccountCreationDateUser ,string OrganisationLogoUser = null,string AffiliationLogoUser=null,string EmailOrganisation=null, string PasswordOrganisation = null)
        {
            Id = IdUser;
            IdOrganization = IdOrganizationUser;
            Name = NameUser;
            Email = EmailUser;
            Role = RoleUser;
            PassWord = PasswordUser;
            AccountCreationDate = AccountCreationDateUser;
            this.OrganisationLogoUser = OrganisationLogoUser;
            this.AffiliationLogoUser = AffiliationLogoUser;
            this.EmailOrganisation = EmailOrganisation;
            this.PasswordOrganisation = PasswordOrganisation;
        }
    }
}
