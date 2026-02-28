namespace Lead.Domain.Entities;

public class Lead : BaseEntity
{
    public string? LeadNo { get;set; } = default!;

    public string FirstName { get;set; }=null!;
    public string LastName { get;set; }=null!;
    public string CompanyName { get;set; }=null!;
    public string CompanySize { get;set; }=null!;

    public string JobTitle { get;set; }=null!;
    public string Industry { get;set; }=null!;
    public decimal Budget { get;set; }=null!;
    public string Timeline { get;set; }=null!;

    public string Email { get;set; }=null!;
    public string Phone { get;set; }=null!;

    public Guid? SourceId { get;set; }

    public Guid? StatusId { get;set; }=default!;

    public Guid StageId { get;set; }=null!;

    public Guid? AssignedToUserId { get; set; }=null!;

    public DateTime CreatedAt { get;set; }
    public DateTime UpdatedAt { get;set; }

    public virtual LeadSource Source { get;set; }=null!;
    public virtual LeadStatus Status { get;set; }=null!;
    public virtual LeadStage Stage { get; set; }=null!;
    

}
