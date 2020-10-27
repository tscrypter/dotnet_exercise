using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDogDaycare.Models
{
    public class DogDao
    {
        
        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [DisplayName("DOB")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [DisplayName("Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public String ContactNumber { get; set; }
        
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}