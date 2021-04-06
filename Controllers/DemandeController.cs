using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSSGBOAdmin.Models.BLL;
using DSSGBOAdmin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSSGBOAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : Controller
    {
        [Route("{idString}")]
        [HttpGet]
        public IActionResult Get(string idString)
        {
            try
            {
                if (long.TryParse(idString, out long id))
                {
                    Demande demande = BLL_Demande.SelectById(id);
                    if (demande != null)
                    {
                        return Ok(demande);
                    }
                    else
                    {
                        return NotFound("Demande introuvable.");
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Demande> Demands = BLL_Demande.SelectAll();
                    return Ok(Demands);
                }
                return NotFound("Paramètre invalide.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Post([FromForm] Demande demande)              
        {
            try
            {
                BLL_Demande.Add(demande);
                return Json(new { success = true, message = "Ajouté avec success" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // PUT api/<DemandeController>/5
        [HttpPost("{id}")]
        public JsonResult Put(int id, [FromForm] Demande demande)
        {
            try
            {         
                BLL_Demande.Add(demande);
                return Json(new { success = true, message = "Ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        // DELETE api/<DemandeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
