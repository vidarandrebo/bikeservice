using System;

namespace BikeHistory.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public User Owner { get; set; }
    }
}