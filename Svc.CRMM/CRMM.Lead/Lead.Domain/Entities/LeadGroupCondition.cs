namespace Lead.Domain.Entities;

public sealed class LeadGroupCondition
{
    public Guid Id { get; private set; }

    public Guid LeadGroupId { get; private set; }
    public LeadGroup? LeadGroup { get; private set; }

    public int SortOrder { get; private set; }

    public string Field { get; private set; } = string.Empty;
    public string Operator { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private LeadGroupCondition() { }

    public LeadGroupCondition(
        Guid id,
        Guid leadGroupId,
        int sortOrder,
        string field,
        string @operator,
        string value,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        LeadGroupId = leadGroupId;
        SortOrder = sortOrder;
        Field = field;
        Operator = @operator;
        Value = value;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public void Update(int sortOrder, string field, string @operator, string value, DateTimeOffset updatedAt)
    {
        SortOrder = sortOrder;
        Field = field;
        Operator = @operator;
        Value = value;
        UpdatedAt = updatedAt;
    }
}
