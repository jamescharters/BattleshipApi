using BattleshipApi.BusinessLogic.Models;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class FireAtRequest
{
    [JsonProperty] 
    public Guid PlayerId { get; set; }
    
    [JsonProperty]
    public Coordinate Coordinates { get; set; }
}