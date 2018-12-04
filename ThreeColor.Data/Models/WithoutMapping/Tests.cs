//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThreeColor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Tests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tests()
        {
            Point_Size = 0;
            Speed = 0;
            int_max = 0;
            int_min = 0;
            field_color = "FFFFFF";
            this.Points = new HashSet<Points>();
            Points.Add(new Models.Points()
            {
                color = "000000",
                Symbol = "A"
            });
        }

        [Key]
        public int id { get; set; }
        public string test_name { get; set; }
        public string field_color { get; set; }
        public Nullable<int> Point_Size { get; set; }
        public Nullable<int> Speed { get; set; }
        public Nullable<int> int_min { get; set; }
        public Nullable<int> int_max { get; set; }

        public virtual ICollection<Points> Points { get; set; }
    }
}
