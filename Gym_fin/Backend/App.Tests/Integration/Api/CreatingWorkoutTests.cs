
using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Extensions.Ordering;

namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class CreatingWorkoutTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;


    public CreatingWorkoutTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact]
    public async Task Access_Workouts()
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
        
        var getResponse = await _client.GetAsync("/api/v1/Workout/all");
        getResponse.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task Create_Simple_Workout()
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

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var DatetoCheck = DateTime.UtcNow;
        var sentEntity = new DTO.v1.WorkoutCreate()
        {
            Date = DatetoCheck,
            Name = "Test1",
            Public = true
        };

        var response2 = await _client.PostAsJsonAsync("/api/v1/Workout", sentEntity);
        Assert.True(response2.StatusCode == HttpStatusCode.Created);

        var getResponse2 = await _client.GetAsync("/api/v1/Workout/all");
        getResponse2.EnsureSuccessStatusCode();

        var content = await getResponse2.Content.ReadFromJsonAsync<List<DTO.v1.Workout>>();
        var enteredWorkout = content?.FirstOrDefault();
        Assert.NotNull(enteredWorkout);
        Assert.True(enteredWorkout!.Name == "Test1");
        Assert.True(enteredWorkout!.Public);
        Assert.True(enteredWorkout!.Date == DatetoCheck);
    }
    [Fact]
    public async Task Create_With_Exercise_Workout()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=120", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var DatetoCheck = DateTime.UtcNow;
        var sentEntity = new DTO.v1.WorkoutCreate()
        {
            Date = DatetoCheck,
            Name = "Test1",
            Public = true
        };

        var response2 = await _client.PostAsJsonAsync("/api/v1/Workout", sentEntity);
        Assert.True(response2.StatusCode == HttpStatusCode.Created);
        // Get Created workout Id from post requests response
        var workoutId = response2.Content.ReadFromJsonAsync<DTO.v1.Workout>().Result!.Id;
        
        //Create Exercise with preset category and create workout from it
        
        //Get ExerciseCategorys (2 are seeded, get random first)
        
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
            Date = DatetoCheck,
            ExerciseCategoryId = firstCategory.Id

        };
        
        var postExerciseResponse = await _client.PostAsJsonAsync("/api/v1/Exercise", createExerciseEntity);
        Assert.True(postExerciseResponse.StatusCode == HttpStatusCode.Created);

        var exerciseContent =
             postExerciseResponse.Content.ReadFromJsonAsync<DTO.v1.Exercise>().Result!;
        Assert.NotNull(exerciseContent);
        
        
        var exerciseInWorkoutEntity = new DTO.v1.ExerInWorkoutCreate()
        {
            Desc = null,
            WorkoutId = workoutId,
            ExerciseId = exerciseContent.Id
        };
        
        var postExerInWorkout = await _client.PostAsJsonAsync("/api/v1/ExerInWorkout", exerciseInWorkoutEntity);
        Assert.True(postExerInWorkout.StatusCode == HttpStatusCode.Created);
        
        //Make get request to see if there is one exerInWorkout entity for the logged in user
        var finResponse = await _client.GetAsync("/api/v1/ExerInWorkout/all");
        finResponse.EnsureSuccessStatusCode();
        var finContent = await finResponse.Content.ReadFromJsonAsync<List<DTO.v1.ExerInWorkout>>();
        Assert.NotNull(finContent);
        Assert.True(finContent.Count == 1);
    }
    
    [Fact]
    public async Task Test_User_Workout()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=120", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var DatetoCheck = DateTime.UtcNow;
        var sentEntity = new DTO.v1.WorkoutCreate()
        {
            Date = DatetoCheck,
            Name = "Test1",
            Public = true
        };

        var response2 = await _client.PostAsJsonAsync("/api/v1/Workout", sentEntity);
        Assert.True(response2.StatusCode == HttpStatusCode.Created);
        // Get Created workout Id from post requests response
        var workoutId = response2.Content.ReadFromJsonAsync<DTO.v1.Workout>().Result!.Id;
        
        //Create Exercise with preset category and create workout from it
        
        //Get ExerciseCategorys (2 are seeded, get random first)
        
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
            Date = DatetoCheck,
            ExerciseCategoryId = firstCategory.Id

        };
        
        var postExerciseResponse = await _client.PostAsJsonAsync("/api/v1/Exercise", createExerciseEntity);
        Assert.True(postExerciseResponse.StatusCode == HttpStatusCode.Created);

        var exerciseContent =
             postExerciseResponse.Content.ReadFromJsonAsync<DTO.v1.Exercise>().Result!;
        Assert.NotNull(exerciseContent);
        
        
        var exerciseInWorkoutEntity = new DTO.v1.ExerInWorkoutCreate()
        {
            Desc = null,
            WorkoutId = workoutId,
            ExerciseId = exerciseContent.Id
        };
        
        var postExerInWorkout = await _client.PostAsJsonAsync("/api/v1/ExerInWorkout", exerciseInWorkoutEntity);
        Assert.True(postExerInWorkout.StatusCode == HttpStatusCode.Created);
        
        //Make get request to see if there is one exerInWorkout entity for the logged in user
        var finResponse = await _client.GetAsync("/api/v1/ExerInWorkout/all");
        finResponse.EnsureSuccessStatusCode();
        var finContent =  finResponse.Content.ReadFromJsonAsync<List<DTO.v1.ExerInWorkout>>().Result!;
        Assert.NotNull(finContent);
        Assert.True(finContent.Count == 1);
        
        // LogIn with other user
        var loginData2 = new LoginInfo()
        {
            Email = "admin@taltech.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response3 = await _client.PostAsJsonAsync("/api/v1/account/login", loginData2);

        // Assert
        response3.EnsureSuccessStatusCode();
        var responseData2 = await response3.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData2);
        Assert.True(responseData2.JWT.Length > 128);
        Assert.True(responseData2.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData2.JWT);
        
        var otherUserResponse = await _client.GetAsync("/api/v1/ExerInWorkout/all");
        otherUserResponse.EnsureSuccessStatusCode();
        
        var finContent2 = await otherUserResponse.Content.ReadFromJsonAsync<List<DTO.v1.ExerInWorkout>>();
        Assert.NotNull(finContent2);
        Assert.True(finContent2.Count == 0);
        
    }
    [Fact]
    public async Task Create_With_Exercise_and_Sets_Workout()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=120", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var DatetoCheck = DateTime.UtcNow;
        var sentEntity = new DTO.v1.WorkoutCreate()
        {
            Date = DatetoCheck,
            Name = "Test1",
            Public = true
        };

        var response2 = await _client.PostAsJsonAsync("/api/v1/Workout", sentEntity);
        Assert.True(response2.StatusCode == HttpStatusCode.Created);
        // Get Created workout Id from post requests response
        var workoutId = response2.Content.ReadFromJsonAsync<DTO.v1.Workout>().Result!.Id;
        
        //Create Exercise with preset category and create workout from it
        
        //Get ExerciseCategorys (2 are seeded, get random first)
        
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
            Date = DatetoCheck,
            ExerciseCategoryId = firstCategory.Id

        };
        
        var postExerciseResponse = await _client.PostAsJsonAsync("/api/v1/Exercise", createExerciseEntity);

        var exerciseContent =
             postExerciseResponse.Content.ReadFromJsonAsync<DTO.v1.Exercise>().Result!;
        
        
        var exerciseInWorkoutEntity = new DTO.v1.ExerInWorkoutCreate()
        {
            Desc = null,
            WorkoutId = workoutId,
            ExerciseId = exerciseContent.Id
        };
        
        await _client.PostAsJsonAsync("/api/v1/ExerInWorkout", exerciseInWorkoutEntity);
        
        //Make get request to see if there is one exerInWorkout entity for the logged in user
        var eiwResponse = await _client.GetAsync("/api/v1/ExerInWorkout/all");
        eiwResponse.EnsureSuccessStatusCode();
        var eiwContent = eiwResponse.Content.ReadFromJsonAsync<List<DTO.v1.ExerInWorkout>>().Result!;
        Assert.NotNull(eiwContent);
        var setInputEntity = new DTO.v1.SetInExercCreate()
        {
            Weight = 10,
            Reps = 10,
            ExerInWorkoutId = eiwContent.FirstOrDefault()!.Id
        };
        var postSetInWorkout = await _client.PostAsJsonAsync("/api/v1/SetInExerc", setInputEntity);
        Assert.True(postSetInWorkout.StatusCode == HttpStatusCode.Created);
        
        //Test if searching workout by id gives all info
        var fullResponse = await _client.GetAsync("/api/v1/Workout/" + workoutId);
        fullResponse.EnsureSuccessStatusCode();
        var workout =fullResponse.Content.ReadFromJsonAsync<DTO.v1.Workout>().Result!;
        Assert.NotNull(workout);

        //Checks that workout has one exerInWorkout
        var exersInWorkoutFromWorkout = workout.Exercises;
        Assert.NotNull(exersInWorkoutFromWorkout);
        Assert.True(exersInWorkoutFromWorkout.Count == 1);
        
        //Check if workout has one set in that exercise
        var setsInWorkout = exersInWorkoutFromWorkout.FirstOrDefault()!.Sets;
        Assert.NotNull(setsInWorkout);
        Assert.True(setsInWorkout.Count == 1);
        
        //Checks set info
        Assert.True(setsInWorkout.FirstOrDefault()!.Weight == 10);
        Assert.True(setsInWorkout.FirstOrDefault()!.Reps == 10);

    }

    [Fact]
    public async Task Test_Patch_and_search_Workout()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "jurgen@gmail.com",
            Password = "StrongPa1!ssword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=120", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);

        var DatetoCheck = DateTime.UtcNow;
        var sentEntity = new DTO.v1.WorkoutCreate()
        {
            Date = DatetoCheck,
            Name = "Test1",
            Public = true
        };

        var response2 = await _client.PostAsJsonAsync("/api/v1/Workout", sentEntity);
        Assert.True(response2.StatusCode == HttpStatusCode.Created);
        // Get Created workout Id from post requests response
        var workoutId = response2.Content.ReadFromJsonAsync<DTO.v1.Workout>().Result!.Id;
        
        
        //Assert workout exists
        var getWorkoutResponse = await _client.GetAsync("/api/v1/Workout/all?name=Test1");
        getWorkoutResponse.EnsureSuccessStatusCode();
        var workoutEntity1 = getWorkoutResponse.Content.ReadFromJsonAsync<List<DTO.v1.Workout>>().Result!;
        Assert.NotNull(workoutEntity1);
        Assert.True(workoutEntity1.First().Id == workoutId);
        
        //Login with other account
        // LogIn with other user
        var loginData2 = new LoginInfo()
        {
            Email = "admin@taltech.ee",
            Password = "Foo.Bar.1"
        };

        // Act
        var response3 = await _client.PostAsJsonAsync("/api/v1/account/login", loginData2);

        // Assert
        response3.EnsureSuccessStatusCode();
        var responseData2 = await response3.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData2);
        Assert.True(responseData2.JWT.Length > 128);
        Assert.True(responseData2.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData2.JWT);
        
        //Search by name
        var getPublicWorkoutResponse = await _client.GetAsync("/api/v1/Workout/all?name=Test1");
        getPublicWorkoutResponse.EnsureSuccessStatusCode();
        var workoutEntity2 = getPublicWorkoutResponse.Content.ReadFromJsonAsync<List<DTO.v1.Workout>>().Result!;
        Assert.NotNull(workoutEntity2);
        Assert.True(workoutEntity2.First().Id == workoutId);
        
        // Assert not owners cant change workout 
        var patchEntity = new DTO.v1.WorkoutEdit()
        {
            Id = workoutId,
            Name = "Test2",
            Date = DateTime.UtcNow,
            Public = false
        };
        var patchFailResponse = await _client.PatchAsJsonAsync("api/v1/Workout/" + workoutId, patchEntity);
        Assert.True(!patchFailResponse.IsSuccessStatusCode);
        
        
        var originalUserResponse = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=120", loginData);

        // Assert
        originalUserResponse.EnsureSuccessStatusCode();
        var originalsAuth = await originalUserResponse.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(originalsAuth);
        Assert.True(originalsAuth.JWT.Length > 128);
        Assert.True(originalsAuth.RefreshToken.Length == Guid.NewGuid().ToString().Length);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", originalsAuth.JWT);

        var patchSuccessResponse = await _client.PatchAsJsonAsync("api/v1/Workout/" + workoutId, patchEntity);
        Assert.True(patchSuccessResponse.IsSuccessStatusCode);
        
        //Log back to old user and search for private workout by name
        
        var guestUser = await _client.PostAsJsonAsync("/api/v1/account/login", loginData2);
        var guestAuth = await guestUser.Content.ReadFromJsonAsync<JWTResponse>();
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", guestAuth!.JWT);
        
        var emptySearchResponse = await _client.GetAsync("/api/v1/Workout/all?name=Test1");
        emptySearchResponse.EnsureSuccessStatusCode();
        var emptyList = emptySearchResponse.Content.ReadFromJsonAsync<List<DTO.v1.Workout>>().Result!;
        Assert.NotNull(emptyList);
        Assert.True(emptyList.Count == 0);
    }
}