using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestCoreReVue.Model
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
