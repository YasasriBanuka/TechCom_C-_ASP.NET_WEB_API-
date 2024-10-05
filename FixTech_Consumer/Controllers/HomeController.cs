using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechCom.Models;

namespace FixTech_Consumer.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult StaffLogin()
        {
            return View();
        }

        // Admin Login (POST)
        [HttpPost]
        public ActionResult StaffLogin(string username, string password)
        {
            // Hardcoded admin credentials
            string Username = "supplier";
            string Password = "sup_11";

            // Validate the credentials
            if (username == Username && password == Password)
            {
                Session["IsAuthenticated"] = true;
                return RedirectToAction("GetProducts"); // Redirect to GetSuppliers page after successful login

            }
            else
            {
                ViewBag.Message = "Invalid credentials. Please try again.";
                return View();
            }
        }

        public async Task<ActionResult> GetProducts()
        {
            List<TechCom.Models.Product> reservationList = new List<TechCom.Models.Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        ///------------------

        // ADD PRODUCT CODE  

        public async Task<ActionResult> AddProduct()
        {
            return View();
        }

        //----------------------
        [HttpPost]

        public async Task<ActionResult> AddProduct(Product pr)
        {
            Product p = new Product();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44362/api/Products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return RedirectToAction("GetProducts");
        }


        //-------------------------------------------------------------------------------------------------------------------------------------

        // UPDATE PRODUCT
        public async Task<ActionResult> UpdateProduct(string id)
        {
            Product product = new Product();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"https://localhost:44362/api/Products/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            product = JsonConvert.DeserializeObject<Product>(apiResponse);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error fetching product details.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(product);
        }
//----------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product pr)
        {
            if (!ModelState.IsValid)
            {
                return View(pr);
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"https://localhost:44362/api/Products/{pr.ProductID}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Optionally, redirect or provide a success message
                            return RedirectToAction("GetProducts");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error updating the product.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(pr);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product p = new Product();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"https://localhost:44362/api/Products/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Product>(apiResponse);
                return RedirectToAction("GetProducts");
            }
        }



        //QUOTATION


        public async Task<ActionResult> GetQuotations()
        {
            List<TechCom.Models.Quotation> reservationList = new List<TechCom.Models.Quotation>();

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

        //------------------------
        // ADD NEW QUOTATION 
        public async Task<ActionResult> AddQuotation()
        {
            return View();
        }

        //--------------------------

        [HttpPost]

        public async Task<ActionResult> AddQuotation(Quotation qr)
        {
            Quotation q = new Quotation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(qr), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44362/api/Quotations", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Quotation>(apiResponse);
                }
            }
            return RedirectToAction("GetQuotations");
        }
        //----------------------------------

        //UPDATE QUOTATION 

        public async Task<ActionResult> UpdateQuotation(string id)
        {
            Quotation qua = new Quotation();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"https://localhost:44362/api/Quotations/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            qua = JsonConvert.DeserializeObject<Quotation>(apiResponse);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error fetching product details.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(qua);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateQuotation(Quotation pr)
        {
            if (!ModelState.IsValid)
            {
                return View(pr);
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"https://localhost:44362/api/Quotations/{pr.QuoteID}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Optionally, redirect or provide a success message
                            return RedirectToAction("GetQuotations");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error updating the quotation.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(pr);

        }

        [HttpGet]
        public async Task<ActionResult> DeleteQuotation(int id)
        {
            Quotation p = new Quotation();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"https://localhost:44362/api/Quotations/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Quotation>(apiResponse);
                return RedirectToAction("GetQuotations");
            }
        }

        //--------------------

        public async Task<ActionResult> GetInventries()
        {
            List<TechCom.Models.Inventory> reservationList = new List<TechCom.Models.Inventory>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Inventories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Inventory>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            Inventory p = new Inventory();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"https://localhost:44362/api/Inventories/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Inventory>(apiResponse);
                return RedirectToAction("GetInventries");
            }
        }


        //---------------------------------

        //VIEW ORDER CODE

        public async Task<ActionResult> GetOrders()
        {
            List<TechCom.Models.Order> reservationList = new List<TechCom.Models.Order>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Orders"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Order>>(apiResponse);
                }
            }
            return View(reservationList);
        }


        [HttpGet] 
        public async Task<ActionResult> DeleteOrder(int id)
        {
            Order p = new Order();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"https://localhost:44362/api/Orders/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Order>(apiResponse);
                return RedirectToAction("GetOrders");
            }
        }








    }
}