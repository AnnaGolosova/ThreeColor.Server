using System;
using ThreeColor.Models.Enums;

namespace ThreeColor.IdentityService.Data.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public Activity Activity { get; set; }
        public Gender Gender { get; set; }
    }
}
