using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;

namespace Gravy.Tests
{
    [TestFixture]
    public class DataFormatterTests
    {
        [Test]
        public void TestSingleBasicFormattingRule()
        {
            var myTodoList = TodoListItem.GetTodoList();

            var memStream = new MemoryStream();
            var outputter = new TextFileDataFormatter<TodoListItem>
            {
                Data = myTodoList,
                Stream = memStream,
                FormattingRule = Rule.Parse("{Category}, {Description}")
            };
            
            outputter.Format();

            memStream.Seek(0, SeekOrigin.Begin);

            var text = new StreamReader(memStream).ReadToEnd();

            Assert.That(text, Is.EqualTo(
@"Home Repair, Replace the screen door.
Home Repair, Fix the clogged sink
Home Repair, Put the flooring down
Purchases, School supplies for the kids
Purchases, Pet food
"));
        }


    }
}
