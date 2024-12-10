﻿// <auto-generated />
using System;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Case2BookingCoworking.Infrastructure.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20241210065759_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Audience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Audiences", (string)null);
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.History", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AudienceId")
                        .HasColumnType("uuid");

                    b.Property<string>("AudienceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AudienceId");

                    b.HasIndex("UserId");

                    b.ToTable("History", (string)null);
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AudienceId")
                        .HasColumnType("uuid");

                    b.Property<string>("AudienceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndOfBooking")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartOfBooking")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AudienceId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullCredential")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelegramId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UniversityEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Audience", b =>
                {
                    b.OwnsOne("Case2BookingCoworking.Core.Domain.Entities.AudienceType", "Type", b1 =>
                        {
                            b1.Property<Guid>("AudienceId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("AudienceId");

                            b1.ToTable("Audiences");

                            b1.WithOwner()
                                .HasForeignKey("AudienceId");
                        });

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.History", b =>
                {
                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.Audience", "Audience")
                        .WithMany()
                        .HasForeignKey("AudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audience");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Order", b =>
                {
                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.Audience", "Audience")
                        .WithMany("Orders")
                        .HasForeignKey("AudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audience");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.User", b =>
                {
                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.Profile", "Profile")
                        .WithOne("User")
                        .HasForeignKey("Case2BookingCoworking.Core.Domain.Entities.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Case2BookingCoworking.Core.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Audience", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.Profile", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Case2BookingCoworking.Core.Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}