using System.ComponentModel.DataAnnotations;
using BattleshipApi.Common.Enums;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class AddVesselRequest
{
    [Range(0, 10)]
    [JsonProperty("column")]
    [Required]
    public int Column { get; set; }
    
    [Range(0, 10)]
    [JsonProperty("row")]    
    [Required]
    public int Row { get; set; }
    
    [JsonProperty("orientation")]
    [Required]
    public VesselOrientation Orientation { get; set; }
    
    [Range(1, 10)]
    [JsonProperty("size")]
    [Required]
    public int Size { get; set; }
    
}