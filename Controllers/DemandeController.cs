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
                        return Json(new { success = true, message = "Demand found", data = demande });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Demand not found", data = demande });
                    }
                }
                else if (idString.ToLower().Equals("all"))
                {
                    List<Demande> Demands = BLL_Demande.SelectAll();
                    if (Demands != null && Demands.Count > 0)
                    {
                        return Json(new { success = true, message = "Demands found", data = Demands });
                    }
                    else
                    {
                        return Json(new { success = true, message = "No Demand in the database", data = Demands });
                    }
                }
                return Json(new { success = false, message = "Parameter request : ' " + idString + " ' invalide. "});
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error server " + e.Message});
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
                return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }



        // PUT api/<DemandeController>/5
        [HttpPost("{id}")]
        public JsonResult ResponseDemande(int id, [FromBody] Demande demande)
        {
            try
            {

                demande.RegDemandDecisionDate = DateTime.Now.ToShortDateString();
                BLL_Demande.Update(id,demande);
                return Json(new { success = true, message = "modifié avec success" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error server " + ex.Message });
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
                    throw new Exception("User Not Found, please check your email or password");

                return Json(new { success = true, message = "user found", data = userLogged });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
