using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeColor.Data.Models
{
    public class Results
    {
        public Guid Id { get; set; }
        public int TestingNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid PointId { get; set; }
        public int Time { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public DateTime Date { get; set; }

        public virtual Points Point { get; set; }
        public virtual Users User { get; set; }
    }
}
