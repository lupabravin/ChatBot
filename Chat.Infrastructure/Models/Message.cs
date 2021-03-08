using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Models
{
    public class Message
    {
        public Message()
        {

        }
        public Message(AppUser User, string text)
        {
            this.Sender = User;
            this.Text = text;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MessageId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string UserId { get; set; }

        public virtual AppUser Sender { get; set; }

    }
}
