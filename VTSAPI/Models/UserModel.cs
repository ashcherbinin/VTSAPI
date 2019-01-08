using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static VTSAPI.Models.ToDoAPIModel;

namespace VTSAPI.Models
{
    public class UserModel
    {
        public UserModel()
        {
           
        }

        public class User
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
      
        }
          
    }
}
