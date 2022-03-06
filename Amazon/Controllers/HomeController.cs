using Amazon.EC2;
using Amazon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Amazon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetInstances());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IndexModel GetInstances()
        {
            IndexModel model = new IndexModel();

            List<string> instances = new List<string>() { "1","2"};

            AmazonEC2Client client = new AmazonEC2Client("insert", "insert", RegionEndpoint.EUWest1);

            var result = client.DescribeInstancesAsync().Result;

            foreach(var reservation in result.Reservations)
            {
                var tag = reservation.Instances.FirstOrDefault().Tags[0];

                instances.Add(tag.Value);
            }

            model.Instances = instances;

            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
