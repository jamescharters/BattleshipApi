using Newtonsoft.Json;

namespace BattleshipApi.Models.Response;

public record FireAtCoordinatesResponse
{
    [JsonProperty("result")]
    public string Result { get; set; }
}