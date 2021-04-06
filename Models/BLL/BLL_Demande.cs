using DSSGBOAdmin.Models.DAL;
using DSSGBOAdmin.Models.Entities;
using MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSSGBOAdmin.Models.BLL
{
    public class BLL_Demande
    {

        public static void CheckNameUnicityEmail(string Email)
        {

            DAL_Demande.selectByField("email", Email);

        }
        public static void CheckNameUnicityName(string Name)
        {

            DAL_Demande.selectByField("name", Name);

        }
        public static void Add(Demande demande)
        {
            DAL_Demande.AddDemande(demande);
        }
        public static void Update(long id, Demande demande)
        {

            Organization newOrganization = new Organization(
              0, demande.Name, "", "", "", demande.Affiliation, "", demande.FieldOfActivity, demande.Adress,
            demande.PostalCode, demande.City, demande.Country, demande.Email, demande.Phone,
            demande.PersonToContact, demande.ContactMail, demande.ContactPhone, demande.ContactPosition,
            "", "", "", "", "essai", "inactive");
            long newOrgId = BLL_Organization.Add(newOrganization);
            if(newOrgId != 0)
            {
                 User AdminOrg = new User(0, newOrgId, "admin" + newOrganization.NameFr.ToUpper(), newOrganization.Email,
                   "1", "admin", DateTime.Now, "", "", demande.Email, "");

                long Iduser = BLL_User.Add(AdminOrg);
                if(Iduser == 0)
                {
                    BLL_Organization.Delete(newOrgId);
                }
            }
            DAL_Demande.UpdateDemande(id, demande);
        }
        public static void Delete(long id)
        {
            DAL_Demande.DeleteDemande(id);
        }
        public static Demande SelectById(long id)
        {
            return DAL_Demande.selectByField("Id", "" + id);
        }
        public static List<Demande> SelectAll()
        {
            return DAL_Demande.selectAll();
        }

    }
}
