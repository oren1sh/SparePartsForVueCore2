using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarTestCoreReVue.Model
{
    public class Company:BaseModel
    {
        public string Name { get; set; }

        public int CountryId { get; set; }
        [JsonIgnore]
        public virtual Country Country { get; set; }

        public List<Car> Cars { get; set; }

        [XmlIgnore]
        public string Type { get;} = "company";
    }
}
