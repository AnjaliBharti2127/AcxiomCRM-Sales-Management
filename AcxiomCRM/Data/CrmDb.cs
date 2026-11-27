using AcxiomCRM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AcxiomCRM.Data;
public class CrmDb(DbContextOptions<CrmDb> options):IdentityDbContext<ApplicationUser>(options){
public DbSet<Customer> Customers=>Set<Customer>();
public DbSet<Lead> Leads=>Set<Lead>();
public DbSet<Opportunity> Opportunities=>Set<Opportunity>();
public DbSet<FollowUp> FollowUps=>Set<FollowUp>();
public DbSet<Activity> Activities=>Set<Activity>();
public DbSet<AuditLog> AuditLogs=>Set<AuditLog>();
protected override void OnModelCreating(ModelBuilder b){base.OnModelCreating(b);
b.Entity<Customer>().HasIndex(x=>x.NormalizedEmail).IsUnique();b.Entity<Customer>().HasIndex(x=>x.Phone).IsUnique();b.Entity<Customer>().HasIndex(x=>x.CustomerCode).IsUnique();b.Entity<Lead>().HasIndex(x=>x.LeadCode).IsUnique();
b.Entity<Opportunity>().Property(x=>x.Amount).HasPrecision(18,2);b.Entity<Lead>().Property(x=>x.ExpectedValue).HasPrecision(18,2);
b.Entity<Customer>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.AssignedTo).OnDelete(DeleteBehavior.Restrict);
b.Entity<Lead>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.AssignedTo).OnDelete(DeleteBehavior.Restrict);
b.Entity<Opportunity>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.AssignedTo).OnDelete(DeleteBehavior.Restrict);
b.Entity<FollowUp>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.AssignedTo).OnDelete(DeleteBehavior.Restrict);
b.Entity<Activity>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.AssignedTo).OnDelete(DeleteBehavior.Restrict);
b.Entity<Opportunity>().HasOne<Customer>().WithMany().HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.Restrict);
b.Entity<Opportunity>().HasOne<Lead>().WithMany().HasForeignKey(x=>x.LeadId).OnDelete(DeleteBehavior.Restrict);
b.Entity<FollowUp>().HasOne<Customer>().WithMany().HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.Restrict);
b.Entity<FollowUp>().HasOne<Lead>().WithMany().HasForeignKey(x=>x.LeadId).OnDelete(DeleteBehavior.Restrict);
b.Entity<FollowUp>().HasOne<Opportunity>().WithMany().HasForeignKey(x=>x.OpportunityId).OnDelete(DeleteBehavior.Restrict);
b.Entity<Activity>().HasOne<Customer>().WithMany().HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.Restrict);
b.Entity<Activity>().HasOne<Lead>().WithMany().HasForeignKey(x=>x.LeadId).OnDelete(DeleteBehavior.Restrict);
b.Entity<Lead>().HasOne<Customer>().WithMany().HasForeignKey(x=>x.ConvertedCustomerId).OnDelete(DeleteBehavior.Restrict);
b.Entity<ApplicationUser>().HasOne<ApplicationUser>().WithMany().HasForeignKey(x=>x.ManagerId).OnDelete(DeleteBehavior.Restrict);
}}
