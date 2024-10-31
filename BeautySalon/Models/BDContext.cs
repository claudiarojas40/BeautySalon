using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Models;

namespace BeautySalon.Models;

public partial class BDContext : DbContext
{
    public BDContext()
    {
    }

    public BDContext(DbContextOptions<BDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    public virtual DbSet<Citas> Citas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DetalleV_3214EC07B817B555");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("FK_DetalleVeIdPro_5441852A");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("FK_DetalleVenta_Venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC0772B4B1BC");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC075DC242FE");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC079089B4AD");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario).HasConstraintName("FK__Usuario__IdRol__2E1BDC42");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3214EC07CEA09002");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Venta__IdUsuario__2F10007B");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   

}
