using System;

namespace BikeHistory.Models
{
    public class Bike
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public User Owner { get; set; }
    }
}
