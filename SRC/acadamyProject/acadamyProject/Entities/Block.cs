namespace acadamyProject.Entities;

public class Block
{
    public Guid Id { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public string PreviousHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}