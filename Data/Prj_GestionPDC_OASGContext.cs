using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Data
{
    public partial class Prj_GestionPDC_OASGContext : DbContext
    {
        public Prj_GestionPDC_OASGContext (DbContextOptions<Prj_GestionPDC_OASGContext> options)
            : base(options)
        {
        }

        //public DbSet<Prj_GestionPDC_OASG.Entities.Pais> Pais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DepartamentoId).HasColumnName("Departamento_Id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Ciudad)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ciudad_Departamento1");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaisId).HasColumnName("Pais_Id");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Departamento)
                    .HasForeignKey(d => d.PaisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departamento_Pais1");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PermisosRol>(entity =>
            {
                entity.ToTable("Permisos_Rol");

                entity.Property(e => e.CodigoFuncion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("Rol_Id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.PermisosRol)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permisos_Rol_Rol");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.ToTable("Rol_Usuario");

                entity.Property(e => e.RolId).HasColumnName("Rol_Id");

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_Rol_Usuario_Rol");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Rol_Usuario_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasColumnName("Usuario")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<PermisosRol> PermisosRol { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
