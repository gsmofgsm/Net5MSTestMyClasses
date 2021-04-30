using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyClassesTest
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }
        protected string _GoodFileName;

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
