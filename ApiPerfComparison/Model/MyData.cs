using System.ComponentModel.DataAnnotations;

public record MyData(string Name, int Age);

public class UserDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(18, 99)]
    public int Age { get; set; }
}