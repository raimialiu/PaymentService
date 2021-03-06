﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessPayment.Models;

namespace ProcessPayment.Migrations
{
    [DbContext(typeof(ProcessPaymentDbContext))]
    [Migration("20210222034642_PaymentStateEntity")]
    partial class PaymentStateEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProcessPayment.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardHolderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("statePaymentStateId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("statePaymentStateId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("ProcessPayment.Models.PaymentState", b =>
                {
                    b.Property<int>("PaymentStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentStateId");

                    b.ToTable("paymentState");
                });

            modelBuilder.Entity("ProcessPayment.Models.Payment", b =>
                {
                    b.HasOne("ProcessPayment.Models.PaymentState", "state")
                        .WithMany()
                        .HasForeignKey("statePaymentStateId");

                    b.Navigation("state");
                });
#pragma warning restore 612, 618
        }
    }
}
