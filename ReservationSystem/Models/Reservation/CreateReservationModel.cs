using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation;

public class CreateReservationModel
{
    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter a date.")]
    [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Please enter a value for location.")]
    public string Location { get; set; }

    [Required(ErrorMessage = "Please enter some fee.")]
    public decimal Fee { get; set; }
}
