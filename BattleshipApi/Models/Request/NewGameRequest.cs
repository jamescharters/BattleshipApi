using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class NewGameRequest
{
    [JsonProperty]
    [Required]
    public List<string> Players { get; set; }
}