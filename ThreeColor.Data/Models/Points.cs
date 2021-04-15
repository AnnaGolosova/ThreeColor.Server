using System;
using System.Collections.Generic;

namespace ThreeColor.Data.Models
{
    public class Points
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Color { get; set; }
        public string Key { get; set; }
        public int IsDeleted { get; set; }

        public virtual Tests Test { get; set; }
        public virtual IEnumerable<Results> Resilts { get; set; }
    }
}
