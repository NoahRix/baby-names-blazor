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
        CommitedYear = year;
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

    private void NotifyStateChanged() => OnChange?.Invoke();
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
