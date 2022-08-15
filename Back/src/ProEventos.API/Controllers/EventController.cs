﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly DataContext _context;

        public EventController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<EventTest> Get()
        {
            return _context.Events;
        }

        [HttpGet("{id}")]
        public EventTest GetById(int id)
        {
            return _context.Events.FirstOrDefault(_event => _event.Id == id);
        }

        [HttpPost]
        public string Post()
        {
            return "post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"put id {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"delete id {id}";
        }
    }
}