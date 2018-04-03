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

            var streamName = "DentonStream";
            var partitionKey = "PartitionKey";

            var putRecordRequest = new PutRecordRequest
            {
                StreamName = streamName,
                PartitionKey = partitionKey,
                Data = new MemoryStream(Encoding.UTF8.GetBytes("1"))
            };

            var putRecordResult = await amazonKinesisClient.PutRecordAsync(putRecordRequest);
        }
    }
}
