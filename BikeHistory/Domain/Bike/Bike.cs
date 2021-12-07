namespace BikeHistory.Domain.Bike
{
    public class Bike
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int UserId { get; set; }

        public Bike(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }
    }
}