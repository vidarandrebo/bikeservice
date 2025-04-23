using System;

namespace BikeService.Domain.Common;

public class ServiceNote : BaseEntity
{
    public double Mileage { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }

    public ServiceNote(double mileage, DateTime date, string note)
    {
        Id = Guid.NewGuid();
        Mileage = mileage;
        Date = date;
        Note = note;
    }
}