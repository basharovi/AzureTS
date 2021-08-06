using System;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

namespace AzureTS.API.Models
{
    public class SoloEntity : TableEntity
    {
        public SoloEntity()
        {
            PartitionKey = nameof(SoloEntity);
            RowKey = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string Head { get; set; }
        public double Speed { get; set; }
        public string Time { get; set; }

        public override void ReadEntity(IDictionary<string, 
            EntityProperty> properties, OperationContext operationContext)
        {

            Name = properties["name"].StringValue;
            Lng = (double)properties["long"].DoubleValue;
            Lat = (double)properties["lat"].DoubleValue;
            Head = properties["head"].ToString();
            Speed = (double)properties["speed"].DoubleValue;
            Time = properties["time"].StringValue;
        }
    }
}
