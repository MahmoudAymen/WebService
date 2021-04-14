using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminServiceGBO.Models.BLL;
using AdminServiceGBO.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminServiceGBO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : Controller
    {

        [Route("{idString}")]
        [HttpGet]
        public IActionResult Get(string idString)
        {
            try
            {
                if (long.TryParse(idString, out long id))
                {
                    Organization organization = BLL_Organization.SelectById(id);
                    if (organization != null && organization.Id > 0)
                    {
                        return Json(new { success = true, message = "Organization found", data = organization });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Organization not found", data = organization });
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Organization> Demands = BLL_Organization.SelectAll();
                    return Json(new { success = true, message = "Organization found", data = Demands });
                }
                return Json(new { success = false, message = "Parameter request : ' " + idString + " ' invalide. " });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        [Route("add")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Post([FromForm] Organization organization)
        {
            try
            {
                BLL_Organization.Add(organization);
                return Json(new { success = true, message = "Ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        [Route("{id}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Put(string id, Organization organization)
        {
            try
            {
                BLL_Organization.Update(long.Parse(id),organization);
                return Json(new { success = true, message = "Modifié avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        // DELETE api/<OrganizationController>/5
        [Route("{id:long}")]
        [HttpDelete()]
        public JsonResult Delete(long id)
        {
            try
            {
                BLL_Organization.Delete(id);
                return Json(new { success = true, message = "Supprimé avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }



    }
}
