using System;

namespace BikeService.Domain.Bikes;

public class ServiceEntry
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }

    public ServiceEntry(DateTime date, string text)
    {
        Id = Guid.NewGuid();
        Date = date;
        Text = text;
    }
}