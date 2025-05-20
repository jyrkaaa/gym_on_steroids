
using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Extensions.Ordering;

namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class ExerciseCategoryTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;


    public ExerciseCategoryTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact]
    public async Task See_Preset_Categories()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);
        
        var getResponse = await _client.GetAsync("/api/v1/ExerciseCategory/all");
        var content = await getResponse.Content.ReadFromJsonAsync<List<App.DTO.v1.ExerciseCategory>>();
        getResponse.EnsureSuccessStatusCode();
        Assert.True(content?.Count == 2);
    }


    [Fact]
    public async Task Login_Existing_User()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }
    
}