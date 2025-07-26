namespace Application.Framework;

public record SearchCriteria : BaseRecord
{
    public int Index { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public IEnumerable<SearchRule> SearchRuleList { get; set; }
    public IEnumerable<SortCondition> SortList { get; set; }

    public SearchCondition ToSearchCondition()
    {
        var searchCondition = new SearchCondition();
        var subSearchCondition = new SearchCondition();
        int count = 1;

        foreach (var item in SearchRuleList)
        {
            if (count == 1)
            {
                searchCondition.Condition = item.Condition;
                searchCondition.SearchRuleList.Add(item);
            }
            else
            {
                if (item.Condition != searchCondition.Condition)
                {
                    subSearchCondition.Condition = item.Condition;
                    subSearchCondition.SearchRuleList.Add(item);
                }
                else
                {
                    if (!string.IsNullOrEmpty(subSearchCondition.Condition))
                    {
                        searchCondition.SearchRuleList.Add(subSearchCondition);
                        subSearchCondition = new SearchCondition();
                    }
                    searchCondition.SearchRuleList.Add(item);
                }
            }
            count++;
        }
        return searchCondition;
    }

    public SearchCriteria()
    {
        SearchRuleList = new List<SearchRule>();
        SortList = new List<SortCondition>();
    }
}