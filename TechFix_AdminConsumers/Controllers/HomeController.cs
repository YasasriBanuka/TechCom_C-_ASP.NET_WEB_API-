using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TechCom.Models;

namespace TechFix_AdminConsumers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult AdminLogin()
        {
            return View();
        }

        // Admin Login (POST)
        [HttpPost]
        public ActionResult AdminLogin(string username, string password)
        {
            // Hardcoded admin credentials
            string adminUsername = "admin";
            string adminPassword = "ad_11";

            // Validate the credentials
            if (username == adminUsername && password == adminPassword)
            {
                Session["IsAuthenticated"] = true;
                return RedirectToAction("GetSuppliers"); // Redirect to GetSuppliers page after successful login
            }
            else
            {
                ViewBag.Message = "Invalid credentials. Please try again.";
                return View();
            }
        }

        // VIEW AND MANAGE SUPPLIER DETAILS 

        // Retrieve the list of suppliers from the API
        public async Task<ActionResult> GetSuppliers()
        {
            List<Supplier> supplierList = new List<Supplier>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Suppliers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    supplierList = JsonConvert.DeserializeObject<List<Supplier>>(apiResponse);
                }
            }

            return View(supplierList);
        }

        // Delete a supplier by ID using the API
        [HttpGet]
        public async Task<ActionResult> DeleteSuppliers(int id)
        {
            Supplier supplier = new Supplier();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:44362/api/Suppliers/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<Supplier>(apiResponse);
                }
            }

            return RedirectToAction("GetSuppliers");
        }

        //VIEW ALL QUOTATION

        public async Task<ActionResult> GetQuotation()
        {
            List<Quotation> reservationList = new List<Quotation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Quotations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Quotation>>(apiResponse);
                }
            }
            return View(reservationList);
        }
        public async Task<ActionResult> AddOrder()
        {
            return View();
        }


        //ORDER QUOTATION 
        //----------------------
        [HttpPost]

        public async Task<ActionResult> AddOrder(Order pr)
        {
            Order p = new Order();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44362/api/Orders", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Order>(apiResponse);
                }
            }
            return RedirectToAction("GetQuotation");
        }


        public async Task<ActionResult> RequsetInventory()
        {
            return View();
        }

        //----------------------
        [HttpPost]

        public async Task<ActionResult> RequsetInventory(Order pr)
        {
            Inventory p = new Inventory();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44362/api/Inventories", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Inventory>(apiResponse);
                }
            }
            return RedirectToAction("RequsetInventory");
        }

    }
}
