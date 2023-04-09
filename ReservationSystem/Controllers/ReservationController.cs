using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Data.Entities;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Controllers;

public class ReservationController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] CreateReservationModel model)
    {
        var reservationDo = new ReservationDo
        {
            Email = model.Email,
            Date = model.Date,
            Fee = model.Fee,
            Location = model.Location
        };
        _dbContext.Reservations.Add(reservationDo);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}