namespace Lead.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime DateAdd { get; set; }

    public DateTime? DateMod { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] RowVersion { get; set; } = default!;
}
