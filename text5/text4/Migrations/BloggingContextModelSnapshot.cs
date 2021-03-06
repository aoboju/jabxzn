﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using text4;

namespace text4.Migrations
{
    [DbContext(typeof(BloggingContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("text4.Blog", b =>
                {
                    b.Property<string>("num")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Sitevalue");

                    b.Property<string>("Url");

                    b.Property<string>("siteId");

                    b.Property<string>("siteProperty");

                    b.HasKey("num");

                    b.ToTable("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
