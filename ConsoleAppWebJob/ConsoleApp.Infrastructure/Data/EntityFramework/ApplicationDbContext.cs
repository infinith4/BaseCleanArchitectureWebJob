﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.Infrastructure.Data.Entities;
using ConsoleApp.Infrastructure.Data.EntityFramework.Entities;


namespace ConsoleApp.Infrastructure.Data.EntityFramework
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //NOTE: [Key]が複数ある場合は順序付けが必要
        //    modelBuilder.Entity<MST_AppSetting>()
        //        .HasKey(c => new { c.ApiKey });
        //}

        /// <summary>
        /// MST_AppSetting
        /// </summary>
        public virtual DbSet<MstAppSetting> MST_AppSetting { get; set; }
    }
}
