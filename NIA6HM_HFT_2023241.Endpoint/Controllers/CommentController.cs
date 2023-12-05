using Microsoft.AspNetCore.Mvc;
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

        public CommentController(ICommentLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut("{id}")]
        public void Update([FromBody] Comment value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
