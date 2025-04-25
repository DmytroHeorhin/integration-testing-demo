using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

public class CreateDemoRecordRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Message { get; set; } = string.Empty;
}