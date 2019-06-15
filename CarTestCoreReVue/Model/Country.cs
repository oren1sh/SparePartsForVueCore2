using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarTestCoreReVue.Model
{
    public class Country:BaseModel
    {
        public string Name { get; set; }

        public List<Company> Companies { get; set; }

        [XmlIgnore]
        public string Type { get;} = "country";
    }
}
