using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relevamiento.Models
{
    public class SynchronizeDataConfig
    {
        [PrimaryKey]
        public int ID { get; set; }
        public bool isSynchronized { get; set; }
        public DateTime lastSynchronized { get; set; }
        public bool isFirstTimeSynchronizedReady { get; set; }
    }

    public class GenericDataConfig
    {
        [PrimaryKey]
        public int ID { get; set; }
        public bool isSynchronized { get; set; }
        public DateTime lastSynchronized { get; set; }
    }
}
