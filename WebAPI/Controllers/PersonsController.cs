using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        public async Task<IActionResult> Delete(int id)
        {
            using var context = new MessagesContext();
            var persons = context.Persons;
            var person = await persons.SingleOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);
            IActionResult result;

            if (person is not null)
            {
                _ = persons.Remove(person);
                _ = await context.SaveChangesAsync().ConfigureAwait(false);

                result = this.NoContent();
            }
            else
            {
                result = this.BadRequest($"No Person with the id '${id}' exists");
            }

            return result;
        }

        public Task<Person?> Get(int id)
        {
            using var context = new MessagesContext();
            return context.Persons.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IActionResult> Post(Person person)
        {
            IActionResult result;

            if (person is not null)
            {
                var context = new MessagesContext();
                var persons = context.Persons;
                var entity = await persons.SingleOrDefaultAsync(p => p.Id == person.Id);

                if (entity is null)
                {
                    _ = persons.Add(person);
                    _ = await context.SaveChangesAsync().ConfigureAwait(false);

                    result = this.Created();
                }
                else
                {
                    result = this.BadRequest("A Person with the id '${id}' already exists");
                }
            }
            else
            {
                result = this.BadRequest("No Person object was provided");
            }

            return result;
        }

        public async Task<IActionResult> Put(Person person)
        {
            IActionResult result;

            if (person is not null)
            {
                var context = new MessagesContext();
                var persons = context.Persons;
                var entity = await persons.SingleOrDefaultAsync(p => p.Id == person.Id);

                if (entity is not null)
                {
                    entity.Name = person.Name;

                    _ = await context.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    _ = persons.Add(person);

                    _ = await context.SaveChangesAsync().ConfigureAwait(false);
                }

                result = this.NoContent();
            }
            else
            {
                result = this.BadRequest("No Person object was provided");
            }

            return result;
        }
    }
}
