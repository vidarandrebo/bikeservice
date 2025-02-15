using System;
using System.Collections.Generic;

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

public static class ServiceNoteExtensions
{
    public static void AddServiceNote(this List<ServiceNote> notes, DateTime date, double mileage, string noteText)
    {
        var note = new ServiceNote(mileage, date, noteText);
        notes.Add(note);
    }
}