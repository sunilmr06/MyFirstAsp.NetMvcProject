using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EmployeeModelViewUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EmployeeModelViewUI.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseURL = new Uri("https://localhost:7136/api");
        private readonly HttpClient client;

        public EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseURL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeViewModel> employes = new List<EmployeViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employes = JsonConvert.DeserializeObject<List<EmployeViewModel>>(data);
            }

            int slno = 1;
            foreach (var msd in employes)
            {
                msd.SlNo = slno++;
            }

            return View(employes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateData(); // Call this method to populate the dropdown
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Employee/Add", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMassage"] = "Employe details added"; 
                    return RedirectToAction("Index");
                }
                else
                {
                   
                    TempData["errorMassage"] = "Error adding employee" + response.ReasonPhrase; 
                    PopulateData();  // Repopulate in case of error
                    return View(model); // Return the model to the view so the user doesn't lose their data.
                }
            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = "Something went wrong: " + ex.Message;
                PopulateData(); 
                return View(model);  
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EmployeViewModel employes = new EmployeViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employes = JsonConvert.DeserializeObject<EmployeViewModel>(data);
            }

            PopulateData(employes.DepartmentId); // Select correct department for Edit
            return View(employes);
        }

        [HttpPost]
        public IActionResult Edit(EmployeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Employee/Update/" + model.EmployeeId, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    //TempData["successMassage"] = "Employe details Update";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMassage"] = "Error updating employee: " + response.ReasonPhrase;
                    PopulateData(model.DepartmentId); // Repopulate with selected value
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = "Something went wrong: " + ex.Message;
                PopulateData(model.DepartmentId);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            EmployeViewModel employes = new EmployeViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employes = JsonConvert.DeserializeObject<EmployeViewModel>(data);
            }

            return View(employes);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Employee/Delete/" + id).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMassage"] = "Error deleting employee: " + response.ReasonPhrase; // Add Error Massage Here
                    return View(); 
                }
            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = "Something went wrong: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            EmployeViewModel employes = new EmployeViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employes = JsonConvert.DeserializeObject<EmployeViewModel>(data);
            }

            return View(employes);
        }

        // Helper method to populate the Departments dropdown list
        private void PopulateData(int? selectedDepartmentId = null, int? selectedDesignationId = null)
        {
            List<DepartmentViewModel> departments = new List<DepartmentViewModel>();
            List<Designation> designations = new List<Designation>();

            // Get the Department data
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(data);
            }
            else
            {
                departments = new List<DepartmentViewModel>();
            }

            // Get the Designation data
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress + "/Designation/Get").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                designations = JsonConvert.DeserializeObject<List<Designation>>(data);
            }
            else
            {
                designations = new List<Designation>();
            }

            // Set the Department list for the dropdown
            ViewBag.DepartmentList = new SelectList(departments, "DepartmentId", "DepartmentName", selectedDepartmentId);

            // Set the Designation list for the dropdown
            ViewBag.DesignationList = new SelectList(designations, "DesignationId", "DesignationName", selectedDesignationId);
        }

    }
}