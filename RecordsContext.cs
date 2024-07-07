using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripleTea.Tables;

namespace TripleTea;

class RecordsContext : DbContext
{
    public virtual DbSet<records> records { get; set; }
    public virtual DbSet<users> users { get; set; }
    public virtual DbSet<computers> computers { get; set; }
    public virtual DbSet<settings> settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(TemporaryAddress());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<computers>()
        .HasMany(e => e.records)
        .WithOne(e => e.computer)
        .HasForeignKey(e => e.computer_id)
        .IsRequired(true);

        modelBuilder.Entity<users>()
        .HasMany(e => e.computers)
        .WithMany(e => e.users);

        modelBuilder.Entity<users>()
        .HasMany(e => e.records)
        .WithOne(e => e.user)
        .HasForeignKey(e => e.user_id);

        modelBuilder.Entity<settings>()
        .HasOne(e => e.user)
        .WithOne(e => e.setting)
        .HasForeignKey<settings>(e => e.user_id)
        .IsRequired(true);

        modelBuilder.Entity<settings>()
        .HasOne(e => e.user)
        .WithOne(e => e.setting)
        .HasForeignKey<settings>(e => e.user_id)
        .IsRequired(true);
    }

    private string TemporaryAddress()
    {
        string path = "C:\\Projects\\TripleTea\\address.txt";
        string text = "";
        try
        {
            text = File.ReadAllText(path);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return text;
    }
}
