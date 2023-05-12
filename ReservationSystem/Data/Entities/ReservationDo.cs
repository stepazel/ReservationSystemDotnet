namespace ReservationSystem.Data.Entities;

public class ReservationDo
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public decimal Fee { get; set; }
    public bool? Approved { get; set; }
}