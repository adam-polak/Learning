using learningREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace learningREST.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ShirtsController: ControllerBase {

        [HttpGet]
        public string GetShirts() {
            return "Reading all the shirts";
        }

        [HttpGet("{id}/{color}")]
        public string getShirtById(int id, [FromRoute] string color) {
            return $"Reading shirt: {id}, color: {color}";
        }

        [HttpPost]
        public string CreateShirt([FromForm]Shirt s) {
            return $"Creating shirt: {s.ShirtId}";
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id) {
            return $"Deleting shirt: {id}";
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id) {
            return $"Updating shirt: {id}";
        }
        
    }
}