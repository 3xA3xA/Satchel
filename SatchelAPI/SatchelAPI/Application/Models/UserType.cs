using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class UserType
{
    [Key]
    public int UserTypeId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }

    [InverseProperty((nameof(User.UserType)))]
    public required ICollection<User> Users { get; set; }
}