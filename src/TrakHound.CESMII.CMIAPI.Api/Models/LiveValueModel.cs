using System;

namespace TrakHound.CESMII.CMIAPI.Api.Models
{
    public class LiveValueModel
    {
        public string Value { get; set; }

        public string ElementId { get; set; }
        public string DataType { get; set; }
        public int Quality { get; set; }
        public DateTime Timestamp { get; set; }

        public string Interpolation { get; set; }
        public string EngUnits { get; set; }
        public string NamespaceUri { get; set; }
        public string ParentId { get; set; }
        public string AttributeMetadata { get; set; }
    }
}
