using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDemo.API.Models;
using MovieDemo.Entity.BusinessEntities;
using MovieDemo.LIB.Exceptions;
using MovieDemo.LIB.Models;
using MovieDemo.LIB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class MoviesController : ControllerBase
    {
        private IMovieRepository _movieRepository;
        private IMapper _mpper;
        private readonly ILogger<MoviesController> _logger;
        public MoviesController(IMovieRepository movieRepository, IMapper mapper, ILogger<MoviesController> logger)
        {
            _movieRepository = movieRepository;
            _mpper = mapper;
            _logger = logger;
        }        

        [HttpGet()]
        [ActionName("filter")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMoviesByFilterCriteriaAsync([FromQuery] MovieFilterCriteria movieFilterCritera)
        {
            if (movieFilterCritera.IsValid())
            {
                IEnumerable<MovieEntity> movies = await _movieRepository.GetMoviesByFilterCriteriaAsync(movieFilterCritera);
                if (movies != null && !movies.Any())
                {
                    _logger.LogInformation("Movie Filter - No Movie(s) found!");
                    return NotFound();
                }

                return Ok(_mpper.Map<IEnumerable<MovieModel>>(movies));
            }
            else
            {
                _logger.LogInformation("Movie Filter - Opps Bad request!");
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("byRating")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetTopFiveMoviesByRatingAsync([FromQuery] int? top = null)
        {
            if (top.GetValueOrDefault(5) > 0)
            {
                IEnumerable<MovieEntity> movies = await _movieRepository.GetTopMoviesByRatingAsync(top.GetValueOrDefault(5));
                if (movies != null && !movies.Any())
                {
                    _logger.LogInformation("Movies by Rating - No Movie(s) found!");
                    return NotFound();
                }

                return Ok(_mpper.Map<IEnumerable<MovieModel>>(movies));
            }
            else
            {
                _logger.LogInformation("Movie by Rating - Opps Bad request!");
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("byUser")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetTopFiveMoviesByUserAsync([FromQuery] long userId, [FromQuery] int? top = null)
        {
            if (userId > 0 && top.GetValueOrDefault(5) > 0)
            {
                IEnumerable<MovieEntity> movies = await _movieRepository.GetTopMoviesByUserAsync(userId, top.GetValueOrDefault(5));
                if (movies != null && !movies.Any())
                {
                    _logger.LogInformation("Movies by User - No Movie(s) found!");
                    return NotFound();
                }

                return Ok(_mpper.Map<IEnumerable<MovieModel>>(movies));
            }
            else
            {
                _logger.LogInformation("Movie by User - Opps Bad request!");
                return BadRequest();
            }
        }

        [HttpPost]
        [ActionName("UpdateUserRating")]
        public async Task<ActionResult> UpdateUserRatingAsync([FromQuery] UpdateMovieUserRating updateMovieUserRating)
        {
            int noOfRecUpdated = 0;
            try
            {
                if (updateMovieUserRating.IsValid())
                {
                    noOfRecUpdated = await _movieRepository.UpdateUserRatingAsync(updateMovieUserRating);
                }
            }
            catch (MovieNotFoundExceptions movieNotFoundException)
            {
                _logger.LogInformation(movieNotFoundException,"Update User Ratings - Movie not found!");
                return NotFound("Movie not found!");
            }
            catch (UserNotFoundExceptions userNotFoundException)
            {
                _logger.LogInformation(userNotFoundException,"User not found!");
                return NotFound("User not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Opps Unexpected error !");
                return StatusCode(500, ex);
            }

            if (noOfRecUpdated > 0)
                return Ok();
            else
            {
                _logger.LogInformation("No records Updated!");
                return StatusCode(500, "No records Updated!");
            }
        }

        //[HttpGet()]
        //[ActionName("TestEF")]
        //public async Task<ActionResult<string>> TestEFAsync()
        //{
        //    return Ok(await _movieRepository.TestEFAsync());
        //}
    }
}
