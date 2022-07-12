using Newtonsoft.Json;

namespace BattleshipApi.Models.Response;

public record PlayerInformation
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
}

public record CreateGameResponse
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("players")] public IEnumerable<PlayerInformation>? Players { get; set; }
}