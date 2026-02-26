namespace Lead.Domain.Entities;

public sealed class Lead
{
    public Guid Id { get; private set; }
    public string LeadNo { get; private set; } = string.Empty;

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? CompanyName { get; private set; }
    public string? CompanySize { get; private set; }

    public string? JobTitle { get; private set; }
    public string? Industry { get; private set; }
    public decimal? Budget { get; private set; }
    public string? Timeline { get; private set; }

    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public Guid SourceId { get; private set; }
    public LeadSource? Source { get; private set; }

    public Guid StatusId { get; private set; }
    public LeadStatus? Status { get; private set; }

    public Guid StageId { get; private set; }
    public LeadStage? Stage { get; private set; }

    public Guid? AssignedToUserId { get; private set; }
    public string? AssignedToName { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private Lead() { }

    public Lead(
        Guid id,
        string leadNo,
        string? firstName,
        string? lastName,
        string? companyName,
        string? companySize,
        string? jobTitle,
        string? industry,
        decimal? budget,
        string? timeline,
        string? email,
        string? phone,
        Guid sourceId,
        Guid statusId,
        Guid stageId,
        Guid? assignedToUserId,
        string? assignedToName,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        LeadNo = leadNo;
        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        CompanySize = companySize;
        JobTitle = jobTitle;
        Industry = industry;
        Budget = budget;
        Timeline = timeline;
        Email = email;
        Phone = phone;
        SourceId = sourceId;
        StatusId = statusId;
        StageId = stageId;
        AssignedToUserId = assignedToUserId;
        AssignedToName = assignedToName;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public void SetAssignment(Guid? assignedToUserId, string? assignedToName, DateTimeOffset updatedAt)
    {
        AssignedToUserId = assignedToUserId;
        AssignedToName = assignedToName;
        UpdatedAt = updatedAt;
    }

    public void UpdateBasics(
        string? firstName,
        string? lastName,
        string? companyName,
        string? companySize,
        string? jobTitle,
        string? industry,
        decimal? budget,
        string? timeline,
        string? email,
        string? phone,
        Guid sourceId,
        Guid statusId,
        Guid stageId,
        DateTimeOffset updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        CompanySize = companySize;
        JobTitle = jobTitle;
        Industry = industry;
        Budget = budget;
        Timeline = timeline;
        Email = email;
        Phone = phone;
        SourceId = sourceId;
        StatusId = statusId;
        StageId = stageId;
        UpdatedAt = updatedAt;
    }
}
