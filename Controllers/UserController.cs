using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminServiceGBO.Models.BLL;
using AdminServiceGBO.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminServiceGBO.Controllers
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
        [Route("Organization/{IdOrganization:long}")]
        [HttpGet]
        public IActionResult GetAllUers(long IdOrganization)
        {
            try
            {
                List<User> users = BLL_User.SelectAll(IdOrganization);
                if (users != null && users.Count > 0)
                    return Json(new { success = true, message = "Users found", data = users });
                else
                    return Json(new { success = true, message = "No User in the database for the organization", data = users });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }



        // GET api/<UserController>/5
        [HttpGet]
        [Route("{iduser:long}")]
        public IActionResult Get(long iduser)
        {
            try
            {
                User user = BLL_User.SelectById(iduser);
                if (user != null && user.Id > 0)
                    return Json(new { success = true, message = "User found", data = user });
                else
                    return Json(new { success = true, message = "User not found in the database", data = user });
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        // POST api/<UserController>
        [Route("add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Post([FromForm] User user)
        {
            try
            {
                BLL_User.Add(user);
                return Json(new { success = true, message = "Ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        // PUT api/<UserController>/5
        [Route("{id:long}")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public JsonResult Put(long id, [FromForm] User user)
        {
            try
            {
                BLL_User.Update(id, user);
                return Json(new { success = true, message = "Modifié avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        [Route("{id:long}")]
        [HttpDelete()]
        public JsonResult Delete(long id)
        {
            try
            {
                BLL_User.Delete(id);
                return Json(new { success = true, message = "Supprimé avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }






        // test connexion
        [Route("TestConnexion")]
        [HttpGet]
        public List<User> TestConnexion(string Name, string Password,string message)
        {
            try
            {
                return BLL_User.TestConnexion(Name, Password, out message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // rechercher compte utilisateur
        // test connexion
        [Route("TestConnexion")]
        [HttpGet]
        public List<User> RechercherCompte(string Email, string message)
        {
            try
            {
                return BLL_User.RechercherCompteUser(Email, out message);
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
        public IActionResult CreateNameValidation(string name, long idOrganization,WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_User.CheckNameUnicityName(name, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }
        // test unicite Email
        [Route("Validation/CreateEmail")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateEmailValidation(string email, long idOrganization,WebsiteLanguage websiteLanguage)
        {
            try
            {
                if (!BLL_User.CheckNameUnicityEmail(email, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"البريد الإلكتروني {email} مستعمل.");
                        default: return new JsonResult($"L'email {email} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
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
                User currentUser = BLL_User.SelectById(id);
                if (!currentUser.Name.Equals(name) && !BLL_User.CheckNameUnicityName(name, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"إسم {name} مستعمل.");
                        default: return new JsonResult($"Le nom {name} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }
        // test l'unicite Email
        [Route("Validation/EditEmail")]
        [AcceptVerbs("Get", "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult EditEmailValidation(string email, long idOrganization,long id, WebsiteLanguage websiteLanguage)
        {
            try
            {
                User currentUser = BLL_User.SelectById(id);
                if (!currentUser.Email.Equals(email) && !BLL_User.CheckNameUnicityEmail(email, idOrganization))
                {
                    switch (websiteLanguage)
                    {
                        case WebsiteLanguage.Ar: return new JsonResult($"البريد الإلكتروني {email} مستعمل.");
                        default: return new JsonResult($"L'email {email} est déjà utilisé.");
                    }
                }
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }
        

    }
}
