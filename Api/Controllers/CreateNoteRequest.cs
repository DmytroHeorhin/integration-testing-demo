using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

public class CreateNoteRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Note { get; set; } = string.Empty;
} 