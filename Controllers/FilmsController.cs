using FilmsNet.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using FilmsNet.Models;

namespace FilmsNet.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext ctx;
        public FilmsController(ApplicationDbContext context)
        {
            ctx = context;
        }
        [HttpGet]
        [Route("/api/GetFilms")]
        public IActionResult Index()
        {
            var filmList = ctx.Films.ToList();
            var json = JsonSerializer.Serialize(filmList);
            return Ok(json);
        }

        [HttpPost("/api/AddFilm/{title}/{year}")]
        public async Task<IActionResult> AddFilm(string title, int year)
        {
            Film film = new Film()
            {
                Id = 2,
                Title = title,
                Year = year
            };
            var res = ctx.Films.Add(film);

            ctx.SaveChanges();
            return Ok();
        }

        [HttpPatch("/api/PatchFilm/{id}/{title}/{year}")]
        public async Task<IActionResult> UpdateFilm(int id, string title, int year)
        {
            return Ok();
        }

        [HttpDelete("/api/DeleteFilm/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            return Ok();
        }

    }
}