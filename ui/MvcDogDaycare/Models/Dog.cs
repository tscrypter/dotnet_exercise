using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcDogDaycare.Models
{
    public class Dog
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [DisplayName("DOB")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [DisplayName("Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public String ContactNumber { get; set; }
        
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}