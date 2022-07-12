﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleshipApi.Models.Request;

public class NewGameRequest
{
    [JsonProperty]
    [Required]
    public string[] Players { get; set; }
}