using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class NewGameRequest
{
    [JsonProperty]
    public List<string> Players { get; set; }
}