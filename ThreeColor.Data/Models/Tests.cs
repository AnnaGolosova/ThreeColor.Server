using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeColor.Data.Models
{
    public class Tests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FieldColor { get; set; }
        public int PointSize { get; set; }
        public int Speed { get; set; }
        public int MinInterval { get; set; }
        public int MaxInterval { get; set; }
        public int IsDeleted { get; set; }

        /// <summary>
        /// field widht in centimeters
        /// </summary>
        public double FieldWidth { get; set; }

        /// <summary>
        /// field height in centimeters
        /// </summary>
        public double FieldHeight { get; set; }
    }
}
