using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        public Dog Pet { get; set; }
    }
}