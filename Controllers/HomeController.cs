using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureTrial.Models;
using BlobServiceClient;
using BlobContainerClient;
using BlobClient;
using DefaultAzureCredential;

namespace AzureTrial.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;

        // Create a BlobServiceClient that will authenticate through Active Directory
        Uri accountUri = new Uri("https://goobiedoobie123.blob.core.windows.net/");
        BlobServiceClient client = new BlobServiceClient(accountUri, new DefaultAzureCredential());

        Upload();
    }

    public IActionResult Index()
    {
        return View();
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

    public void Upload () {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=goobiedoobie123;AccountKey=6M5tWaoSuVZ0Q2bd9+30ltZUCRIWJVfogyGkoJc7+SwynVKFzR07cKgxtpiQGoO/WVhjkG2nu8eo+AStdpbLEw==;EndpointSuffix=core.windows.net";
        string containerName = "sams-house";
        string blobName = "samantha";
        string filePath = "../forms/cc219b.pdf";

        // Get a reference to a container named "sample-container" and then create it
        BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
        container.Create();

        // Get a reference to a blob named "sample-file" in a container named "sample-container"
        BlobClient blob = container.GetBlobClient(blobName);

        // Upload local file
        blob.Upload(filePath);
    }
    
}
