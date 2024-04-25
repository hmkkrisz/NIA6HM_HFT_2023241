﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NIA6HM_HFT_2023241.Endpoint.Services;
using NIA6HM_HFT_2023241.Logic;
using NIA6HM_HFT_2023241.Models;
using System.Collections.Generic;

namespace NIA6HM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        ICommentLogic logic;
        IHubContext<SignalRHub> hub;

        public CommentController(ICommentLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<Comment> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Comment Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Comment value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CommentCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Comment value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CommentUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var commentToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CommentDeleted", commentToDelete);
        }
    }
}
