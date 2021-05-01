using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest : TestBase
    {
        private const string BAD_FILE_NAME = @"C:\Users\qing.ma\bogus.txt";

        [ClassInitialize()]
        public static void ClassInitialize(TestContext tc)
        {
            // TODO: Initialize for all tests in class
            tc.WriteLine("In ClassInitialize() method");
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            // TODO: Cleanup after all tests in class
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("In TestInitialize() method");

            WriteDescription(this.GetType());

            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Create file: " + _GoodFileName);
                    // Create the 'Good' file.
                    File.AppendAllText(_GoodFileName, "Some Text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("In TestCleanup() method");

            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                // Delete file
                if (File.Exists(_GoodFileName))
                {
                    TestContext.WriteLine("Deleting file: " + _GoodFileName);
                    File.Delete(_GoodFileName);
                }
            }
        }

        [TestMethod]
        [Description("Check to see if a file exists.")]
        [Owner("QingMa")]
        [Priority(1)]
        [TestCategory("NoException")]
        //[Ignore]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine("Checking File " + _GoodFileName);

            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        //[TestMethod]
        //[Timeout(3000)]
        //public void SimulateTimeout()
        //{
        //    System.Threading.Thread.Sleep(4000);
        //}

        [TestMethod]
        [Description("Check to see if a file does not exist.")]
        [Owner("QingMa")]
        [Priority(2)]
        [TestCategory("NoException")]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine("Checking File " + BAD_FILE_NAME);

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Check for a thrown ArgumentNullException using ExpectedException attribute.")]
        [Owner("Mas")]
        [Priority(3)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();
            fp.FileExists("");
        }

        [TestMethod]
        [Description("Check for a thrown ArgumentNullException using try...catch.")]
        public void FileNameNullOrEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                // Test was a success
                return;
            }

            // Fail the test
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException");
        }
    }
}
