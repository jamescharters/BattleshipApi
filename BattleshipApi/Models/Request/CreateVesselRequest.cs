using System.ComponentModel.DataAnnotations;
using BattleshipApi.Common.Enums;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public record CreateVesselRequest
{
    [Range(0, 9)]
    [JsonProperty("column")]
    [Required]
    public int Column { get; set; }
    
    [Range(0, 9)]
    [JsonProperty("row")]    
    [Required]
    public int Row { get; set; }
    
    [JsonProperty("orientation")]
    [Required]
    public VesselOrientation Orientation { get; set; }
    
    [Range(1, 5)]
    [JsonProperty("size")]
    [Required]
    public int Size { get; set; }
}