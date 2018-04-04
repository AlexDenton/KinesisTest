using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Microsoft.Extensions.Configuration;

namespace KinesisTest
{
    class Program
    {
        private static IConfiguration Configuration;

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            Run().Wait();
        }

        private static async Task Run()
        {
            var options = Configuration.GetAWSOptions();
            var amazonKinesisClient = options.CreateServiceClient<IAmazonKinesis>();

            var kinesisStreamManager = new KinesisStreamManager(amazonKinesisClient, "DentonStream");
            var partitionKey = "PartitionKey";

            var putRecordResult = await kinesisStreamManager.PutKinesisRecord("1", partitionKey);
        }
    }
}
