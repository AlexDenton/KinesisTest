using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KinesisTest
{
    public class UserNotificationSimulator
    {
        private readonly TimeSpan _DelayInterval = TimeSpan.FromSeconds(1);

        private readonly Random _RandomNumberGenerator = new Random();

        private readonly KinesisStreamManager _KinesisStreamManager;

        public UserNotificationSimulator(KinesisStreamManager kinesisStreamManager)
        {
            _KinesisStreamManager = kinesisStreamManager;
        }

        public async Task Run()
        {
            while (true)
            {
                var userId = _RandomNumberGenerator.Next(0, 100);
                await _KinesisStreamManager.PutKinesisRecord("Notification", userId.ToString());
                await Task.Delay(_DelayInterval);
            }
        }
    }
}