namespace Lead.Domain.DTO;

public sealed record ConvertLeadRequest(
    bool CreateContact,
    bool CreateAccount,
    bool CreateOpportunity,
    string? Notes,
    string? ConvertedBy);
