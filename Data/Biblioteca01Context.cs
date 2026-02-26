using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Biblioteca0_0.Models;

namespace Biblioteca0_0.Data;

public partial class Biblioteca01Context : DbContext
{
    public Biblioteca01Context()
    {
    }

    public Biblioteca01Context(DbContextOptions<Biblioteca01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Devolucion> Devolucions { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=miesposita22;database=biblioteca01", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.43-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Devolucion>(entity =>
        {
            entity.HasKey(e => e.Iddevo).HasName("PRIMARY");

            entity.ToTable("devolucion");

            entity.HasIndex(e => e.Idprestamo, "idprestamo");

            entity.Property(e => e.Iddevo).HasColumnName("iddevo");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('normal','dañado','regular')")
                .HasColumnName("estado");
            entity.Property(e => e.Fechadevo)
                .HasColumnType("datetime")
                .HasColumnName("fechadevo");
            entity.Property(e => e.Idprestamo).HasColumnName("idprestamo");
            entity.Property(e => e.Monto)
                .HasPrecision(12)
                .HasDefaultValueSql("'0'")
                .HasColumnName("monto");
            entity.Property(e => e.Multa)
                .HasColumnType("enum('si','no')")
                .HasColumnName("multa");

            entity.HasOne(d => d.IdprestamoNavigation).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.Idprestamo)
                .HasConstraintName("devolucion_ibfk_1");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Idlibro).HasName("PRIMARY");

            entity.ToTable("libros");

            entity.HasIndex(e => e.Codlibro, "codlibro").IsUnique();

            entity.Property(e => e.Idlibro).HasColumnName("idlibro");
            entity.Property(e => e.Autor)
                .HasMaxLength(20)
                .HasDefaultValueSql("'anonimo'")
                .HasColumnName("autor");
            entity.Property(e => e.Codlibro)
                .HasMaxLength(50)
                .HasColumnName("codlibro");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .HasColumnName("genero");
            entity.Property(e => e.Publicacion)
                .HasColumnType("datetime")
                .HasColumnName("publicacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Idprestamo).HasName("PRIMARY");

            entity.ToTable("prestamo");

            entity.HasIndex(e => e.Idlibro, "idlibro");

            entity.HasIndex(e => e.Idusuario, "idusuario");

            entity.Property(e => e.Idprestamo).HasColumnName("idprestamo");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('Bueno','Regular','dañado')")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Fechadevo).HasColumnName("fechadevo");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Idlibro)
                .HasConstraintName("prestamo_ibfk_2");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("prestamo_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
            entity.Property(e => e.NumDoc)
                .HasMaxLength(20)
                .HasColumnName("numDoc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDoc)
                .HasColumnType("enum('TI','CC','PPT')")
                .HasColumnName("tipoDoc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
