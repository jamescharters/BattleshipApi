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

        var newGameResponse = await createGame(webAppFactory, httpClient);

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

        var newGameResponse = await createGame(webAppFactory, httpClient);

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

        var newGameResponse = await createGame(webAppFactory, httpClient);

        var createVesselResponse = await httpClient.PostAsJsonAsync(
            $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/vessel", new
                AddVesselRequest
                {
                    Orientation = VesselOrientation.Horizontal,
                    Size = 5,
                    Column = 2,
                    Row = 1
                });

        var addVesselResponse = await createVesselResponse.Content.ReadFromJsonAsync<AddVesselResponse>();

        Assert.AreNotEqual(Guid.Empty,addVesselResponse.Id);
        Assert.AreEqual(5,addVesselResponse.Size);
        Assert.IsFalse(string.IsNullOrWhiteSpace(addVesselResponse.Name));
        Assert.AreEqual(0, addVesselResponse.Damage);
    }
    
    [Test]
    public async Task it_should_allow_firing_hit()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        var newGameResponse = await createGame(webAppFactory, httpClient);

        var createVesselResponse = await httpClient.PostAsJsonAsync(
            $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/vessel", new
                AddVesselRequest
                {
                    Orientation = VesselOrientation.Horizontal,
                    Size = 5,
                    Column = 2,
                    Row = 1
                });

        await createVesselResponse.Content.ReadFromJsonAsync<AddVesselResponse>();

        var fireAtResponse = await httpClient.PutAsJsonAsync(
            $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/fire", new
                FireAtRequest
                {
                    Coordinates = new CartesianCoordinates(1, 2)
                });
        
        var response = await fireAtResponse.Content.ReadFromJsonAsync<FireAtResponse>();

        Assert.AreEqual(FireResult.Hit, response.Result);
    }
    
    [Test]
    public async Task it_should_allow_firing_miss()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();
        
        var newGameResponse = await createGame(webAppFactory, httpClient);

        var createVesselResponse = await httpClient.PutAsJsonAsync(
            $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/vessel", new
                AddVesselRequest
                {
                    Orientation = VesselOrientation.Horizontal,
                    Size = 5,
                    Column = 2,
                    Row = 1
                });

        await createVesselResponse.Content.ReadFromJsonAsync<AddVesselResponse>();

        var fireAtResponse = await httpClient.PutAsJsonAsync(
            $"{API_ROOT}/{newGameResponse.Id}/player/{newGameResponse.Players.First().Id}/fire", new
                FireAtRequest
                {
                    Coordinates = new CartesianCoordinates(5, 5)
                });
        
        var response = await fireAtResponse.Content.ReadFromJsonAsync<FireAtResponse>();

        Assert.AreEqual(FireResult.Miss, response.Result);
    }
    
    // TODO: how do we indicate the player has sunk a ship / lost the game?
    
    // TODO: integration tests around negative cases (BadRequest responses etc)

    private async Task<NewGameResponse> createGame(WebApplicationFactory<Program> webAppFactory, HttpClient httpClient)
    {
        var createResponse = await httpClient.PostAsJsonAsync(API_ROOT, new
            NewGameRequest
            {
                Players = new[]
                {
                    "James"
                }
            });

        return await createResponse.Content.ReadFromJsonAsync<NewGameResponse>();
    }
}