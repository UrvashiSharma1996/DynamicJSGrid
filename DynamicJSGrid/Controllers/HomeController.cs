using DynamicJSGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Principal;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace DynamicJSGrid.Controllers
{
    public class HomeController : Controller //Home controller class
    {
        private readonly AppDbContext context;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(AppDbContext context) // parameteraized constructor
        {
            this.context = context;
        }

        public async Task<IActionResult> Student()
        {
            var data = await context.Students.ToListAsync();
            return new JsonResult(data);
        }
        [HttpPost]
        public JsonResult Addstd(Student std)
        {
            var emp = new Student()
            {
                Name = std.Name,
                Rollno = std.Rollno,
                Section = std.Section,
                Branch = std.Branch,
                
            };
            context.Students.Add(emp);
            context.SaveChanges();
            return new JsonResult(emp);

        }

        public JsonResult Delete(int? id)
        {
            if (id == null || context.Students == null)
            {
                return new JsonResult("");
            }

            var stdData = context.Students.FirstOrDefault(s => s.Id == id);
            if (stdData == null)
            {
                return new JsonResult("");
            }
            return new JsonResult(stdData);


        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfimed(int? id)
        {
            var stdData = context.Students?.Find(id);
            if (stdData != null)
            {
                context.Students?.Remove(stdData);
            }
            context.SaveChanges();
            return new JsonResult(stdData);


        }

        [HttpPut]
        public ActionResult EditEmployee(int id, Student edited)
        {
            Student emp = context.Students.Find(id);

            if (emp == null) { return View(); }

            emp.Name = edited.Name;
            emp.Rollno = edited.Rollno;
            emp.Section = edited.Section;
            emp.Branch = edited.Branch;

            //context.Employees.Add(emp);
            //context.Update(emp);
            //context.Attach(emp);
            context.SaveChanges();
            return new JsonResult(edited);
        }








        public IActionResult Index() //Action method
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
