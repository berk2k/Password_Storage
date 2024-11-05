﻿using Microsoft.EntityFrameworkCore;
using Password_Storage.Api.Models;

namespace Password_Storage.Api.Context
{
    public class PasswordDbContext : DbContext
    {
        public PasswordDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<PasswordModel> passwordstorage { get; set; }
    }
}
