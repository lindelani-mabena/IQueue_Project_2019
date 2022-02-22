using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    [DataContract]
    public abstract class Model: IModel
    {
        public const int NoId = -1;
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }

        protected Model()
        {
            InitialDate = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        [DataType(DataType.Date)]
        public DateTime GetInitialDate()
        {
            return InitialDate;
        }

        [DataType(DataType.Time)]
        public DateTime GetInitialTime()
        {
            return InitialDate;
        }
        [DataType(DataType.Date)]
        public DateTime GetLastUpdateDate()
        {
            return LastUpdate;
        }

        [DataType(DataType.Time)]
        public DateTime GetLastUpdateTime()
        {
            return LastUpdate;
        }
        public DateTime UpdateLastUpdate()
        {
            LastUpdate = DateTime.Now;
            return LastUpdate;
        }

        public void Inspect()
        {
            Console.WriteLine("Needs Implementation");
        }
    }
}