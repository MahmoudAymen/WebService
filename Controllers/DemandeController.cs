using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminServiceGBO.Models.BLL;
using AdminServiceGBO.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminServiceGBO.Controllers
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
                return Json(new { success = false, message = " Le paramètre de la request : (' " + idString + " ')  est invalide. "});
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message});
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
            catch(Exception ex)
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
                BLL_Demande.Update(id,demande, OrganizationSystemPrefix); 
                return Json(new { success = true, message = "modifié avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }


        //[ValidateAntiForgeryToken]
        [Route("/api/auth")]
        [HttpPost]
        public JsonResult Login(User user)
        {
            try
            {
                User userLogged = BLL_User.loggingUser(user.Email, user.PassWord);
                if (userLogged == null)
                    throw new Exception("Utilisateur pas trouve, verifiez vos informations");

                return Json(new { success = true, message = "Utilisateur trouve", data = userLogged });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
