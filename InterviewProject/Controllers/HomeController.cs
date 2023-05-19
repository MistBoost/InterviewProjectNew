using InterviewProject.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace InterviewProject.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private List<EmployeeTimeWorked> Data;
        private string APIEndpoint = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            var json = Get(APIEndpoint);
            var data = JsonSerializer.Deserialize<List<TimeEntry>>(json);
            DataLogicConverter dlc = new DataLogicConverter(data);
            Data = dlc.Result;
        }

        public IActionResult Index()
        {
            if (Data == null) return View();
            return View(Data);
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

        private string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}