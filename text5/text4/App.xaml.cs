using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace text4
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
    class ifcData
    {
        public string Property { get; set; }
        public string Value { get; set; }

    }
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ifc;Trusted_Connection=True;");

        }

    }
    public class Blog
    {
        [Key]
        public string   num { get; set; }
        public string Url { get; set; }
        public string siteId { get; set; }
        public string siteProperty { get; set; }
        public string Sitevalue { get; set; }

    }
}
