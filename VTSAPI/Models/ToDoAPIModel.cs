using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace VTSAPI.Models
{
    public class ToDoAPIModel
    {


        public class User
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string LastName { get; set; }
            public string FirstMidName { get; set; }
         

            public ICollection<TodoList> todoLists { get; set; }
        }


        public class TodoList
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int TodoListID { get; set; }
  
            public int UserID { get; set; }
            public string Name { get; set; }
            public bool isDefault { get; set; } = false;

            public User User { get; set; }

        }


        public class TodoItem
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int TodoItemID { get; set; }
            public string Name { get; set; }
            public bool isComplete { get; set; } = false;

            public ICollection<TodoList> todoLists { get; set; }
        }


        public class ItemList
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ItemListID { get; set; }
          
            public int TodoItemId { get; set; }
            public TodoItem todoItems { get; set; }

            public int TodoListID { get; set; }
            public TodoList todoLists { get; set; }
 
        }


        public class ToDoContext : DbContext
        {
            public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
            {

            }

            public DbSet<User> Users { get; internal set; }
       
            public DbSet<TodoItem> TodoItems { get; internal set; }
            public DbSet<TodoList> TodoLists { get; internal set; }




            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().ToTable("Users");
       
                modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
                modelBuilder.Entity<TodoList>().ToTable("TodoLists");
                modelBuilder.Entity<ItemList>().ToTable("ItemList");


            }

        }

    }
}
