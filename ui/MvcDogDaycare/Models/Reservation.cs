using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using MvcDogDaycare.Services;

namespace MvcDogDaycare.Models
{
    public class Reservation
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        
        
        [Required]
        [DisplayName("Drop Off Time")]
        [DataType(DataType.DateTime)]
        public DateTime DropOffDttm { get; set; }
        
        [Required]
        [DisplayName("Pick Up Time")]
        [DataType(DataType.DateTime)]
        public DateTime PickUpDttm { get; set; }

        
        [Required]
        [HiddenInput]
        public int PetId { get; set; }
        public DogDao Pet { get; set; }

        [Required]
        [DisplayName("Daycare Facility")]
        public int FacilityId { get; set; }
    }
}