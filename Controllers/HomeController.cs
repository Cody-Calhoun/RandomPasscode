using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        public Random rand = new Random();

        public string Generate()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string code = "";

            for(int i = 0; i < 14; i++)
            {
                code += chars[rand.Next(36)];
            }

            return code;
        }
        [HttpGet("")]
        public IActionResult Index ()
        {

            if(HttpContext.Session.GetString("Passcode") == null)
            {
        
                HttpContext.Session.SetString("Passcode", Generate());
                HttpContext.Session.SetInt32("Counter", 1);
            }

            ViewBag.Passcode = HttpContext.Session.GetString("Passcode");
            ViewBag.Counter = HttpContext.Session.GetInt32("Counter");
            return View("Index");
        }

        [HttpPost("generate")]

        public IActionResult NewCode()
        {
            if(HttpContext.Session.GetString("Passcode") == null)
            {
                HttpContext.Session.SetString("Passcode", Generate());
                HttpContext.Session.SetInt32("Counter", 1);
            }
            else
            {
                int? counter = HttpContext.Session.GetInt32("Counter");
                counter++;
                HttpContext.Session.SetString("Passcode", Generate());
                HttpContext.Session.SetInt32("Counter", (int)counter);
            }
            return RedirectToAction("Index");
        }
    }
}