using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BattleshipApi.Models.Request;

public class NewGameRequest
{
    [JsonProperty]
    [Required]
    [MinLength(1)]
    public string[]? Players { get; set; }
}