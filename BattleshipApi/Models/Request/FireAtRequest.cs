using System.ComponentModel.DataAnnotations;
using BattleshipApi.Common.Models;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class FireAtRequest
{
    [JsonProperty("coordinates")]
    [Required]
    public Coordinate Coordinates { get; set; }
}