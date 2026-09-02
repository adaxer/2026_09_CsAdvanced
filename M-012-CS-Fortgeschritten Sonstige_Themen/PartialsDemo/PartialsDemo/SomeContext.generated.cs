using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartialsDemo;

/* Generated code - do not modify 
 * Last generated: 2024-06-19 12:00:00
 */

partial class SomeContext : DbContext
{
    partial void OnConstructor();
    partial void OnAfterModelCreating();
    partial void OnBeforeModelCreating();

    public SomeContext(DbContextOptions<SomeContext> options)
        : base(options)
    {
        OnConstructor();
    }

    partial string SomeProperty { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnBeforeModelCreating();
        base.OnModelCreating(modelBuilder);
        OnAfterModelCreating();
    }

}
