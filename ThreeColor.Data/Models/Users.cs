using System;
using System.Collections.Generic;

namespace ThreeColor.Data.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Activity { get; set; }
        public string Gender { get; set; }

        public virtual IEnumerable<Results> Results { get; set; }
    }
}
