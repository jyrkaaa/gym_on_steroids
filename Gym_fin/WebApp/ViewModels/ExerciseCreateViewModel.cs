using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ExerciseCreateViewModel
{
   public Exercise Exercise { get; set; } = default!;
   public SelectList? ExerciseCategories { get; set; } = default!;
   public SelectList? ExerTargets { get; set; } = default!;
   public SelectList? ExerGuides { get; set; } = default!;
   
}