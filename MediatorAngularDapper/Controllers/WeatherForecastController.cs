using DAL;
using DAL.Repositories;
using MediatorAngularDapper.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MediatorAngularDapper.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository _repo;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IUserRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            try
            {
                return _repo.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            try
            {
                return _repo.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.Create(user);

                    return Ok(user);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.Update(user);

                    return Ok(user);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deletedUser = _repo.Get(id);
                    _repo.Delete(id);

                    return Ok(deletedUser);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }
    }
}
