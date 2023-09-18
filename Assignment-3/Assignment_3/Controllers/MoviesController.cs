using Assignment_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
            private readonly List<Movie> movies = new List<Movie>
    {
        new Movie { Id = 1, Title = "Movie 1", Genre = "Action" },
        new Movie { Id = 2, Title = "Movie 2", Genre = "Comedy" },
        new Movie { Id =  3, Title ="Movie3",Genre = "Thriller"}
    };

            [HttpGet]
            public ActionResult<IEnumerable<Movie>> Get()
            {
                return Ok(movies);
            }

            [HttpGet("{id}")]
            public ActionResult<Movie> Get(int id)
            {
                var movie = movies.FirstOrDefault(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }

            [HttpPost]
            public IActionResult Post([FromBody] Movie movie)
            {
                if (movie == null)
                {
                    return BadRequest("Invalid data!!!");
                }

                movie.Id = movies.Max(m => m.Id) + 1; 
                movies.Add(movie);

                return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] Movie updatedMovie)
            {
                if (updatedMovie == null || id != updatedMovie.Id)
                {
                    return BadRequest("Invalid data!!!!!!");
                }

                var existingMovie = movies.FirstOrDefault(m => m.Id == id);
                if (existingMovie == null)
                {
                    return NotFound();
                }

                existingMovie.Title = updatedMovie.Title;
                existingMovie.Genre = updatedMovie.Genre;

                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var movie = movies.FirstOrDefault(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }

                movies.Remove(movie);

                return NoContent();
            }
        }
    }

