using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Gravy
{
    public class Rule
    {
        public static Rule Parse(string ruleText)
        {
            return new Rule { RuleText = ruleText } ;            
        }

        private string RuleText { get; set; }

        public string Apply<T>(T dataItem)
        {
            var stringBuilder = new StringBuilder(RuleText);
            var matchCollection = Regex.Matches(RuleText, @"\{[\w\.]+([:]?[^\}]+)\}");

            foreach (Match m in matchCollection)
            {
                var matchValue = m.Value;
#if DEBUG
                Trace.WriteLine("Matched " + matchValue);
#endif
                var splitItems = matchValue.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);

                var propertyName = splitItems[0].Replace("{", string.Empty).Replace("}", string.Empty);
                var formatString = splitItems.Length > 1 ? "{0:" + splitItems[1] : "{0}";
                var childProperties = propertyName.Split(new[] {'.'}, StringSplitOptions.None);

                object propertyValue = string.Empty;
                object data = dataItem;

                foreach (var prop in childProperties)
                {
                    propertyValue = GetProperty(data, prop);
                    data = propertyValue;
                }

                var returnedValue = string.Format(formatString, propertyValue);
                stringBuilder.Replace(matchValue, returnedValue);
            }
            return stringBuilder.ToString();

        }
        public static object GetProperty(object target, string name)
        {
            try
            {
                var site = System.Runtime.CompilerServices.CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, name, target.GetType(), new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
                
                return site.Target(site, target);
            }
            catch (Exception)
            {
                return "ERROR: No property by the name '" + name + "' exists on the datasource.";                
            }
        }
    }
}