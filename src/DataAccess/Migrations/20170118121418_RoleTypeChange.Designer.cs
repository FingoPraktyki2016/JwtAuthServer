﻿using System;
using LegnicaIT.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LegnicaIT.DataAccess.Migrations
{
    [DbContext(typeof(JwtDbContext))]
    [Migration("20170118121418_RoleTypeChange")]
    partial class RoleTypeChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(150)");

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<DateTime?>("EmailConfirmedOn");

                    b.Property<DateTime?>("LockedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(128)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.UserApps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("Role");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("UserApps");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.UserApps", b =>
                {
                    b.HasOne("LegnicaIT.DataAccess.Models.App", "App")
                        .WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LegnicaIT.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
