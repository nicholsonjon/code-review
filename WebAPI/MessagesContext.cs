using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public enum MessageStatus
    {
        Unread,
        Read,
        Archived,
    }

    public class Message
    {
        public int Id { get; set; }

        public Person Recipient { get; set; }

        public Person Sender { get; set; }

        public MessageStatus Status { get; set; }

        public string Text { get; set; }
    }

    public class MessagesContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlite("Data Source=" + Path.Join(Environment.CurrentDirectory, "messages.db"));
        }
    }
}
