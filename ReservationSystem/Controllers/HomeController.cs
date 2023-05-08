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

    public IActionResult Privacy()
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
        // TODO create a error handling(logging, flashmessaage) middleware
        // TODO logger

        if (IsDateInPast(model.Date))
            throw new Exception($"Date {model.Date} is in past.");

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

    private static bool IsDateInPast(DateTime dateTime) => dateTime.Date < DateTime.Now;
}