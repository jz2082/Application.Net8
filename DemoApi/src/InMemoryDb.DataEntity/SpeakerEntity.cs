using Application.Framework;

namespace InMemoryDb.DataEntity;

public record SpeakerEntity : BaseEntity<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool Sat { get; set; }
    public bool Sun { get; set; }
    public bool Favorite { get; set; }
    public string? Bio { get; set; }
    public string? Company { get; set; }
    public string? TwitterHandle { get; set; }
    public string? UserBioShort { get; set; }
    public string? ImageUrl { get; set; }
    public string? Email { get; set; }
    public string? Photo { get; set; }
}