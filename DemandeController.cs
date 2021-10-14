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
        [Route("{idString}")]
        [HttpGet]
        public IActionResult Get(string idString)
        {
            try
            {

                if (long.TryParse(idString, out long id))
                {
                    Demande demande = BLL_Demande.SelectById(id);
                    if (demande != null && demande.ID > 0)
                    {
                        return Json(new { success = true, message = "Demand trouve", data = demande });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Demand pas trouve", data = demande });
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Demande> Demands = BLL_Demande.SelectAll();
                    if (Demands != null && Demands.Count > 0)
                    {
                        return Json(new { success = true, message = "Demands trouve", data = Demands });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Pas de Demands dans la base de données", data = Demands });
                    }
                }
                return Json(new { success = false, message = " Le paramètre de la request : (' " + idString + " ')  est invalide. " });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        //[ValidateAntiForgeryToken]
        [Route("")]
        [HttpPost]
        public JsonResult RegisterNewDemande(Demande demande)
        {
            try
            {
                demande.RegDemandDate = DateTime.Now.ToShortDateString();
                demande.RegDemandDecision = "attends";
                demande.RegDemandDecisionDate = null;
                demande.RegDecisionComments = null;
                BLL_Demande.Add(demande);
                return Json(new { success = true, message = "Ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        // PUT api/<DemandeController>/5
        [HttpPost("{id}")]
        public JsonResult ResponseDemande(int id, string OrganizationSystemPrefix, [FromBody] Demande demande)
        {
            try
            {
                demande.RegDemandDecisionDate = DateTime.Now.ToShortDateString();
                BLL_Demande.Update(id, demande, OrganizationSystemPrefix);
                return Json(new { success = true, message = "modifié avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

    }
}
