using System.Reflection;
using System.Text;
using EmployeeModelViewUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeModelViewUI.Controllers
{
    public class DepartmentController : Controller
    {
        Uri baseURL = new Uri("https://localhost:7136/api");
        private readonly HttpClient client;
        public DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = baseURL;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<DepartmentViewModel> departments = new List<DepartmentViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/Get").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(data);
            }
            int slno = 1;
            foreach (var msd in departments)
            {
                msd.SlNo = slno++;
            }

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Department/Post", content).Result;
                if (response.IsSuccessStatusCode)
                {
                   // TempData["successMassage"] = "Employe details added";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                //TempData["errorMassage"] = " something went wrong";
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            DepartmentViewModel department = new DepartmentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/Get/" +id).Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentViewModel>(data);
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Delete(int id)
         {
            DepartmentViewModel department = new DepartmentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/Get/" + id).Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentViewModel>(data);
            }
            return View(department);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "Department/Delete/" + id).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
               
            }
            catch (Exception ex)
            {

                return View();
            }
            return View();
        }

    }
}

