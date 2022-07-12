using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public record CreateGameRequest
{
    [JsonProperty]
    [Required]
    [MinLength(1)]
    public string[]? Players { get; set; }
}