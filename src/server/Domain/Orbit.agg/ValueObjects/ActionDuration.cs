using Core;

namespace Domain;

public record ActionDuration(int? StartYear,Month? StartMonth, int? StartWeek, int? EndYear, Month? EndMonth, int? EndWeek)
{
    public ActionDuration() : this(null, null, null, null, null, null) { }

    //public int? Year { get; set; } = Year;
    public int? StartYear { get; set; } = StartYear;
    public Month? StartMonth { get; set; } = StartMonth;
    public int? StartWeek { get; set; } = StartWeek;
    public int? EndYear { get; set; } = EndYear;
    public Month? EndMonth { get; set; } = EndMonth;
    public int? EndWeek { get; set; } = EndWeek;


    [Calculated]
    public int? Start => StartWeek != null ? int.Parse($"{StartYear.Value}{ToMonthString(StartMonth.Value)}{StartWeek.Value}") : null;

    [Calculated]
    public int? End => EndWeek != null ? int.Parse($"{EndYear.Value}{ToMonthString(EndMonth.Value)}{EndWeek.Value}") : null;

    public string ToMonthString(Month month) => month < Month.October ? $"0{(int)month}" : $"{(int)month}";
}