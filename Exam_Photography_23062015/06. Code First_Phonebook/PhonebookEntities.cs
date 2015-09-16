namespace _06.Code_First_Phonebook
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookEntities : DbContext
    {
        public PhonebookEntities()
            : base("name=PhonebookEntities")
        {
           
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<UserMessage> UserMesseges { get; set; }
        public virtual DbSet<ChannelMessage> ChannelMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasRequired(x => x.Sender)
                .WithMany(x => x.SentMessages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                .HasRequired(x => x.Recipient)
                .WithMany(x => x.AnsweredMessages)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

    
}