using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Models
{
    public class ChatContext : IdentityDbContext, IDataProtectionKeyContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

        }
    }
}
