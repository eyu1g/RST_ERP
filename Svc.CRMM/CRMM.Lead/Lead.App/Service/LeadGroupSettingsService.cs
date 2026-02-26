using Lead.Domain.DTO;

namespace Lead.App.Service;

public sealed class LeadGroupSettingsService
{
    public LeadGroupSettingsDto GetSettings()
    {
        var operators = new List<LeadGroupConditionOperatorDto>
        {
            new("Equals", "Equals"),
            new("NotEquals", "Not Equals"),
            new("Contains", "Contains"),
            new("GreaterThan", "Greater Than"),
            new("LessThan", "Less Than")
        };

        var fields = new List<LeadGroupConditionFieldDto>
        {
            new(
                Key: "Score",
                Label: "Score",
                ValueType: "int",
                AllowedOperators: new[] { "Equals", "GreaterThan", "LessThan" }),
            new(
                Key: "Industry",
                Label: "Industry",
                ValueType: "string",
                AllowedOperators: new[] { "Equals", "NotEquals", "Contains" }),
            new(
                Key: "CompanySize",
                Label: "Company Size",
                ValueType: "string",
                AllowedOperators: new[] { "Equals", "NotEquals", "Contains" }),
            new(
                Key: "Budget",
                Label: "Budget",
                ValueType: "decimal",
                AllowedOperators: new[] { "Equals", "NotEquals", "GreaterThan", "LessThan" }),
            new(
                Key: "StatusId",
                Label: "Status",
                ValueType: "guid",
                AllowedOperators: new[] { "Equals", "NotEquals" }),
            new(
                Key: "StageId",
                Label: "Stage",
                ValueType: "guid",
                AllowedOperators: new[] { "Equals", "NotEquals" }),
            new(
                Key: "SourceId",
                Label: "Source",
                ValueType: "guid",
                AllowedOperators: new[] { "Equals", "NotEquals" })
        };

        return new LeadGroupSettingsDto(fields, operators);
    }
}
