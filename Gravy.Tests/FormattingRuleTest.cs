using System;
using NUnit.Framework;

namespace Gravy.Tests
{
    [TestFixture]
    public class FormattingRuleTest
    {
        const string descriptionItem = "I would do anything for love.";
        [Test]
        public void TestSimpleFormattingRule()
        {
            var rule = Rule.Parse("{Description}");
            var result = rule.Apply(new TodoListItem { Description = descriptionItem });

            Assert.That(result, Is.EqualTo(descriptionItem));
        }
        

        [TestCase("Description2")]
        [TestCase("Boogers")]
        [TestCase("DESCRIPTION")]
        public void TestNoPropertyByThatName(string ruleText)
        {
            var rule = Rule.Parse("{" + ruleText + "}");
            var result = rule.Apply(new TodoListItem { Description = descriptionItem });

            Assert.That(result, Is.EqualTo("ERROR: No property by the name '" + ruleText + "' exists on the datasource."));
        }

        [Test]
        public void FieldsWork()
        {
            var rule = Rule.Parse("{NewYork}");

            var result = rule.Apply(new TodoListItem { Description = descriptionItem });

            Assert.That(result, Is.EqualTo("New York"));
        }



        [Test]
        public void MethodsDontWork()
        {
            var rule = Rule.Parse("{ToString}");

            var result = rule.Apply(new TodoListItem { Description = descriptionItem });

            Assert.That(result, Is.StringContaining("ERROR"));
        }


        [Test]
        public void TestFormattingRuleWithExtraformattingFieldInformation()
        {
            var rule = Rule.Parse("Lyric: {Description}");

            var result = rule.Apply(new TodoListItem { Description = descriptionItem });

            Assert.That(result, Is.EqualTo("Lyric: " + descriptionItem));
        }

        [Test]
        public void TestMultipleProperties()
        {
            var rule = Rule.Parse("{Description}, {Category}");

            var result = rule.Apply(new TodoListItem {
                Description = descriptionItem,
                Category = "My Style"})
                ;

            Assert.That(result, Is.EqualTo(descriptionItem + ", My Style"));
        }


        [Test]
        public void TestFixedDecimalNumberFormatting()
        {
            var rule = Rule.Parse("{Description}, {Category}, {Cost:F2}");

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = 15.95812m
            })
                ;

            Assert.That(result, Is.EqualTo(descriptionItem + ", My Style, 15.96"));
        }

        [Test]
        public void TestNumberFormatting()
        {
            var rule = Rule.Parse("{Description}, {Category}, {Cost:0.00;(0.00)}");

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = -15.95812m
            });

            Assert.That(result, Is.EqualTo(descriptionItem + ", My Style, (15.96)"));
        }

        [Test]
        public void TestDefaultDateFormatting()
        {
            var rule = Rule.Parse("{Date}");
            var dateValue = new DateTime(2015, 12, 25);

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = -15.95812m,
                Completed = false,
                Date = dateValue
            });

            Assert.That(result, Is.EqualTo(dateValue.ToString()));
        }

        [Test]
        public void TestCustomizedDateFormatting()
        {
            var rule = Rule.Parse("{Date:MMMM dd, yyyy}");
            var dateValue = new DateTime(2015, 12, 25);

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = -15.95812m,
                Completed = false,
                Date = dateValue
            });

            Assert.That(result, Is.EqualTo("December 25, 2015"));
            
        }

        [Test]
        public void TestBool()
        {
            var rule = Rule.Parse("{Completed}");

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = -15.95812m,
                Completed = false
            });

            Assert.That(result, Is.EqualTo("False"));
        }


        [Test]
        public void TestObjectGraph()
        {
            var rule = Rule.Parse("{Sublist.Count}");

            var result = rule.Apply(new TodoListItem
            {
                Description = descriptionItem,
                Category = "My Style",
                Cost = -15.95812m,
                Completed = false
            });
            
            Assert.That(result, Is.EqualTo("0"));
        }
    }
}