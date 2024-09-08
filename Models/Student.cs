using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreDemo.Models
{
    public class Student
    {
        public int StudId {get;set;}

        [Required]
        public string? Name {get;set;}

        [Required]
        public string? Gender {get;set;}

        [Required]
        public string? Department {get;set;}

        [Required]
        public string? City {get;set;}
    }
}