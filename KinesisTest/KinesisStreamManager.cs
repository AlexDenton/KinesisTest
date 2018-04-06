using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace KinesisTest
{
    public class KinesisStreamManager
    {
        private readonly IAmazonKinesis _AmazonKinesisClient;

        private readonly string _StreamName;

        public KinesisStreamManager(
            IAmazonKinesis amazonKinesisClient,
            string streamName)
        {
            _AmazonKinesisClient = amazonKinesisClient;
            _StreamName = streamName;
        }

        public async Task<PutRecordResponse> PutKinesisRecord(string data, string partitionKey)
        {
            var putRecordRequest = new PutRecordRequest
            {
                StreamName = _StreamName,
                PartitionKey = partitionKey,
                Data = new MemoryStream(Encoding.UTF8.GetBytes(data))
            };

            return await _AmazonKinesisClient.PutRecordAsync(putRecordRequest);
        }
        public async Task<PutRecordsResponse> PutKinesisRecords(IEnumerable<string> data, string partitionKey)
        {
            var putRecordsRequest = new PutRecordsRequest
            {
                StreamName = _StreamName,
                Records = data.Select(
                    d => new PutRecordsRequestEntry
                    {
                        Data = new MemoryStream(Encoding.UTF8.GetBytes(d)),
                        PartitionKey = partitionKey
                    }).ToList()
            };

            return await _AmazonKinesisClient.PutRecordsAsync(putRecordsRequest);
        }

        public async Task<DescribeStreamResponse> DescribeStream()
        {
            var describeStreamRequest = new DescribeStreamRequest
            {
                StreamName = _StreamName
            };

            return await _AmazonKinesisClient.DescribeStreamAsync(describeStreamRequest);
        }

        public async Task<DescribeLimitsResponse> DescribleStreamLimits()
        {
            return await _AmazonKinesisClient.DescribeLimitsAsync(new DescribeLimitsRequest());
        }

        public async Task<DescribeStreamSummaryResponse> DescribeStreamSummary()
        {
            return await _AmazonKinesisClient.DescribeStreamSummaryAsync(
                new DescribeStreamSummaryRequest
                {
                    StreamName = _StreamName
                });
        }
    }
}