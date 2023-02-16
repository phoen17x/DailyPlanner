using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Context.Entities.Common;

/// <summary>
/// Base entity class with unique identifier and identity key.
/// </summary>
[Index("Uid", IsUnique = true)]
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the identity key.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    [Required]
    public virtual Guid Uid { get; set; } = Guid.NewGuid();
}