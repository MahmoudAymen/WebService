using System;
using System.Collections.Generic;
using System.Text;

namespace DSSGBOAdmin.Models.Entities
{
    public class Organization
    {
        public long Id { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string Acronym { get; set; }
        public string OrganisationLogo { get; set; }
        public string Affiliation { get; set; }
        public string AffiliationLogo { get; set; }
        public string FieldOfActivity { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PersonToContact { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactPosition { get; set; }
        public string ParDiffusionEmail { get; set; }
        public string ParDiffusionEmailPW { get; set; }
        public string ParOutgoingMailChar { get; set; }
        public string ParIngoingMailChar { get; set; }
        public string AccountStatus { get; set; }
        public string AccountType { get; set; }
        public string OrganizationSystemPrefix { get; set; }
        public Organization(){}
        public Organization(long Id, string NameFr,string NameAr,string Acronym,string OrganisationLogo,string Affiliation,string AffiliationLogo,string FieldOfActivity,string Adress,string PostalCode,string City,string Country,string Email,string Phone,string PersonToContact,string ContactMail,string ContactPhone,string ContactPosition,string ParDiffusionEmail,string ParDiffusionEmailPW,string ParOutgoingMailChar, string ParIngoingMailChar,string AccountStatus,string AccountType,string OrganizationSystemPrefix) {

            this.Id = Id;
            this.NameFr = NameFr;
            this.NameAr = NameAr;
            this.Acronym = Acronym;
            this.OrganisationLogo = OrganisationLogo;
            this.Affiliation = Affiliation;
            this.AffiliationLogo = AffiliationLogo;
            this.FieldOfActivity = FieldOfActivity;
            this.Adress = Adress;
            this.PostalCode = PostalCode;
            this.City = City;
            this.Country = Country;
            this.Email = Email;
            this.Phone = Phone;
            this.PersonToContact = PersonToContact;
            this.ContactMail = ContactMail;
            this.ContactPhone = ContactPhone;
            this.ContactPosition = ContactPosition;
            this.ParDiffusionEmail = ParDiffusionEmail;
            this.ParDiffusionEmailPW = ParDiffusionEmailPW;
            this.ParOutgoingMailChar = ParOutgoingMailChar;
            this.ParIngoingMailChar = ParIngoingMailChar;
            this.AccountStatus = AccountStatus;
            this.AccountType = AccountType;
            this.OrganizationSystemPrefix = OrganizationSystemPrefix;
        }
    }
}
