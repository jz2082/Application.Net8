using System.Linq.Expressions;
using System.Reflection;

namespace Application.Framework;

public class LinqExpressionParser
{
    private AdditionalInfo _info = new();
    private string _conditionString = string.Empty;

    public LinqExpressionParser()
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "LinqExpressionParser");
    }

    public Func<T, bool> ParsePredicateOf<T>(SearchCondition searchCondition)
    {
        return ParseExpressionOf<T>(searchCondition).Compile();
    }

    public override string ToString()
    {
        return _conditionString;
    }

    public Expression<Func<T, bool>> ParseExpressionOf<T>(SearchCondition searchCondition)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ParseExpressionOf");
        info.Add(AppLogger.Message, $"public Expression<Func<T, bool>> ParseExpressionOf<T>(SearchCondition searchCondition) {searchCondition.GetValue()}");
        try
        {
            var itemExpression = Expression.Parameter(typeof(T));
            var conditions = ParseTree<T>(searchCondition, itemExpression);
            if (conditions != null && conditions.CanReduce)
            {
                conditions = conditions.ReduceAndCheck();
            }
            _conditionString = conditions!=null?conditions.ToString():string.Empty;
            info.Add("ExpressionText", _conditionString);
            return Expression.Lambda<Func<T, bool>>(conditions, itemExpression);
        }
        catch (Exception ex)
        {
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw new Exception(ex.Message, ex);
        }
    }

    private readonly MethodInfo MethodContains = typeof(Enumerable)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Single(m => m.Name == nameof(Enumerable.Contains) && m.GetParameters().Length == 2);

    private delegate Expression Binder(Expression left, Expression right);

    private Expression ParseTree<T>(SearchCondition condition, ParameterExpression parm)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, " ParseTree");
        info.Add(AppLogger.Message, "private Expression ParseTree<T>(SearchCondition condition, ParameterExpression parm)");

        Expression left = null;
        Expression right = null;
        var ruleList = condition.SearchRuleList;
        Binder binder = condition.Condition == AppLogicGate.And ? (Binder)Expression.And : Expression.Or;
        Expression bind(Expression left, Expression right) => left == null ? right : binder(left, right);
        foreach (var rule in ruleList)
        {
            right = null;
            if (rule.GetType() == typeof(SearchCondition))
            {
                right = ParseTree<T>((SearchCondition)rule, parm);
                if (right != null) left = bind(left, right);
                continue;
            }
            var searchRule = (SearchRule)rule;
            var property = Expression.Property(parm, searchRule.Name);
            switch (searchRule.MatchMode)
            {
                case AppFilterMode.ValueIn:
                case AppFilterMode.Contains:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Text:
                        case AppFilterType.String:
                            Expression childLeft = null;
                            foreach (var item in searchRule.SearchValue.Split(",").ToList())
                            {
                                Binder childBinder = Expression.Or;
                                var chileRight = Expression.Call(
                                    property,
                                    typeof(string).GetMethod(searchRule.MatchMode, new[] { typeof(string) }),
                                    Expression.Constant(item.Trim(), typeof(string)));
                                if (chileRight != null) childLeft = childLeft == null ? chileRight: childBinder(childLeft, chileRight);
                            }
                            if (childLeft != null) right = childLeft;
                            break;
                    }
                    break;
                case AppFilterMode.NotContains:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Text:
                        case AppFilterType.String:
                            Expression childLeft = null;
                            foreach (var item in searchRule.SearchValue.Split(",").ToList())
                            {
                                Binder childBinder = Expression.Or;
                                var chileRight = Expression.Call(
                                    property,
                                    typeof(string).GetMethod(AppFilterMode.Contains, new[] { typeof(string) }),
                                    Expression.Constant(item.Trim(), typeof(string)));
                                if (chileRight != null) childLeft = childLeft == null ? chileRight : childBinder(childLeft, chileRight);
                            }
                            if (childLeft != null) right = Expression.Not(childLeft);
                            break;
                    }
                    break;
                case AppFilterMode.StartsWith:
                case AppFilterMode.EndsWith:
                    right = Expression.Call(
                           property,
                           typeof(string).GetMethod(searchRule.MatchMode, new[] { typeof(string) }),
                           Expression.Constant(searchRule.SearchValue, typeof(string)));
                    break;
                case AppFilterMode.DateIs:
                case AppFilterMode.ValueEquals:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Guid:
                            if (Guid.TryParse(searchRule.SearchValue, out Guid guidValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(guidValue, typeof(Guid)));
                            }
                            break;
                        case AppFilterType.DropDown:
                        case AppFilterType.Text:
                        case AppFilterType.String:
                            right = Expression.Equal(property, Expression.Constant(searchRule.SearchValue, typeof(string)));
                            break;
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.Boolean:
                            if (bool.TryParse(searchRule.SearchValue, out bool boolValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(boolValue, typeof(bool)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateIsNot:
                case AppFilterMode.ValueNotEquals:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Guid:
                            if (Guid.TryParse(searchRule.SearchValue, out Guid guidValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(guidValue, typeof(Guid)));
                            }
                            break;
                        case AppFilterType.DropDown:
                        case AppFilterType.Text:
                        case AppFilterType.String:
                            right = Expression.NotEqual(property, Expression.Constant(searchRule.SearchValue, typeof(string)));
                            break;
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.Boolean:
                            if (bool.TryParse(searchRule.SearchValue, out bool boolValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(boolValue, typeof(bool)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateBefore:
                case AppFilterMode.LessThan:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.LessThenEqual:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateAfter:
                case AppFilterMode.GreaterThan:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.GreaterThanEqual:
                    switch (searchRule.Type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchRule.SearchValue, out int intValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchRule.SearchValue, out decimal decimalValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchRule.SearchValue, out double doubleValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchRule.SearchValue, out DateTime dateTimeValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (right!= null) left = bind(left, right);
           
        }
        return left;
    }
}