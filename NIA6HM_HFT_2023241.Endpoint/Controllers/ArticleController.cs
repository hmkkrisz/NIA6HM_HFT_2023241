using Microsoft.AspNetCore.Mvc;
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
    public class ArticleController : ControllerBase
    {

        IArticleLogic logic;
        IHubContext<SignalRHub> hub;

        public ArticleController(IArticleLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<Article> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Article Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Article value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("ArticleCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Article value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ArticleUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var articleToDelete = this.logic.Read(id); 
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ArticleDeleted", articleToDelete);
        }
    }
}
