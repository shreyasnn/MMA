using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularTest.Models;
using AngularTest.DAL;

namespace AngularTest.Controllers
{
    

    public class HomeController : Controller
    {
        private mmsdbEntities db = new mmsdbEntities();

        MMA_Model Model = new MMA_Model();
        public ActionResult Index()
        {
            //var model = db.MMS_Beobachter.Where(x => x.ID >= 1 && x.Name.Contains("a"));
            //var result = model.ToList();
            return View();
        }
        public ActionResult appcomponent()
        {
            return PartialView();
        }
       
        public ActionResult ObserverList()
        {
            return PartialView();
        }
        public ActionResult AreaList()
        { 
            return PartialView();
        }
        public ActionResult TimeList()
        {
            return PartialView();
        }
        public ActionResult EmployeeList()
        {
            return PartialView();
        }
        public ActionResult SelectedEmployeeList()
        {
            return PartialView();
        }
        public ActionResult EmployeeActivitiesList()
        {
            return PartialView();
        }       
       public ActionResult EmployeeSubActivitiesList()
        {
            return PartialView();
        }
        public ActionResult MachineLineList()
        {
            return PartialView();
        }
        public ActionResult MachinesList()
        {
            return PartialView();
        }
        public ActionResult MachinesStateList()
        {
            return PartialView();
        }
            public ActionResult SubmitTime()
        {
            return PartialView();
        }
        public ActionResult Addentry()
        {
            return PartialView();
        }

        public ActionResult Report()
        {
            return PartialView();
        }
      
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}