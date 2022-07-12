using System.ComponentModel.DataAnnotations;
using BattleshipApi.Common.Models;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public record FireAtCoordinatesRequest
{
    [JsonProperty("coordinates")]
    [Required]
    public CartesianCoordinates? Coordinates { get; set; }
}