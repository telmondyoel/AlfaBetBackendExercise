using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlfaBetBackendExercise.Database.Entities;

public class Event
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(500)]
    public string Summary { get; set; }

    [MaxLength(150)]
    public string Location { get; set; }

    public DateTime Date { get; set; }
    public int? ParticipantsAmount { get; set; }
    public DateTime CreationDate { get; set; }
}