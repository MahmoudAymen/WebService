using System;
using Newtonsoft.Json;
using AdminServices.Utilities;
using Microsoft.AspNetCore.Mvc;
using AdminServices.Models.BLL;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AdminServices.Models.Entities;
using Microsoft.AspNetCore.Hosting;

namespace AdminServices.Controllers
{
    public enum WebsiteLanguage
    {
        Fr = 0,
        Ar = 1
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IWebHostEnvironment webHostingEnvironment;

        public UserController(IWebHostEnvironment environment)
        {
            webHostingEnvironment = environment;
        }


        //[HttpGet]
        //public IActionResult GetIp()
        //{
        //    try
        //    {
        //        string IpRemoteAdress = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
        //        string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);
        //        return Json(new { success = true, message = "IpRemoteAdress: " + IpRemoteAdress, data = "IdentifiantUserRequest : " + IdentifiantUserRequest + " \\ UserID : " + Request.Cookies["UserID"]  + " \\ RootPath " + webHostingEnvironment.ContentRootPath });

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Erreur serveur: " + ex.Message });
        //    }
        //}
        [HttpGet]
        [Route("{iduser:long}")]
        public IActionResult Get(long iduser)
        {
            try
            {
                string IpRemoteAdress = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest = true;

                    User user = BLL_User.SelectById(iduser, IsAdminRequest);
                    if (user != null && user.Id > 0)
                    {
                        return Json(new { success = true, message = "Utlisateur trouve", data = user });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Utilisateur introuvable.", data = user });
                    }
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

        [HttpPost]
        [Route("")]
        public JsonResult Post([FromForm] User user)
        {
            try
            {
                string IpRemoteAdress = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest = true;


                    BLL_User.Add(user, IsAdminRequest, Request.Cookies["UserID"], IdentifiantUserRequest, webHostingEnvironment.ContentRootPath);
                    return Json(new { success = true, message = "Utilisateur ajouté avec success" });

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

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("{id:long}")]
        public JsonResult Put(long id, [FromForm] User user)
        {
            try
            {
                string IpRemoteAdress = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest = true;


                    BLL_User.Update(id, user, IsAdminRequest, Request.Cookies["UserID"], IdentifiantUserRequest, webHostingEnvironment.ContentRootPath);
                    return Json(new { success = true, message = "Utilisateur modifié avec succès." });

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

        [HttpDelete]
        [Route("{id}")]
        public JsonResult Delete(long id)
        {
            try
            {
                string IpRemoteAdress = MyHelpers.GetIpRequest(HttpContext.Connection.RemoteIpAddress);
                string IdentifiantUserRequest = MyHelpers.GetIdentifiantUserRequest(Request.Cookies);

                if (MyHelpers.ValidateIpAdresse(IpRemoteAdress, BLL_IpAdresse.SelectAllIpAdresseValidation(IdentifiantUserRequest)))
                {

                    bool IsAdminRequest = false;
                    if (IdentifiantUserRequest.Equals(MyHelpers.IdentifiantAdminRequest))
                        IsAdminRequest = true;

                    BLL_User.Delete(id, IsAdminRequest, Request.Cookies["UserID"], IdentifiantUserRequest, webHostingEnvironment.ContentRootPath);
                    return Json(new { success = true, message = "Utilisateur supprimé avec succès" });

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

        // test connexion
        [Route("TestConnexion")]
        [HttpGet]
        public KeyValuePair<List<User>, string> TestConnexion(string Name, string Password)
        {
            try
            {
                string message;
                //System.Diagnostics.Debug.WriteLine("message1=" + message);
                List<User> Users = BLL_User.TestConnexion(Name, Password, out message);
                System.Diagnostics.Debug.WriteLine("message2=" + message);
                return new KeyValuePair<List<User>, string>(Users, message);

            }
            catch (Exception ex)
            {
                return new KeyValuePair<List<User>, string>(new List<User>(), ex.Message);
            }
        }

        // rechercher compte utilisateur
        // test connexion
        [Route("Login/RechercherCompte")]
        [HttpGet]
        public KeyValuePair<string, string> RechercherCompte(string Email)
        {
            try
            {
                List<User> ListUsers = new List<User>();
                string message = "";
                string JsonUsers = "";
                if (ModelState.IsValid)
                {
                    ListUsers = BLL_User.RechercherCompteUser(Email, out message);
                    if (ListUsers.Count != 0)
                    {
                        JsonUsers = JsonConvert.SerializeObject(ListUsers);
                    }
                }
                return new KeyValuePair<string, string>(JsonUsers, message);
                //return BLL_User.RechercherCompteUser(Email, out message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // test unicite UserName
        [Route("Validation/CreateName")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNameValidation(string name, long idOrganization, WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_User.CheckNameUnicity(name, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        // test unicite Email
        [Route("Validation/CreateEmail")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateEmailValidation(string email, long idOrganization, WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_User.CheckEmailUnicity(email, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"البريد الإلكتروني {email} مستعمل.");
                        default: return new JsonResult($"L'email {email} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        // test l'unicite Name
        [Route("Validation/EditName")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult EditNameValidation(string name, long idOrganization, long id, WebsiteLanguage websiteLanguage)
        {
            try
            {
                User currentUser = BLL_User.SelectById(id, false);
                if (!currentUser.Name.Equals(name) && !BLL_User.CheckNameUnicity(name, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        // test l'unicite Email
        [Route("Validation/EditEmail")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult EditEmailValidation(string email, long idOrganization, long id, WebsiteLanguage websiteLanguage)
        {
            try
            {
                User currentUser = BLL_User.SelectById(id, false);
                if (!currentUser.Email.Equals(email) && !BLL_User.CheckEmailUnicity(email, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"البريد الإلكتروني {email} مستعمل.");
                        default: return new JsonResult($"L'email {email} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


    }
}
