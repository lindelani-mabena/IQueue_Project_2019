using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.sun.org.apache.bcel.@internal.generic;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class QueueRepository: IQueueRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public QueueRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(_db);
        }

        public List<QueueModel> Queues()
        {
            var queueModels = new List<QueueModel>();
            var queues = _db.Queues;

            foreach (var queue in queues)
            {
                queueModels.Add((QueueModel)
                    _modelFactory.CreateQueueModel(queue));
            }

            return queueModels;
        }

        public QueueModel GetQueue(int queueId)
        {
            var queue = _db.Queues.FirstOrDefault(x => x.Queue_Id == queueId);
            if (queue == null) return null;
            return (QueueModel) _modelFactory.CreateQueueModel(queue);
        }

        public QueueModel UpdateQueue(int queueId, QueueModel queueModel)
        {
            var queue = _db.Queues.FirstOrDefault(x => x.Queue_Id == queueId);

            if (queue == null) return null;

            queue.Service_Id = queueModel.ServiceId;
            queue.Status = queue.Status;
            queue.Average_Waiting_Time = queue.Average_Waiting_Time;
            queue.Last_Update = queueModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (QueueModel) _modelFactory.CreateQueueModel(queue);
        }

        public QueueModel AddQueue(QueueModel queueModel)
        {
            var queue = new Queue()
            {
                Service_Id = queueModel.ServiceId,
                Status = queueModel.Status,
                Average_Waiting_Time = queueModel.AverageWaitingTime,
                Last_Update = queueModel.LastUpdate,
                Initial_Date = queueModel.InitialDate
            };

            _db.Queues.InsertOnSubmit(queue);
            _db.SubmitChanges();

            return (QueueModel) _modelFactory.CreateQueueModel(queue);
        }

        public QueueModel DeleteQueue(int queueId)
        {
            var queue = _db.Queues.FirstOrDefault(x => x.Queue_Id == queueId);

            if (queue == null) return null;

            _db.Queues.DeleteOnSubmit(queue);
            _db.SubmitChanges();

            return (QueueModel) _modelFactory.CreateQueueModel(queue);
        }
    }
}