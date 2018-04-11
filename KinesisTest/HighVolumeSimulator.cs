using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Kinesis.Model;

namespace KinesisTest
{
    public class HighVolumeSimulator
    {
        private readonly KinesisStreamManager _KinesisStreamManager;

        public HighVolumeSimulator(KinesisStreamManager kinesisStreamManager)
        {
            _KinesisStreamManager = kinesisStreamManager;
        }

        public async Task Run()
        {
            var tasks = new List<Task<PutRecordsResponse>>();

            for (var i = 0; i < 100; i++)
            {
                var data = Enumerable.Range(0, 500).Select(j => j.ToString());

                var paritionKey = new Random().Next().ToString();
                var putRecordsResponseTask = _KinesisStreamManager.PutKinesisRecords(data, paritionKey);
                tasks.Add(putRecordsResponseTask);
            }

            var results = await Task.WhenAll(tasks);
        }
    }
}