using System;
using AdminServices.Utilities;
using Microsoft.AspNetCore.Mvc;
using AdminServices.Models.BLL;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using AdminServices.Models.Entities;


namespace AdminServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : Controller
    {

        private readonly IWebHostEnvironment webHostingEnvironment;

        public DemandeController(IWebHostEnvironment environment)
        {
            webHostingEnvironment = environment;
        }


        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    List<Demande> demandes    = BLL_Demande.SelectAll();
                    return Json(new { success = true, message = "Demandes trouvées.", data = demandes });
                }
                else
                {
                    throw new Exception($"Requete refusée pour cette adresse IP {IpRemoteAdress}");
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [Route("{IdDemende}")]
        [HttpGet]
        public IActionResult Get(long IdDemende)
        {
            try
            {
                
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    Demande demande = BLL_Demande.SelectById(IdDemende);
                    if (demande != null && demande.ID > 0)
                    {
                        return Json(new { success = true, message = "Demande trouvée.", data = demande });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Demande introuvable." });
                    }

                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }


        [Route("")]
        [HttpPost]
        public JsonResult RegisterNewDemande(Demande demande)
        {
            try
            {
                
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    BLL_Demande.Add(demande);
                    return Json(new { success = true, message = "Ajouté avec success" });

                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost("{id}")]
        public JsonResult ResponseDemande(int id, string OrganizationSystemPrefix, [FromBody] Demande demande)
        {
            try
            {
                
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    BLL_Demande.Update(id, demande, OrganizationSystemPrefix);
                    return Json(new { success = true, message = "modifié avec success" });

                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

    }
}
