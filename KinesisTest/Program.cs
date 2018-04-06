using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var tasks = new List<Task<PutRecordsResponse>>();

            for (var i = 0; i < 100; i++)
            {
                var data = Enumerable.Range(0, 500).Select(j => j.ToString());

                var randomPartitionKey = new Random().Next().ToString();
                var putRecordsResponseTask = kinesisStreamManager.PutKinesisRecords(data, randomPartitionKey);
                tasks.Add(putRecordsResponseTask);
            }

            var results = await Task.WhenAll(tasks);
        }
    }
}
