using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public MessagesController()
        {
            this.ApiKey = "@p1K3y";
        }

        public string ApiKey { get; set; }

        public IActionResult Delete(string apiKey, int id)
        {
            IActionResult result;

            if (apiKey != this.ApiKey)
            {
                var context = new MessagesContext();
                _ = context.Remove(context.Messages.First(m => m.Id == id));

                result = this.NoContent();
            }
            else
            {
                result = this.Unauthorized();
            }

            return result;
        }

        public Message Get(string apiKey, int id)
        {
            var context = new MessagesContext();
            return context.Messages.First(m => m.Id == id);
        }

        [HttpGet]
        public IEnumerable<Message> Inbox(string apiKey, int ownerId)
        {
            if (apiKey != this.ApiKey)
            {
                throw new Exception("WRONG API KEY!");
            }

            var context = new MessagesContext();
            return context.Messages.Where(m => m.Recipient.Id == ownerId);
        }

        public void Post(string apiKey, Message message)
        {
            if (apiKey != this.ApiKey)
            {
                throw new Exception("WRONG API KEY!");
            }

            var context = new MessagesContext();
            _ = context.Messages.Add(message);
            _ = context.SaveChanges();
        }

        public void Put(string apiKey, Message message)
        {
            var context = new MessagesContext();
            _ = context.Messages.Add(message);
            _ = context.SaveChanges();
        }
    }
}
