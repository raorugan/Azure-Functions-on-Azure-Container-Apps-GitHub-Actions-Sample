using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobEventProj
{
    public class BlobEventApp
    {
        private readonly ILogger<BlobEventApp> _logger;

        public BlobEventApp(ILogger<BlobEventApp> logger)
        {
            _logger = logger;
        }

        [Function(nameof(BlobEventApp))]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Source = BlobTriggerSource.EventGrid, Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob Trigger (using Event Grid) is processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
