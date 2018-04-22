﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository,
            ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = (_repository.GetTripsByUsername(this.User.Identity.Name));

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));

            }
            catch (Exception ex)
            {
                // TODO Logging - log error
                _logger.LogError($"Failed to get  All Trips: {ex}");

                return BadRequest("Error occurred");
            }
            
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if(ModelState.IsValid)
            {
                // save to model
                var newTrip = Mapper.Map<Trip>(theTrip);
                
                newTrip.UserName = User.Identity.Name;
                
                _repository.AddTrip(newTrip);
                if(await _repository.SaveChangesAsync())
                {
                    // return what was sent - convert newTrip obj back to cx facing JSON obj
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));

                }
                else
                {
                    return BadRequest("Failed to save changes to the database");
                }
            }

            return BadRequest("Failed to save the trip"); 
        }
    }
}
