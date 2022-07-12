using BattleshipApi.Common.Enums;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Response;

public class FireAtResponse
{
    [JsonProperty("result")]
    public string Result { get; set; }
}