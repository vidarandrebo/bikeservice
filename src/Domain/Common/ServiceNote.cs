using System;
using System.Collections.Generic;

namespace BikeService.Domain.Common;

public class ServiceNote : BaseEntity
{
    public double Mileage { get; set; }
    public DateOnly Date { get; set; }
    public string Note { get; set; }
}

public static class ServiceNoteExtensions
{
    public static void AddServiceNote(this List<ServiceNote> notes, DateOnly date, double mileage, string noteText)
    {
        var note = new ServiceNote()
        {
            Id = Guid.NewGuid(),
            Note = noteText,
            Mileage = mileage,
            Date = date
        };
        notes.Add(note);
    }
}