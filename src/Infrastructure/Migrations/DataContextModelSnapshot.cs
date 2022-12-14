// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ShapesContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.ShapeAggregate.Shape", b =>
                {
                    b.Property<int>("ShapeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShapeId"));

                    b.HasKey("ShapeId");

                    b.ToTable("Shapes");
                });

            modelBuilder.Entity("Domain.ShapeAggregate.Vertex", b =>
                {
                    b.Property<int>("VertexId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VertexId"));

                    b.Property<int?>("ShapeId")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.Property<int>("Z")
                        .HasColumnType("integer");

                    b.HasKey("VertexId");

                    b.HasIndex("ShapeId");

                    b.ToTable("Vertex");
                });

            modelBuilder.Entity("Domain.ShapeAggregate.Vertex", b =>
                {
                    b.HasOne("Domain.ShapeAggregate.Shape", null)
                        .WithMany("Vertices")
                        .HasForeignKey("ShapeId");
                });

            modelBuilder.Entity("Domain.ShapeAggregate.Shape", b =>
                {
                    b.Navigation("Vertices");
                });
#pragma warning restore 612, 618
        }
    }
}
