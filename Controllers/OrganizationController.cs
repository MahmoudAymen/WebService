using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSSGBOAdmin.Models.BLL;
using DSSGBOAdmin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSSGBOAdmin.Controllers
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
                    if (organization != null)
                    {
                        return Ok(organization);
                    }
                    else
                    {
                        return NotFound("Organisation introuvable.");
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Organization> Organizations = BLL_Organization.SelectAll();
                    return Ok(Organizations);
                }
                return NotFound("Paramètre invalide.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [Route("")]
        [HttpPost]
        public JsonResult Post([FromForm] Organization organization)
        {
            try
            {
                BLL_Organization.Add(organization);
                return Json(new { success = true, message = "Organisation ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Route("{id:long}")]
        [HttpPut]
        public JsonResult Put(long id,[FromBody] Organization organization)
        {
            try
            {
                BLL_Organization.Update(id,organization);
                return Json(new { success = true, message = "Organisation modifié avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // DELETE api/<OrganizationController>/5
        [Route("{id:long}")]
        [HttpDelete]
        public JsonResult Delete(long id)
        {
            try
            {
                BLL_Organization.Delete(id);
                return Json(new { success = true, message = "Organisation supprimé avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
