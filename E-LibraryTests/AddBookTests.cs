using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace E_Library.Tests
{
    [TestClass()]
    public class AddBookTests
    {
        [TestMethod()]
        public void addBookFuncTest()
        {
            AddBook a = new AddBook();
            a.addBookFunc(10, "Mapping Experiences Test :)", @"D:\HaniASUS\Computer Science\BI\PDFs\Mapping Experiences.pdf", 2, "2017-5-14", 200, 2, "6844-546-54-1", "Bla Bla Bla Bla",new byte[10],null);
        }
    }
}