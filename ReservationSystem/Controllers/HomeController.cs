using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Data.Entities;
using ReservationSystem.Models;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly ApplicationDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] CreateReservationModel model)
    {
        if (IsDateInPast(model.DateTime))
        {
            TempData["ErrorMessage"] = $"Date {model.DateTime.Date} is in past.";
            return RedirectToAction("Index");
        }

        if (_dbContext.Reservations.Any(reservationDo =>
                reservationDo.Approved == true && reservationDo.Date == model.DateTime.Date))
        {
            TempData["ErrorMessage"] = "There is already an approved reservation on this date.";
            return RedirectToAction("Index");
        }

        var reservationDo = new ReservationDo
        {
            Email = model.Email,
            Date = model.DateTime,
            Fee = model.Fee,
            Location = model.Location
        };
        _dbContext.Reservations.Add(reservationDo);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    private static bool IsDateInPast(DateTime dateTime) => dateTime.Date < DateTime.Now;
}