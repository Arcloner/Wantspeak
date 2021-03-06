﻿using Automation.Workspace.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Test
{
    [TestClass]
    public class BaseTestCase
    {
        readonly string basePage = "https://wantspeak.azurewebsites.net";

        protected Tester tester = new Tester();

        [TestInitialize]
        public void SetUp()
        {
            tester.GoToPage(basePage);            
        }

        [TestCleanup]
        public void TearDown()
        {
            tester.FinishesWork();            
        }
    }
}
