using BattleshipApi.Common.Enums;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Response;

public class FireAtResponse
{
    [JsonProperty("result")]
    public FireResult Result { get; set; }
}