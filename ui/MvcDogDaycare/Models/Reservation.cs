using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MvcDogDaycare.Models
{
    public class Reservation
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        
        [DisplayName("Drop Off Time")]
        [DataType(DataType.DateTime)]
        public DateTime DropOffDttm { get; set; }
        
        [DisplayName("Pick Up Time")]
        [DataType(DataType.DateTime)]
        public DateTime PickUpDttm { get; set; }

        [HiddenInput]
        public int PetId { get; set; }
        public Dog Pet { get; set; }
    }
}