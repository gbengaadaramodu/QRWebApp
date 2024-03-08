using Microsoft.AspNetCore.Mvc;
using QRWebApp.Models;
using System.Diagnostics;
using System.Text;
using Net.Codecrete.QrCodeGenerator;
using Dapper;
using Azure;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QRWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(CountDto totalCount)
        {

            try
            {
                if (totalCount.DataCount > 0)
                {
                    int eachCount = 1;
                    while (eachCount <= totalCount.DataCount)
                    {
                        var prefix = "NOU";
                        Random rnd = new();
                        var num = rnd.Next(1000, 9999);

                        string trackno = $"https://nou.edu.ng/verify?student={prefix}{num}{eachCount}&data={totalCount.Data}";
                       // var response = GenerateQRCode(trackno);
                        var response = GenerateQRCode(totalCount.Data);

                        //save in the db
                        using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnections").ToString()))// (var db = new DatabaseContext())
                        {
                            db.Open();
                            var product = new QRProduct() { PictureUrl = response };

                            var sql = $"INSERT INTO Products (PictureUrl) VALUES ('{response}')";

                            await db.ExecuteAsync(sql);
                            // db. .Add(product);

                            db.Close();
                        }

                        eachCount += 1;
                    }
                }


                ViewBag.Msg = "QR Code generated Successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Error occured";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            //save in the db
            var sql = string.Format("SELECT * FROM Products");
            IEnumerable<QRProduct> products;
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnections").ToString()))
            {
                db.Open();
                products = db.Query<QRProduct>(sql);
                db.Close();
            }

            ViewBag.QRCode = products;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public string GenerateQRCode(string trackno)
        {
            var fileName = Guid.NewGuid().ToString();
            fileName += ".svg";
            string fullPath = Directory.GetCurrentDirectory() + _configuration["document_upload"] + fileName;
            string directory = Directory.GetCurrentDirectory() + _configuration["document_upload"];

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

           // byte[] file = null;
          //  var qr = QrCode.EncodeBinary(file, QrCode.Ecc.Medium);
            var qr = QrCode.EncodeText(trackno, QrCode.Ecc.Medium);
            string svg = qr.ToSvgString(4);
            FileStream stream = null;
            stream = new FileStream(fullPath, FileMode.Create);
            // Create a StreamWriter from FileStream
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine(svg);
                writer.Close();
            }
            stream.Close();


          //  var imagefullpath =  string.Concat(_httpContextAccessor.HttpContext.Request.Scheme, "://", _httpContextAccessor.HttpContext.Request.Host, $"/applicantdocuments/{fileName}");

            return fileName;
        }
    }
}