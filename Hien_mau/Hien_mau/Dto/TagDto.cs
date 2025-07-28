using System.ComponentModel.DataAnnotations;

public class TagDto
{
    [Required]
    public string TagName { get; set; } = null!;
}
