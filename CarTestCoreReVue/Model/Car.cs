using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarTestCoreReVue.Model
{
    public class Car:BaseModel
    {
        public string Plate { get; set; }

        public string ModelName { get; set; }
        
        public int CompanyId { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }

        [XmlIgnore]
        public string Type { get;} = "car";
    }
}
