using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Data.Entities;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var reservations = _dbContext.Reservations.ToList();
        var model = new IndexModel {Reservations = reservations};
        return View(model);
    }

    [HttpPost]
    public IActionResult Test()
    {
        var reservations = new List<ReservationDo>();
        for (var i = 0; i < 20; i++)
        {
            var reservation = new ReservationDo
            {
                Date = DateTime.Today.AddDays(i),
                Email = $"test{i}@email.com",
                Fee = 500 * (i + 1),
                Location = $"Test{i}"
            };
            reservations.Add(reservation);
        }

        _dbContext.Reservations.AddRange(reservations);
        _dbContext.SaveChanges();
        return Ok("Ahoj");
    }

    [HttpPost]
    public IActionResult Edit(int id)
    {
        var reservation = _dbContext.Reservations.FirstOrDefault(reservationDo => reservationDo.Id == id);
        if (reservation is null)
            return NotFound($"A reservation with id {id} was not found");

        reservation.Approved = reservation.Approved is not true;
        _dbContext.SaveChanges();
        return PartialView("ReservationRow", reservation);
    }
}