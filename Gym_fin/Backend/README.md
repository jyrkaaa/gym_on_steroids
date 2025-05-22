ef
~~~sh 
dotnet ef migrations add --project App.DAL --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef migrations   --project App.DAL --startup-project WebApp remove

dotnet ef database   --project App.DAL --startup-project WebApp update
dotnet ef database   --project App.DAL --startup-project WebApp drop

~~~
MVC Controllers
~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name ExerciseController        -actions -m  App.Domain.EF.Exercise        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserWeightController        -actions -m  App.Domain.EF.UserWeight        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkoutController        -actions -m  App.Domain.EF.Workout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerInWorkoutController        -actions -m  App.Domain.EF.ExerInWorkout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerciseCategoryController        -actions -m  App.Domain.EF.ExerciseCategory        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerTargerController        -actions -m  App.Domain.EF.ExerTarget        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SetInExercController        -actions -m  App.Domain.EF.SetInExerc        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersInWorkoutController        -actions -m  App.Domain.EF.UsersInWorkout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerGuideController        -actions -m  App.Domain.EF.ExerGuide        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator identity -dc App.DAL.AppDbContext -f
~~~
API Controllers
~~~sh
dotnet aspnet-codegenerator controller -name ExerciseController  -m  App.Domain.EF.Exercise        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UserWeightController  -m  App.Domain.EF.UserWeight        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name WorkoutController  -m  App.Domain.EF.Workout        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ExerInWorkoutController  -m  App.Domain.EF.ExerInWorkout        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ExerciseCategoryController  -m  App.Domain.EF.ExerciseCategory        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ExerTargerController  -m  App.Domain.EF.ExerTarget        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SetInExercController  -m  App.Domain.EF.SetInExerc        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UsersInWorkoutController  -m  App.Domain.EF.UsersInWorkout        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ExerGuideController  -m  App.Domain.EF.ExerGuide        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f


For ADMin: mvc

cd WebApp

dotnet aspnet-codegenerator controller -name ExerciseController        -actions -m  App.Domain.EF.Exercise        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserWeightController        -actions -m  App.Domain.EF.UserWeight        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkoutController        -actions -m  App.Domain.EF.Workout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerInWorkoutController        -actions -m  App.Domain.EF.ExerInWorkout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerciseCategoryController        -actions -m  App.Domain.EF.ExerciseCategory        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerTargerController        -actions -m  App.Domain.EF.ExerTarget        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SetInExercController        -actions -m  App.Domain.EF.SetInExerc        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersInWorkoutController        -actions -m  App.Domain.EF.UsersInWorkout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ExerGuideController        -actions -m  App.Domain.EF.ExerGuide        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

~~~
~~~sh
In Backend catalog
docker run --name gym_docker_be --rm -it -p 8888:8080 webapp
~~~