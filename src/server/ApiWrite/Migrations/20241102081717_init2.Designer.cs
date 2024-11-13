﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiWrite.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241102081717_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Types.CommunicationItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnnouncementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommunicationProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreationBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NewsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("NewsId");

                    b.ToTable("CommunicationItem");
                });

            modelBuilder.Entity("Domain.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpireDuration")
                        .HasColumnType("int");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerScopeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShouldAuthenticated")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("Announcement");
                });

            modelBuilder.Entity("Domain.AnnouncementFile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnnouncementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("AnnouncementFile");
                });

            modelBuilder.Entity("Domain.GroupNews", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpireDuration")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerScopeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShouldAuthenticated")
                        .HasColumnType("bit");

                    b.Property<string>("Summery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("GroupNews");
                });

            modelBuilder.Entity("Domain.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpireDuration")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerScopeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShouldAuthenticated")
                        .HasColumnType("bit");

                    b.Property<string>("Summery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Domain.NewsContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Domain.SliderImageItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OrderRank")
                        .HasColumnType("float");

                    b.Property<Guid?>("SliderImagesContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("SliderImagesContentId");

                    b.ToTable("SliderImageItem");
                });

            modelBuilder.Entity("Domain.BottomImageContent", b =>
                {
                    b.HasBaseType("Domain.NewsContent");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BottomImageContents", (string)null);
                });

            modelBuilder.Entity("Domain.SliderImagesContent", b =>
                {
                    b.HasBaseType("Domain.NewsContent");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SliderImagesContents", (string)null);
                });

            modelBuilder.Entity("Domain.TopBottomImageContent", b =>
                {
                    b.HasBaseType("Domain.NewsContent");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("TopBottomImageContents", (string)null);
                });

            modelBuilder.Entity("Domain.TopImageContent", b =>
                {
                    b.HasBaseType("Domain.NewsContent");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("TopImageContents", (string)null);
                });

            modelBuilder.Entity("Core.Types.CommunicationItem", b =>
                {
                    b.HasOne("Domain.Announcement", null)
                        .WithMany("Communications")
                        .HasForeignKey("AnnouncementId");

                    b.HasOne("Domain.News", null)
                        .WithMany("Communications")
                        .HasForeignKey("NewsId");
                });

            modelBuilder.Entity("Domain.Announcement", b =>
                {
                    b.OwnsMany("Core.Domain.AccessEntityValue", "AccessEntityItems", b1 =>
                        {
                            b1.Property<Guid>("AnnouncementId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("ScopeId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AnnouncementId", "Id");

                            b1.ToTable("Announcement_AccessEntityItems");

                            b1.WithOwner()
                                .HasForeignKey("AnnouncementId");
                        });

                    b.OwnsOne("Domain.FileImage", "Image", b1 =>
                        {
                            b1.Property<Guid>("AnnouncementId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AnnouncementId");

                            b1.ToTable("Announcement");

                            b1.WithOwner()
                                .HasForeignKey("AnnouncementId");
                        });

                    b.OwnsOne("Domain.FileImage", "TitleImage", b1 =>
                        {
                            b1.Property<Guid>("AnnouncementId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AnnouncementId");

                            b1.ToTable("Announcement");

                            b1.WithOwner()
                                .HasForeignKey("AnnouncementId");
                        });

                    b.Navigation("AccessEntityItems");

                    b.Navigation("Image");

                    b.Navigation("TitleImage");
                });

            modelBuilder.Entity("Domain.AnnouncementFile", b =>
                {
                    b.HasOne("Domain.Announcement", null)
                        .WithMany("Files")
                        .HasForeignKey("AnnouncementId");
                });

            modelBuilder.Entity("Domain.GroupNews", b =>
                {
                    b.OwnsMany("Core.Domain.AccessEntityValue", "AccessEntityItems", b1 =>
                        {
                            b1.Property<Guid>("GroupNewsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("ScopeId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GroupNewsId", "Id");

                            b1.ToTable("GroupNews_AccessEntityItems");

                            b1.WithOwner()
                                .HasForeignKey("GroupNewsId");
                        });

                    b.OwnsMany("Domain.GroupNewsItem", "Items", b1 =>
                        {
                            b1.Property<Guid>("GroupNewsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("NewsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.HasKey("GroupNewsId", "Id");

                            b1.ToTable("GroupNewsItem");

                            b1.WithOwner()
                                .HasForeignKey("GroupNewsId");
                        });

                    b.Navigation("AccessEntityItems");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.News", b =>
                {
                    b.HasOne("Domain.NewsContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");

                    b.OwnsMany("Core.Domain.AccessEntityValue", "AccessEntityItems", b1 =>
                        {
                            b1.Property<Guid>("NewsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("ScopeId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("NewsId", "Id");

                            b1.ToTable("News_AccessEntityItems");

                            b1.WithOwner()
                                .HasForeignKey("NewsId");
                        });

                    b.OwnsOne("Domain.FileImage", "TitleImage", b1 =>
                        {
                            b1.Property<Guid>("NewsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("NewsId");

                            b1.ToTable("News");

                            b1.WithOwner()
                                .HasForeignKey("NewsId");
                        });

                    b.Navigation("AccessEntityItems");

                    b.Navigation("Content");

                    b.Navigation("TitleImage");
                });

            modelBuilder.Entity("Domain.SliderImageItem", b =>
                {
                    b.HasOne("Domain.SliderImagesContent", null)
                        .WithMany("SliderImageItems")
                        .HasForeignKey("SliderImagesContentId");

                    b.OwnsOne("Domain.FileImage", "Image", b1 =>
                        {
                            b1.Property<Guid>("SliderImageItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SliderImageItemId");

                            b1.ToTable("SliderImageItem");

                            b1.WithOwner()
                                .HasForeignKey("SliderImageItemId");
                        });

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Domain.BottomImageContent", b =>
                {
                    b.OwnsOne("Domain.FileImage", "Image", b1 =>
                        {
                            b1.Property<Guid>("BottomImageContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BottomImageContentId");

                            b1.ToTable("BottomImageContents");

                            b1.WithOwner()
                                .HasForeignKey("BottomImageContentId");
                        });

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Domain.TopBottomImageContent", b =>
                {
                    b.OwnsOne("Domain.FileImage", "BottomImage", b1 =>
                        {
                            b1.Property<Guid>("TopBottomImageContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TopBottomImageContentId");

                            b1.ToTable("TopBottomImageContents");

                            b1.WithOwner()
                                .HasForeignKey("TopBottomImageContentId");
                        });

                    b.OwnsOne("Domain.FileImage", "TopImage", b1 =>
                        {
                            b1.Property<Guid>("TopBottomImageContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TopBottomImageContentId");

                            b1.ToTable("TopBottomImageContents");

                            b1.WithOwner()
                                .HasForeignKey("TopBottomImageContentId");
                        });

                    b.Navigation("BottomImage");

                    b.Navigation("TopImage");
                });

            modelBuilder.Entity("Domain.TopImageContent", b =>
                {
                    b.OwnsOne("Domain.FileImage", "Image", b1 =>
                        {
                            b1.Property<Guid>("TopImageContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TopImageContentId");

                            b1.ToTable("TopImageContents");

                            b1.WithOwner()
                                .HasForeignKey("TopImageContentId");
                        });

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Domain.Announcement", b =>
                {
                    b.Navigation("Communications");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("Domain.News", b =>
                {
                    b.Navigation("Communications");
                });

            modelBuilder.Entity("Domain.SliderImagesContent", b =>
                {
                    b.Navigation("SliderImageItems");
                });
#pragma warning restore 612, 618
        }
    }
}