namespace Expenses.Repository;
public class DbSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string CategoryCollection { get; set; }

    public string SubcategoryCollection { get; set; }

    public string PeriodCollection { get; set; }
}
