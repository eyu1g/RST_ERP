namespace Lead.Domain.Entities;



public class LeadAssignmentRule : BaseEntity

{

    public string Name { get;set; } = default!;

    public bool IsActive { get;set; }

}

