﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using NIA6HM_HFT_2023241.Endpoint.Services;
using NIA6HM_HFT_2023241.Logic;
using NIA6HM_HFT_2023241.Models;
using System.Collections.Generic;

namespace NIA6HM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        IAuthorLogic logic;
        IHubContext<SignalRHub> hub;

        public AuthorController(IAuthorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<Author> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Author Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Author value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("AuthorCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Author value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("AuthorUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var authorToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("AuthorDeleted", authorToDelete);
        }
    }
}
