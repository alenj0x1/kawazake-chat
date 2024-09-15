using System;
using System.Collections.Generic;
using KawasakeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KawasakeChat.Domain.Context;

public partial class KawasakeChatDbContext : DbContext
{
    public KawasakeChatDbContext()
    {
    }

    public KawasakeChatDbContext(DbContextOptions<KawasakeChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Groupchat> Groupchats { get; set; }

    public virtual DbSet<Groupchatmember> Groupchatmembers { get; set; }

    public virtual DbSet<Groupchatmemberrole> Groupchatmemberroles { get; set; }

    public virtual DbSet<Groupchatmessage> Groupchatmessages { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    public virtual DbSet<Useraccountrole> Useraccountroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Groupchat>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("groupchat_pkey");

            entity.ToTable("groupchat");

            entity.Property(e => e.GroupId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("group_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.InviteCode)
                .HasMaxLength(50)
                .HasColumnName("invite_code");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Private)
                .HasDefaultValue(false)
                .HasColumnName("private");
        });

        modelBuilder.Entity<Groupchatmember>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("groupchatmember");

            entity.HasIndex(e => e.MemberId, "groupchatmember_member_id_key").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("joined_at");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Role)
                .HasDefaultValue(2)
                .HasColumnName("role");

            entity.HasOne(d => d.Group).WithMany()
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_group_id_fkey");

            entity.HasOne(d => d.Member).WithOne()
                .HasForeignKey<Groupchatmember>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_member_id_fkey");

            entity.HasOne(d => d.RoleNavigation).WithMany()
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_role_fkey");
        });

        modelBuilder.Entity<Groupchatmemberrole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("groupchatmemberrole_pkey");

            entity.ToTable("groupchatmemberrole");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Groupchatmessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("groupchatmessage_pkey");

            entity.ToTable("groupchatmessage");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Groupchatmessages)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmessage_group_id_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.Groupchatmessages)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmessage_member_id_fkey");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("useraccount_pkey");

            entity.ToTable("useraccount");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasDefaultValue(3)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Useraccounts)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("useraccount_role_fkey");
        });

        modelBuilder.Entity<Useraccountrole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("useraccountrole_pkey");

            entity.ToTable("useraccountrole");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
