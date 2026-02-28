namespace Lead.Domain.Entities;

public class LeadActivity : BaseEntity
{
    public Guid LeadId { get;  set; }
    public string ActivityType { get;  set; } = default!;
    public string? Notes { get;  set; }
    public DateTime ActivityAt { get;  set; }
}
