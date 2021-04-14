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
                    if (demande != null && demande.ID > 0)
                    {
                        return Json(new { success = true, message = "Demande trouvée", data = demande });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Demande pas trouvée", data = demande });
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Demande> Demands = BLL_Demande.SelectAll();
                    if (Demands != null && Demands.Count > 0)
                    {
                        return Json(new { success = true, message = "Demandes trouvée", data = Demands });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Demande pas trouvée", data = Demands });
                    }
                }
                return Json(new { success = false, message = "Paramètre : ' " + idString + " ' invalide. " });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Erreur serveur : " + e.Message });
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
                return Json(new { success = true, message = "Demande ajouté avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur serveur : " + ex.Message });
            }
        }



        // PUT api/<DemandeController>/5
        [HttpPost("{id}")]
        public JsonResult ResponseDemande(int id, [FromBody] Demande demande)
        {
            try
            {

                demande.RegDemandDecisionDate = DateTime.Now.ToShortDateString();
                BLL_Demande.Update(id, demande);
                return Json(new { success = true, message = "Demande modifié avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur serveur : " + ex.Message });
            }

        }
    }
}
