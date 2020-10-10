﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastucture.Data.Configuration
{
    class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
           builder.ToTable("Comentario");

           builder.HasKey(e => e.CommentID);

           builder.Property(e => e.CommentID)
            .HasColumnName("IdComentario")
            .ValueGeneratedNever();

           builder.Property(e => e.PostId)
           .HasColumnName("idPublicacion");

           builder.Property(e => e.UserId)
              .HasColumnName("IdUsuario");

           builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("Descripcion")
                .HasMaxLength(500)
                .IsUnicode(false);


           builder.Property(e => e.Date)
            .HasColumnType("Fecha")
            ;
           builder.Property(e => e.IsActive)
           .HasColumnName("Activo");

           builder.HasOne(d => d.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Publicacion");

           builder.HasOne(d => d.User)
                .WithMany(p => p.Comment)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Usuario");
        }
    }
}
