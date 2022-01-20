using Microsoft.EntityFrameworkCore;
using System;

using Database.Models;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        DbSet<CredentialModel> Credentials { get; set; }

        DbSet<UserModel> Users { get; set; }

        DbSet<MessageModel> Messages { get; set; }

        DbSet<FriendshipModel> Friendships { get; set; }

        DbSet<DialogModel> Dialogs { get; set; }

        DbSet<PostModel> Posts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CredentialModel>()
                .HasOne(u => u.User)
                .WithOne(c => c.Credential);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.UserDialogs)
                .WithOne(d => d.Initiator)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.DialogsToUser)
                .WithOne(d => d.Addressee)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Author);

            modelBuilder.Entity<MessageModel>()
                .HasOne(m => m.Dialog)
                .WithMany(d => d.Messages);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author);

            modelBuilder.Entity<FriendshipModel>()
                .HasOne(f => f.Me)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.MeID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FriendshipModel>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FriendOf)
                .HasForeignKey(f => f.FriendID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
