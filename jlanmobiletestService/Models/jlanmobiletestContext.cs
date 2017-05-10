using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using jlanmobiletestService.DataObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace jlanmobiletestService.Models
{
    public class jlanmobiletestContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public jlanmobiletestContext() : base(connectionStringName)
        {
        } 

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<TodoItem>()
                .HasRequired(todo => todo.Author)
                .WithMany(author => author.TodoItems);

            modelBuilder.Entity<TodoItem>()
                .HasKey(todo => todo.Id)
                .Property(todo => todo.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Author>()
                .HasKey(author => author.Id)
                .Property(author => author.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


        }


    }

}
