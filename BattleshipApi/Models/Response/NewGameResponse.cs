using Newtonsoft.Json;

namespace BattleshipApi.Models.Response;

public class PlayerInformation
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
}

public class NewGameResponse
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("players")] public IEnumerable<PlayerInformation>? Players { get; set; }
}