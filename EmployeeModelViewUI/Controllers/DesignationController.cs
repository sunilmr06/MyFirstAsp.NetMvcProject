using System.Text;
using EmployeeModelViewUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeModelViewUI.Controllers
{
    public class DesignationController : Controller
    {
        Uri baseURL = new Uri("https://localhost:7136/api");
        private readonly HttpClient client;

        public DesignationController()
        {
            client = new HttpClient();
            client.BaseAddress = baseURL;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Designation> designations = new List<Designation>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Designation/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                designations = JsonConvert.DeserializeObject<List<Designation>>(data);
            }
            int slno = 1;
            foreach (var msd in designations)
            {
                msd.SlNo = slno++;
            }

            return View(designations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Designation model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Designation/Post", content).Result;
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
            Designation department = new Designation();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Designation/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<Designation>(data);
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Designation department = new Designation();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Designation/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<Designation>(data);
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "Designation/Delete/" + id).Result;

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
