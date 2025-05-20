using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.ViewModels;

public class ExerciseCreateViewModel
{
    public App.Domain.EF.Exercise Exercise { get; set; } = default!;
    public SelectList? ExerciseCategories { get; set; } = default!;
    public SelectList? ExerTargets { get; set; } = default!;
    public SelectList? ExerGuides { get; set; } = default!;
   
}