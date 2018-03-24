using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Models;
using System.IO;
using System.Dynamic;

namespace WebApi.Controllers
{
    [Route("api/campaign")]
    public class CampaignController : Controller
    {


        CampaignDb context = new CampaignDb();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult GetCampaign()
        {
            List<CampaignDto> campaigns = context.Campaign.ToList();
            return Json(new { campaigns });
        }

        [HttpPost()]
        public void Post([FromBody]dynamic value)
        {
            string dataAsJson = JsonConvert.SerializeObject(value);
            byte[] dataAsBytes = Encoding.UTF8.GetBytes(dataAsJson);
            using (MemoryStream memoryStream = new MemoryStream(dataAsBytes))
            {
                try
                {
                    AmazonKinesisConfig config = new AmazonKinesisConfig();
                    config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
                    AmazonKinesisClient kinesisClient = new AmazonKinesisClient(config);
                    String kinesisStreamName = "click-stream";

                    PutRecordRequest requestRecord = new PutRecordRequest();
                    requestRecord.StreamName = kinesisStreamName;
                    requestRecord.PartitionKey = "temp";
                    requestRecord.Data = memoryStream;

                    kinesisClient.PutRecordAsync(requestRecord);
                    //Console.WriteLine("Successfully sent record {0} to Kinesis. Sequence number: {1}", wt.Url, responseRecord.SequenceNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send record to Kinesis. Exception: {0}", ex.Message);
                }
            }
        }
            
    }
}