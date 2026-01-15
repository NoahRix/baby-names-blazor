namespace blazor_frontend.Services;

public class AppState
{
    public StateInfo? SelectedState { get; private set; }
    public int? CommitedYear { get; private set; }
    public int? SelectedYear { get; private set; }
    public int? SelectedStateMinYear { get; private set; }
    public int? SelectedStateMaxYear { get; private set; }
    public int? DistinctMaleCountForSelectedState { get; private set; }
    public int? DistinctFemaleCountForSelectedState { get; private set; }
    public TopPopularMaleFemaleNameCounts? TopPopularMaleFemaleNameCounts { get; private set; }
    public List<YearGenderCount>? YearGenderCounts { get; private set; }

    public event Action? OnChange;

    public void SetSelectedState(StateInfo? state)
    {
        SelectedState = state;
        NotifyStateChanged();
    }

    public void SetStateWithYearRange(StateInfo? state, int minYear, int maxYear)
    {
        SelectedState = state;
        SelectedStateMinYear = minYear;
        SelectedStateMaxYear = maxYear;
        NotifyStateChanged();
    }

    public void SetCommitedYear(int? year)
    {
        Console.WriteLine($"SetCommitedYear called with: {year}");
        CommitedYear = year;
        Console.WriteLine($"CommitedYear now: {CommitedYear}");
        NotifyStateChanged();
    }

    public void SetSelectedYear(int? year)
    {
        SelectedYear = year;
        NotifyStateChanged();
    }

    public void SetSelectedStateMinYear(int? year)
    {
        SelectedStateMinYear = year;
        NotifyStateChanged();
    }

    public void SetSelectedStateMaxYear(int? year)
    {
        SelectedStateMaxYear = year;
        NotifyStateChanged();
    }

    public void SetDistinctMaleCountForSelectedState(int? count)
    {
        DistinctMaleCountForSelectedState = count;
        NotifyStateChanged();
    }

    public void SetDistinctFemaleCountForSelectedState(int? count)
    {
        DistinctFemaleCountForSelectedState = count;
        NotifyStateChanged();
    }

    public void SetTopPopularMaleFemaleNameCounts(TopPopularMaleFemaleNameCounts? counts)
    {
        TopPopularMaleFemaleNameCounts = counts;
        NotifyStateChanged();
    }

    public void SetYearGenderCounts(List<YearGenderCount>? counts)
    {
        YearGenderCounts = counts;
        NotifyStateChanged();
    }

    public void SetAllStateData(
        StateInfo? state,
        int minYear,
        int maxYear,
        int? commitedYear,
        List<YearGenderCount>? yearGenderCounts,
        int? maleCount,
        int? femaleCount,
        TopPopularMaleFemaleNameCounts? topNames)
    {
        SelectedState = state;
        SelectedStateMinYear = minYear;
        SelectedStateMaxYear = maxYear;
        if (commitedYear.HasValue)
        {
            CommitedYear = commitedYear;
        }
        YearGenderCounts = yearGenderCounts;
        DistinctMaleCountForSelectedState = maleCount;
        DistinctFemaleCountForSelectedState = femaleCount;
        TopPopularMaleFemaleNameCounts = topNames;
        
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        var handlers = OnChange?.GetInvocationList();
        if (handlers == null) return;
        
        Console.WriteLine($"NotifyStateChanged called - {handlers.Length} subscribers");
        
        foreach (var handler in handlers)
        {
            try
            {
                ((Action)handler)?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in event handler: {ex.Message}");
            }
        }
    }
}

public class StateInfo
{
    public string StateCode { get; set; } = string.Empty;
    public string StateName { get; set; } = string.Empty;
    public string DrawPath { get; set; } = string.Empty;
}

public class TopPopularMaleFemaleNameCounts
{
    public List<PieChartData> Male { get; set; } = new();
    public List<PieChartData> Female { get; set; } = new();
}

public class PieChartData
{
    public string Label { get; set; } = string.Empty;
    public int Value { get; set; }
}

public class YearGenderCount
{
    public int Year { get; set; }
    public int MaleCount { get; set; }
    public int FemaleCount { get; set; }
}
