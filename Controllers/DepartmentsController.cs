using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AppOwnsData.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppOwnsData.Controllers
{
    public class DepartmentsController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            return View(list);
        }
    }
}
