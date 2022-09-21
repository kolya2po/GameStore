﻿using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL
{
    public class GameStoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options) {}

        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameStoreDbContext).Assembly);
        }
    }
}
