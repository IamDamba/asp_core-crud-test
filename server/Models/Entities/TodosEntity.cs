namespace server.Models.Entities;

public class TodosEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int FkMemberId { get; set; }
}