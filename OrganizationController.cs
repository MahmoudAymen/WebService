using System;
using AdminServices.Utilities;
using Microsoft.AspNetCore.Mvc;
using AdminServices.Models.BLL;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AdminServices.Models.Entities;
using Microsoft.AspNetCore.Hosting;

namespace AdminServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : Controller
    {

        private readonly IWebHostEnvironment webHostingEnvironment;

        public OrganizationController(IWebHostEnvironment environment)
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
                    List<Organization> organizations = BLL_Organization.SelectAll();
                    return Json(new { success = true, message = "Organisations trouvées.", data = organizations });
                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [Route("{IdOrganization}")]
        [HttpGet]
        public IActionResult Get(long IdOrganization)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    Organization organization = BLL_Organization.SelectById(IdOrganization);
                    if (organization != null && organization.Id > 0)
                    {
                        return Json(new { success = true, message = "Organisation trouvée.", data = organization });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Organisation introuvable." });
                    }
                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur service : " + ex.Message });
            }

        }

        [Route("")]
        [HttpPost]
        public JsonResult Post([FromForm] Organization organization)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    BLL_Organization.Add(organization);
                    return Json(new { success = true, message = "Organisation ajouté avec success" });
                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur serveur: " + ex.Message });
            }
        }

        [Route("{id:long}")]
        [HttpPut]
        public JsonResult Put(long id, [FromForm] Organization organization)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest  = true;

                    BLL_Organization.Update(id, organization, IsAdminRequest, Request.Cookies["UserID"], IdentifiantUserRequest, webHostingEnvironment.ContentRootPath);
                    return Json(new { success = true, message = "Organisation modifié avec succès" });

                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur serveur: " + ex.Message });
            }
        }

        // DELETE api/<OrganizationController>/5
        [Route("{id:long}")]
        [HttpDelete]
        public JsonResult Delete(long id)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    BLL_Organization.Delete(id);
                    return Json(new { success = true, message = "Organisation supprimé avec succès" });

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


        [Route("{IdOrganization}/users")]
        [HttpGet]
        public IActionResult GetAllUersByOrganization(long IdOrganization)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {
                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest = true;

                    List<User> users = BLL_User.SelectAll(IdOrganization, IsAdminRequest);
                    if (users != null && users.Count > 0)
                        return Json(new { success = true, message = "Utlisateurs trouves", data = users });
                    else
                        return Json(new { success = true, message = "Pas des utlisateurs pour cette organisation", data = users });
                }
                else
                {
                    throw new Exception("Requete refusée pour cette adresse IP " + IpRemoteAdress);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur serveur: " + ex.Message });
            }
        }

        [HttpGet("{id}/stats")]
        public JsonResult GetStatisticOrganization(long id)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    var result = BLL_Organization.GetStatsOrganization(id);
                    return Json(new { success = true, message = "", data = result });

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


        [HttpPost("{id}/status")]
        public JsonResult UpdateStatusOrganization(long id, string NewStatus, string NewType)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    BLL_Organization.UpdateStatusOrganization(id, NewStatus, NewType);
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

        [HttpPost("{id}/active")]
        public JsonResult ActiveOrganization(long id, Contract contract)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    contract = BLL_Organization.ActiveOrganization(id, contract);
                    return Json(new { success = true, message = "activé avec success", data = contract });

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

        [HttpPost("{id}/desactive")]
        public JsonResult DesactiveOrganization(long id, string OldTypeOrganization)
        {
            try
            {
                string IpRemoteAdress         = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest) && MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    BLL_Organization.DesactiveOrganization(id, OldTypeOrganization);
                    return Json(new { success = true, message = "desactivé avec success" });

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
