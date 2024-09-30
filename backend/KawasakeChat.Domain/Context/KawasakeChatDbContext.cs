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

    public virtual DbSet<Tokenaccess> Tokenaccesses { get; set; }

    public virtual DbSet<Tokenrefresh> Tokenrefreshes { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    public virtual DbSet<Useraccountrole> Useraccountroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Groupchat>(entity =>
        {
            entity.HasKey(e => e.GroupChatId).HasName("groupchat_pkey");

            entity.ToTable("groupchat");

            entity.Property(e => e.GroupChatId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("group_chat_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
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
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Private)
                .HasDefaultValue(false)
                .HasColumnName("private");

            entity.HasOne(d => d.Owner).WithMany(p => p.Groupchats)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchat_owner_id_fkey");
        });

        modelBuilder.Entity<Groupchatmember>(entity =>
        {
            entity.HasKey(e => e.GroupChatMemberId).HasName("groupchatmember_pkey");

            entity.ToTable("groupchatmember");

            entity.HasIndex(e => e.MemberId, "groupchatmember_member_id_key").IsUnique();

            entity.Property(e => e.GroupChatMemberId).HasColumnName("group_chat_member_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
            entity.Property(e => e.GroupChatId).HasColumnName("group_chat_id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("joined_at");
            entity.Property(e => e.MemberId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("member_id");
            entity.Property(e => e.Role)
                .HasDefaultValue(2)
                .HasColumnName("role");
            entity.Property(e => e.RoleGrantedBy).HasColumnName("role_granted_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.Groupchatmembers)
                .HasForeignKey(d => d.GroupChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_group_chat_id_fkey");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Groupchatmembers)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_role_fkey");

            entity.HasOne(d => d.RoleGrantedByNavigation).WithMany(p => p.InverseRoleGrantedByNavigation)
                .HasPrincipalKey(p => p.MemberId)
                .HasForeignKey(d => d.RoleGrantedBy)
                .HasConstraintName("groupchatmember_role_granted_by_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Groupchatmembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmember_user_id_fkey");
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
            entity.Property(e => e.GroupChatId).HasColumnName("group_chat_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.Groupchatmessages)
                .HasForeignKey(d => d.GroupChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmessage_group_chat_id_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.Groupchatmessages)
                .HasPrincipalKey(p => p.MemberId)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupchatmessage_member_id_fkey");
        });

        modelBuilder.Entity<Tokenaccess>(entity =>
        {
            entity.HasKey(e => e.TokenAccessId).HasName("tokenaccess_pkey");

            entity.ToTable("tokenaccess");

            entity.Property(e => e.TokenAccessId).HasColumnName("token_access_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Expiration).HasColumnName("expiration");
            entity.Property(e => e.TokenRefreshId).HasColumnName("token_refresh_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.TokenRefresh).WithMany(p => p.Tokenaccesses)
                .HasForeignKey(d => d.TokenRefreshId)
                .HasConstraintName("tokenaccess_token_refresh_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Tokenaccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tokenaccess_user_id_fkey");
        });

        modelBuilder.Entity<Tokenrefresh>(entity =>
        {
            entity.HasKey(e => e.TokenRefreshId).HasName("tokenrefresh_pkey");

            entity.ToTable("tokenrefresh");

            entity.Property(e => e.TokenRefreshId).HasColumnName("token_refresh_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Expiration).HasColumnName("expiration");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.User).WithMany(p => p.Tokenrefreshes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tokenrefresh_user_id_fkey");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("useraccount_pkey");

            entity.ToTable("useraccount");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
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
