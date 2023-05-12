using ReservationSystem.Data.Entities;

namespace ReservationSystem.Models.Reservation;

public class IndexModel
{
    public IList<ReservationDo> Reservations { get; init; }
}