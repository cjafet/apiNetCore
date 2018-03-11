using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/campaign")]
    public class CampaignController : Controller
    {


        CampaignDb context = new CampaignDb();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult GetCampaign()
        {
            List<CampaignDto> campaigns = context.Campaign.ToList();
            return Json(new { campaigns });
        }
            
    }
}