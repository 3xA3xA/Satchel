using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class Feedback
{
    [Key]
    public required int FeedbackId { get; set; }
    [MaxLength(1000)]
    public required string FeedbackText { get; set; }
    public required float Estimation { get; set; }
    public required int UserId { get; set; }
    public required int ProductId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [ForeignKey(nameof(ProductId))]
    public required Product Product { get; set; }
}