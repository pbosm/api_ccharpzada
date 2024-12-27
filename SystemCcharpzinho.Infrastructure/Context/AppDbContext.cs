﻿using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Core.Models;

namespace SystemCcharpzinho.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}