using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace Application.Framework;

public class XmlLinqExpressionParser
{
    private AdditionalInfo _info = new();
    private string conditionString = string.Empty;

    public XmlLinqExpressionParser()
    { }

    public Func<T, bool> ParsePredicateOf<T>(JsonDocument doc)
    {
        return ParseExpressionOf<T>(doc).Compile();
    }

    public override string ToString()
    {
        return conditionString;
    }

    public Expression<Func<T, bool>> ParseExpressionOf<T>(JsonDocument doc)
    {
        var itemExpression = Expression.Parameter(typeof(T));
        var conditions = ParseTree<T>(doc.RootElement, itemExpression);
        if (conditions.CanReduce)
        {
            conditions = conditions.ReduceAndCheck();
        }
        conditionString = conditions.ToString();

        var x =  Expression.Lambda<Func<T, bool>>(conditions, itemExpression);
        
        return x;
    }

    private readonly MethodInfo MethodContains = typeof(Enumerable)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Single(m => m.Name == nameof(Enumerable.Contains) && m.GetParameters().Length == 2);

    private delegate Expression Binder(Expression left, Expression right);

    private Expression ParseTree<T>(JsonElement condition, ParameterExpression parm)
    {
        Expression left = null;
        Expression right = null;
        var gate = condition.GetProperty(nameof(condition)).GetString();
        JsonElement searchRuleList = condition.GetProperty(nameof(searchRuleList));
        Binder binder = gate == AppLogicGate.And ? (Binder)Expression.And : Expression.Or;
        Expression bind(Expression left, Expression right) => left == null ? right : binder(left, right);
        foreach (var rule in searchRuleList.EnumerateArray())
        {
            right = null;
            if (rule.TryGetProperty(nameof(condition), out JsonElement check))
            {
                right = ParseTree<T>(rule, parm);
                if (right != null) left = bind(left, right);
                continue;
            }
            string name = rule.GetProperty(nameof(name)).GetString();
            string type = rule.GetProperty(nameof(type)).GetString();
            string matchMode = rule.GetProperty(nameof(matchMode)).GetString();
            string searchValue = rule.GetProperty(nameof(searchValue)).GetString();
            var property = Expression.Property(parm, name);
            switch (matchMode)
            {
                case AppFilterMode.ValueIn:
                case AppFilterMode.Contains:
                    switch (type)
                    {
                        case AppFilterType.String:
                            right = Expression.Call(
                             MethodContains.MakeGenericMethod(typeof(string)),
                             Expression.Constant(searchValue.Split(","), typeof(string[])),
                             property);
                            break;
                    }
                    break;
                case AppFilterMode.NotContains:
                    switch (type)
                    {
                        case AppFilterType.String:
                            right = Expression.Call(
                             MethodContains.MakeGenericMethod(typeof(string)),
                             Expression.Constant(searchValue.Split(","), typeof(string[])),
                             property);
                            right = Expression.Not(right);
                            break;
                    }
                    break;
                case AppFilterMode.StartsWith:
                case AppFilterMode.EndsWith:
                    right = Expression.Call(
                           property,
                           typeof(string).GetMethod(matchMode, new[] { typeof(string) }),
                           Expression.Constant(searchValue, typeof(string)));
                    break;
                case AppFilterMode.DateIs:
                case AppFilterMode.ValueEquals:
                    switch (type)
                    {
                        case AppFilterType.Guid:
                            if (Guid.TryParse(searchValue, out Guid guidValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(guidValue, typeof(Guid)));
                            }
                            break;
                        case AppFilterType.DropDown:
                        case AppFilterType.Text:
                        case AppFilterType.String:
                            right = Expression.Equal(property, Expression.Constant(searchValue, typeof(string)));
                            break;
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Boolean:
                            if (bool.TryParse(searchValue, out bool boolValue))
                            {
                                right = Expression.Equal(property, Expression.Constant(boolValue, typeof(bool)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.Equal(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateIsNot:
                case AppFilterMode.ValueNotEquals:
                    switch (type)
                    {
                        case AppFilterType.String:
                            right = Expression.NotEqual(property, Expression.Constant(searchValue, typeof(string)));
                            break;
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Boolean:
                            if (bool.TryParse(searchValue, out bool boolValue))
                            {
                                right = Expression.NotEqual(property, Expression.Constant(boolValue, typeof(bool)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.NotEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateBefore:
                case AppFilterMode.LessThan:
                    switch (type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchValue, out double doubleValue))
                            {
                                right = Expression.LessThan(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.LessThan(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.LessThenEqual:
                    switch (type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchValue, out double doubleValue))
                            {
                                right = Expression.LessThanOrEqual(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.LessThanOrEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.DateAfter:
                case AppFilterMode.GreaterThan:
                    switch (type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchValue, out double doubleValue))
                            {
                                right = Expression.GreaterThan(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.GreaterThan(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
                            }
                            break;
                    }
                    break;
                case AppFilterMode.GreaterThanEqual:
                    switch (type)
                    {
                        case AppFilterType.Integer:
                            if (int.TryParse(searchValue, out int intValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(intValue, typeof(int)));
                            }
                            break;
                        case AppFilterType.Numeric:
                            if (decimal.TryParse(searchValue, out decimal decimalValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(decimalValue, typeof(decimal)));
                            }
                            break;
                        case AppFilterType.Double:
                            if (double.TryParse(searchValue, out double doubleValue))
                            {
                                right = Expression.GreaterThanOrEqual(property, Expression.Constant(doubleValue, typeof(double)));
                            }
                            break;
                        case AppFilterType.DateTime:
                            if (DateTime.TryParse(searchValue, out DateTime dateTimeValue))
                            {
                                left = Expression.GreaterThanOrEqual(property, Expression.Constant(AppUtility.EnsureUtcDate(dateTimeValue), typeof(DateTime)));
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