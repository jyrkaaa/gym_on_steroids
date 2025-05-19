using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class WorkoutEdit
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }

    public string Name { get; set; } = default!;

    public bool Public { get; set; } = false;

}