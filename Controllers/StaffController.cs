using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using CoreDemoJSON.Services;
using CoreDemoJSON.Models;



namespace CoreDemoJSON.Controllers
{
    public class StaffController : Controller
    {
        //private readonly ILogger<StaffController> _logger;

        private readonly IStaffService _staffService;

  
        // public StaffController(ILogger<StaffController> logger)
        // {
        //     _logger = logger;
        // }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }


        // Constructor
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }


        // Add - BEGIN
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]        
        public IActionResult Add(StaffMember staffModel)
        {
            //Server-Side Validation
            if(ModelState.IsValid)
            {
                _staffService.SaveNewStaff(staffModel);
                return RedirectToAction("ListStaff");                
            }

            return View(staffModel);
        }
        // Add - END


        // ListStaff - BEGIN
        [HttpGet]
        public IActionResult ListStaff()
        {
            List<StaffMember>  staffList = _staffService.GetStaff();
            return View(staffList);
        }
        // ListStaff - END


        // Edit - BEGIN
        [HttpGet]
        public IActionResult Edit(int id)
        { 
            StaffMember staff = _staffService.FindStaffById(id);
            return View(staff);
        }


        [HttpPost]
        public IActionResult Edit(StaffMember staffModel)
        {     
            _staffService.UpdateStaff(staffModel);
            return RedirectToAction(nameof(ListStaff));
        }
        // Edit - END


        // Delete - BEGIN
        [HttpGet]
        public IActionResult Delete(int id)
        {            
            StaffMember staff = new StaffMember();            
            staff = _staffService.FindStaffById(id);
            return View(staff);
        }


        [HttpPost]
        [ActionName("Delete")] 
        public IActionResult DeletePost(int id) // Different name used since HttpGet has same signature. Note above we have "Delete" which is correct name!
        {
            bool rtn = _staffService.DeleteStaff(id);
            return RedirectToAction(nameof(ListStaff));
        }
        // Delete - END

    }
}