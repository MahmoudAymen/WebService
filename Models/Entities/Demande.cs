using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSSGBOAdmin.Models.Entities
{
    public class Demande
    {
        public Demande()
        {

        }
        public Demande(long IdDemande,
            string NameDemande, string AffiliationDemande, string FieldOfActivityDemande,
            string AdressDemande, string PostalCodeDemande, string CityDemande,
            string CountryDemande, string EmailDemande,
            string PhoneDemande, string PersonToContactDemande, string ContactMailDemande,
            string ContactPhoneDemande, string ContactPositionDemande,
            string RegDemandDateDemande, string RegDemandDecisionDemande,
            string RegDemandDecisionDateDemande, string RegDecisionCommentsDemande)
        {
            ID = IdDemande;
            Name = NameDemande;
            Affiliation = AffiliationDemande;
            FieldOfActivity = FieldOfActivityDemande;
            Adress = AdressDemande;
            PostalCode = PostalCodeDemande;
            City = CityDemande;
            Country = CountryDemande;
            Email = EmailDemande;
            Phone = PhoneDemande;
            PersonToContact = PersonToContactDemande;
            ContactMail = ContactMailDemande;
            ContactPhone = ContactPhoneDemande;
            ContactPosition = ContactPositionDemande;
            RegDemandDate = RegDemandDateDemande;
            RegDemandDecision = RegDemandDecisionDemande;
            RegDemandDecisionDate = RegDemandDecisionDateDemande;
            RegDecisionComments = RegDecisionCommentsDemande;
            /*
            if (RegDemandDecisionDemande == 0)
                RegDemandDecisionText = "Attends";
            else if (RegDemandDecisionDemande == 1)
                RegDemandDecisionText = "Accepte";
            else if (RegDemandDecisionDemande == 2)
                RegDemandDecisionText = "Refuse";
            else
                RegDemandDecisionText = "Pas de decision";
            */
        }
        public Demande(Demande mDemande)
        {
            if (mDemande != null)
            {
                ID = mDemande.ID;
                Name = mDemande.Name;
                Affiliation = mDemande.Affiliation;
                FieldOfActivity = mDemande.FieldOfActivity;
                Adress = mDemande.Adress;
                PostalCode = mDemande.PostalCode;
                City = mDemande.City;
                Country = mDemande.Country;
                Email = mDemande.Email;
                Phone = mDemande.Phone;
                PersonToContact = mDemande.PersonToContact;
                ContactMail = mDemande.ContactMail;
                ContactPhone = mDemande.ContactPhone;
                ContactPosition = mDemande.ContactPosition;
                RegDemandDate = mDemande.RegDemandDate;
                RegDemandDecision = mDemande.RegDemandDecision;
                RegDemandDecisionDate = mDemande.RegDemandDecisionDate;
                RegDecisionComments = mDemande.RegDecisionComments;

            }
        }

        public long ID { get; set; }
        [Display(Name = "Nom")]

        public string Name { get; set; }
        public string Affiliation { get; set; }

        [Display(Name = "Champs d'Activité")]

        public string FieldOfActivity { get; set; }
        [Display(Name = "Addresse")]
        public string Adress { get; set; }
        [Display(Name = "Code Postal")]
        public string PostalCode { get; set; }
        [Display(Name = "Ville")]
        public string City { get; set; }
        [Display(Name = "Pays")]
        public string Country { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Personne à Contacter")]
        public string PersonToContact { get; set; }

        [Display(Name = "Email Contact")]
        public string ContactMail { get; set; }

        [Display(Name = "Telephone Contact")]
        public string ContactPhone { get; set; }
        [Display(Name = "Position Contact")]
        public string ContactPosition { get; set; }
        [Display(Name = "Date Demande")]
        public string RegDemandDate { get; set; }

        [Display(Name = "Decision")]
        public string RegDemandDecision { get; set; }

        [Display(Name = "Date de Decision")]
        public string RegDemandDecisionDate { get; set; }

        [Display(Name = "Motif de Decision")]
        public string RegDecisionComments { get; set; }

    }
}
