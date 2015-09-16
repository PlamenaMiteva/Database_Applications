using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _06.Code_First_Phonebook
{
    public class User
    {
        private ICollection<Channel> channels;
        private ICollection<UserMessage> sentMessages;
        private ICollection<UserMessage> answeredMessages;

        public User()
        {
            this.channels = new HashSet<Channel>();
            this.sentMessages = new HashSet<UserMessage>();
            this.answeredMessages = new HashSet<UserMessage>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        
        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        }

        public virtual ICollection<UserMessage> SentMessages
        {
            get { return this.sentMessages; }
            set { this.sentMessages = value; }
        }
        public virtual ICollection<UserMessage> AnsweredMessages
        {
            get { return this.answeredMessages; }
            set { this.answeredMessages = value; }
        }
    }
}
