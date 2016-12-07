using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LegnicaIT.DataAccess.Context;

namespace JwtAuthServer.Migrations
{
    [DbContext(typeof(JwtDbContext))]
    [Migration("20161207153932_hah")]
    partial class hah
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("App");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<DateTime>("EmailConfirmedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasColumnType("ntext")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.UserApps", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppId");

                    b.Property<int?>("UserId");

                    b.HasKey("id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("UserApps");
                });

            modelBuilder.Entity("LegnicaIT.DataAccess.Models.UserApps", b =>
                {
                    b.HasOne("LegnicaIT.DataAccess.Models.App", "App")
                        .WithMany()
                        .HasForeignKey("AppId");

                    b.HasOne("LegnicaIT.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
