
using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Extensions.Ordering;

namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class ExerciseTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;


    public ExerciseTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact]
    public async Task Can_Create_Exercise()
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
        
        //Fetch Category Ids
        var exerCategoryResponse = await _client.GetAsync("/api/v1/ExerciseCategory/all");
        var categorysContent =
            await exerCategoryResponse.Content.ReadFromJsonAsync<List<DTO.v1.ExerciseCategory>>();
        var firstCategory = categorysContent?.FirstOrDefault();
        Assert.NotNull(firstCategory);
        
        //Create Exercise 
        var createExerciseEntity = new DTO.v1.ExerciseCreate()
        {
            Name = "TestExercise",
            Desc = null,
            Date = DateTime.UtcNow,
            ExerciseCategoryId = firstCategory.Id

        };
        
        var postExerciseResponse = await _client.PostAsJsonAsync("/api/v1/Exercise", createExerciseEntity);
        Assert.True(postExerciseResponse.StatusCode == HttpStatusCode.Created);

        var getResponse = await _client.GetAsync("/api/v1/Exercise/all");
        getResponse.EnsureSuccessStatusCode();
        var responseData2 = await getResponse.Content.ReadFromJsonAsync<List<DTO.v1.Exercise>>();
        Assert.True(responseData2?.Count == 1);
        var insertedExercise = responseData2.FirstOrDefault();
        Assert.NotNull(insertedExercise);
        Assert.Equal(createExerciseEntity.Name, insertedExercise.Name);
        Assert.Equal(createExerciseEntity.Desc, insertedExercise.Desc);
    }


    
    
}