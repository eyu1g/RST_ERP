namespace Lead.Domain.Entities;

public class LeadConversionLog : BaseEntity
{
    public Guid LeadId { get;set; }
    public string ConvertedTo { get;set; } = default!;
    public Guid? TargetId { get;set; }
    public string? ConvertedBy { get;set; }
    public DateTime ConvertedAt { get;set; }
}
