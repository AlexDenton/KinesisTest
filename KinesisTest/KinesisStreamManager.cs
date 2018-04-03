using System.IO;
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
    }
}