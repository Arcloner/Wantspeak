using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Test.TestCases
{
    [TestClass]
    public class TC1:BaseTestCase
    {
        [TestMethod]
        public void TestUserCanGetExpectedResultOfSearch()
        {
            var filmToSearch = "Пираты карибского моря";
            tester.AtStartPage().SearchFilm(filmToSearch);
            tester.AtResultsOfSearchPage().CheckFoundFilmContains(filmToSearch);
        }
    }
}
