using Newtonsoft.Json;
using System;

namespace ThreeColor.Models.ResponseModels
{
    public class StudentLoginResponseModel
    {
        public StudentLoginResponseModel()
        {
        }

        public StudentLoginResponseModel(string accessToken, Guid userId, string message, int responseCode)
        {
            AccessToken = accessToken;
            UserId = userId;
            Message = message;
            ResponseCode = responseCode;
        }

        public StudentLoginResponseModel(string message, int responseCode)
        {
            Message = message;
            ResponseCode = responseCode;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public int ResponseCode { get; set; }
    }
}
