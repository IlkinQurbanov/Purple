﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;

namespace Purple.Controllers
{
    public class AboutController : Controller
    {
      

        public async Task<IActionResult> Index()
        {
          

            return View();
        }
    }
}
