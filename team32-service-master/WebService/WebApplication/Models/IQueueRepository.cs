using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public interface IQueueRepository
    {
        List<QueueModel> Queues();
        QueueModel GetQueue(int queueId);
        QueueModel UpdateQueue(int queueId, QueueModel queueModel);
        QueueModel AddQueue(QueueModel queueModel);
        QueueModel DeleteQueue(int queueId);
    }
}