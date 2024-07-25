using Core;

namespace Domain;

public record YearMonth(int Year, Month Month)
{
    public YearMonth() : this(2024, Month.January) { }

    public int Year{ get; set; } = Year;
    public Month Month{ get; set; } = Month;
}