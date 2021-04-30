using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MyClassesTest
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }
        protected string _GoodFileName;

        protected void WriteDescription(Type typ)
        {
            string testName = TestContext.TestName;

            // Find the test method currently executing
            MemberInfo method = typ.GetMethod(testName);
            if (method != null)
            {
                // See if the [Description] attribute exists on this test
                Attribute attr = method.GetCustomAttribute(
                    typeof(DescriptionAttribute));
                if (attr != null)
                {
                    // Cast the attribute to a DescriptionAttribute
                    DescriptionAttribute dattr = (DescriptionAttribute)attr;
                    // Display the test description
                    TestContext.WriteLine("Test Description: " + dattr.Description);
                }
            }
        }

        protected void SetGoodFileName()
        {
            _GoodFileName = TestContext.Properties["GoodFileName"].ToString();

            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                // the file path will be C:\Users\qing.ma\AppData\Roaming\TestFile.txt
            }
        }
    }
}
