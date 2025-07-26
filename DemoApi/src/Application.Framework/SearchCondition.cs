using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text.Json;

namespace Application.Framework;

public record SearchCondition : BaseRecord
{
    public string Condition { get; set; }
    public ArrayList SearchRuleList { get; set; }

    public SearchCondition EnsureSearchCondition(SearchCondition searchCondition)
    {
        var returnValue = searchCondition;
        var searchRuleList = new ArrayList();
        foreach (var rule in searchCondition.SearchRuleList)
        {
            if (rule.GetType() == typeof(JObject) || rule.GetType() == typeof(JsonElement))
            {
                var searchRule = JsonConvert.DeserializeObject<SearchRule>(rule.ToString());
                var searchChildCondition = JsonConvert.DeserializeObject<SearchCondition>(rule.ToString());
                if (!string.IsNullOrEmpty(searchChildCondition.Condition))
                {
                    searchRuleList.Add(EnsureSearchCondition(searchChildCondition));
                    continue;
                }
                else
                {
                    searchRuleList.Add(searchRule);
                }
            }
            else if (rule.GetType() == typeof(SearchCondition))
            {
                searchRuleList.Add(EnsureSearchCondition((SearchCondition)rule));
                continue;
            }
            else
            {
                searchRuleList.Add(rule);
            }
        }
        returnValue.SearchRuleList.Clear();
        returnValue.SearchRuleList.AddRange(searchRuleList);
        return returnValue;
    }


    public SearchCondition()
    {
        SearchRuleList = new ArrayList();
    }
}
