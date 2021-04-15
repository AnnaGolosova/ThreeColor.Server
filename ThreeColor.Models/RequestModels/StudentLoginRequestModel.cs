using ThreeColor.Models.Enums;

namespace ThreeColor.Models.RequestModels
{
    public class StudentLoginRequestModel
    {
        public int Age { get; set; }
        public Activity Activity { get; set; }
        public Gender Gender { get; set; }
    }
}
