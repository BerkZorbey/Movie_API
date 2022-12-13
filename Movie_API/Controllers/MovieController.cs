using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Movie_API.Models;
using Movie_API.Models.DTOs;
using Movie_API.Models.Value_Object;
using Movie_API.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.Text.Json;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly MovieService _movieService;
       
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(Summary = "Get First 100 Movies")]
        public IActionResult Get([FromQuery] PagingQuery query)
        {
            var movieList = _movieService.GetMovies(query);
            
            return Ok(new {data=movieList,paging=movieList.Result});
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Movie By ID")]
        public IActionResult GetById(string id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Add Movie")]
        public IActionResult AddMovie([FromBody] MovieDetailDTO movie)
        {
            var movie_Id = ObjectId.GenerateNewId().ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _movieService.AddMovie(movie, movie_Id);
            return StatusCode(201);

        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Movie By ID")]
        public IActionResult UpdateMovie([FromBody] MovieDetailDTO updateMovie, string id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            
            movie.Type = updateMovie.Type != default ? updateMovie.Type : movie.Type;
            movie.Title = updateMovie.Title != default ? updateMovie.Title : movie.Title;
            movie.Director = updateMovie.Director != default ? updateMovie.Director : movie.Director;
            movie.Cast = updateMovie.Cast != default ? updateMovie.Cast : movie.Cast;
            movie.Country = updateMovie.Country != default ? updateMovie.Country : movie.Country;
            movie.Date_Added = updateMovie.Date_Added != default ? updateMovie.Date_Added : movie.Date_Added;
            movie.Release_Year = updateMovie.Release_Year != default ? updateMovie.Release_Year : movie.Release_Year;
            movie.Rating = updateMovie.Rating != default ? updateMovie.Rating : movie.Rating;
            movie.Duration = updateMovie.Duration != default ? updateMovie.Duration : movie.Duration;
            movie.Listed_In = updateMovie.Listed_In != default ? updateMovie.Listed_In : movie.Listed_In;
            movie.Description = updateMovie.Description != default ? updateMovie.Description : movie.Description;
            _movieService.UpdateMovie(movie);
            return Ok();
        }
        
        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Update Movie Duration By ID")]
        public IActionResult UpdateMovieDuration([FromBody] MovieDurationDTO movieDuration, string id)
        {
            
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            movie.Duration = movieDuration.Duration != default ? movieDuration.Duration : movie.Duration;
            _movieService.UpdateMovie(movie);
            return Ok();
        }



        
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Movie By ID")]
        public IActionResult DeleteMovie(string id)
        {
            var movie = _movieService.GetMovieById(id);
            if(movie == null)
            {
                return NotFound();
            }
            _movieService.DeleteMovie(id);
            return Ok();
        }
        
    }
}
