﻿using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Kinesis;
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

            var userNotificationSimulator = new UserNotificationSimulator(kinesisStreamManager);
            await userNotificationSimulator.Run();
        }
    }
}
