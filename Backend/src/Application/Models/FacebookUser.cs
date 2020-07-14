using Newtonsoft.Json;

namespace Application.Models
{
    public class FacebookUser
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
