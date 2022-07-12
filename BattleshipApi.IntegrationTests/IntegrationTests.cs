using System.Net.Http.Json;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;
using BattleshipApi.Models.Request;
using BattleshipApi.Models.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace BattleshipApi.IntegrationTests;

public class IntegrationTests
{
    private const string API_ROOT = "battleship/v1/game";

    [Test]
    public async Task it_should_create_game()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(httpClient);

        Assert.AreNotEqual(Guid.Empty, newGameResponse.Id);
        Assert.AreEqual(1, newGameResponse.Players.Count());
        Assert.AreNotEqual(Guid.Empty, newGameResponse.Players.First().Id);
        Assert.AreEqual("James", newGameResponse.Players.First().Name);
    }

    [Test]
    public async Task it_should_get_player_game_board()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(httpClient);

        var getResponse =
            await httpClient.GetAsync(
                $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/board");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();

        Assert.IsFalse(string.IsNullOrWhiteSpace(getResponseContent));
    }

    [Test]
    public async Task it_should_add_vessel()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(httpClient);

        var playerId = newGameResponse.Players.First().Id;

        var createVesselResponse = await createVessel(httpClient, newGameResponse.Id,
            playerId, new CartesianCoordinates(2, 2), 5);

        Assert.AreNotEqual(Guid.Empty, createVesselResponse.Id);
        Assert.AreEqual(5, createVesselResponse.Size);
        Assert.IsFalse(string.IsNullOrWhiteSpace(createVesselResponse.Name));
        Assert.AreEqual(0, createVesselResponse.Damage);
    }

    [Test]
    public async Task it_should_allow_firing_hit()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(httpClient);

        var playerId = newGameResponse.Players.First().Id;

        await createVessel(httpClient, newGameResponse.Id, playerId, new CartesianCoordinates(1, 1), 5);

        var response = await fireAt(httpClient, newGameResponse.Id, playerId, new CartesianCoordinates(1, 2));

        Assert.AreEqual(FireResult.Hit.ToString("G"), response.Result);
    }

    [Test]
    public async Task it_should_allow_firing_miss()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(httpClient);

        var playerId = newGameResponse.Players.First().Id;

        await createVessel(httpClient, newGameResponse.Id, playerId, new CartesianCoordinates(0, 0), 5);

        var response = await fireAt(httpClient, newGameResponse.Id, playerId, new CartesianCoordinates(5, 5));

        Assert.AreEqual(FireResult.Miss.ToString("G"), response.Result);
    }

    // TODO: how do we indicate the player has sunk a ship / lost the game?

    // TODO: integration tests around negative cases (BadRequest responses etc)

    #region Private

    private async Task<CreateGameResponse> createGame(HttpClient httpClient)
    {
        var createResponse = await httpClient.PostAsJsonAsync(API_ROOT, new
            CreateGameRequest
            {
                Players = new[]
                {
                    "James"
                }
            });

        return await createResponse.Content.ReadFromJsonAsync<CreateGameResponse>();
    }

    private async Task<CreateVesselResponse> createVessel(HttpClient httpClient, Guid gameId, Guid playerId,
        CartesianCoordinates origin, int size)
    {
        var createVesselResponse = await httpClient.PostAsJsonAsync(
            $"{API_ROOT}/{gameId}/player/{playerId}/vessel", new
                CreateVesselRequest
                {
                    Orientation = VesselOrientation.Horizontal,
                    Size = size,
                    Column = origin.Column,
                    Row = origin.Row
                });

        return await createVesselResponse.Content.ReadFromJsonAsync<CreateVesselResponse>();
    }

    private async Task<FireAtCoordinatesResponse> fireAt(HttpClient httpClient, Guid gameId, Guid playerId,
        CartesianCoordinates coordinates)
    {
        var fireAtResponse = await httpClient.PostAsJsonAsync(
            $"{API_ROOT}/{gameId}/player/{playerId}/fire", new
                FireAtCoordinatesRequest
                {
                    Coordinates = coordinates
                });

        return await fireAtResponse.Content.ReadFromJsonAsync<FireAtCoordinatesResponse>();
    }

    #endregion
}