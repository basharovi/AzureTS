using Microsoft.Azure.Cosmos.Table;
using System;
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
        public string Long { get; set; }
        public string Lat { get; set; }
        public string Head { get; set; }
        public string Speed { get; set; }
        public string Time { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            Name = properties["name"].StringValue;
            Long = properties["long"].StringValue;
            Lat = properties["lat"].StringValue;
            Head = properties["head"].StringValue;
            Speed = properties["speed"].StringValue;
            Time = properties["time"].StringValue;
        }
    }
}
