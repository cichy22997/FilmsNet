using FilmsNet.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using FilmsNet.Models;
using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var filmList = await ctx.Films.ToListAsync();

            if (filmList.Count > 0)
                return Json(JsonConvert.SerializeObject(filmList));
            else
                return NotFound();
        }

        [HttpPost("/api/AddFilm/{title}/{year}")]
        public async Task<IActionResult> AddFilm(string title, int year)
        {
            Film film = new Film()
            {
                Id = 0,
                Title = title,
                Year = year
            };

            if (!ctx.Entry<Film>(film).IsKeySet) // false means add entity
            {
                await ctx.Films.AddAsync(film);
                await ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();

        }

        [HttpPatch("/api/PatchFilm/{id}/{title}/{year}")]
        public async Task<IActionResult> UpdateFilm(int id, string title, int year)
        {
            var film = await ctx.Films.FindAsync(id);
            if (film != null)
            {
                film.Title = title;
                film.Year = year;
                ctx.Entry(film).CurrentValues.SetValues(film);
                await ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpDelete("/api/DeleteFilm/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await ctx.Films.FindAsync(id);
            if(film!=null)
            {
                ctx.Films.Remove(film);
                await ctx.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}