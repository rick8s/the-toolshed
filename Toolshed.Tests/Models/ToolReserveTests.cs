﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toolshed.Models;
using System.Collections.Generic;

namespace Toolshed.Tests.Models
{
    [TestClass]
    public class ToolReserveTests
    {
        [TestMethod]
        public void ToolReserveEnsureICanCreateAnInstance()
        {
            ToolReserve a_reservation = new ToolReserve();
            Assert.IsNotNull(a_reservation);
        }
        
        [TestMethod]
        public void ToolReserveAReservationHasItsInfo()
        {
            ToolReserve a_reserved_tool = new ToolReserve();

            a_reserved_tool.ReserveDate = "12-21";
            a_reserved_tool.ItemId = 1;
            a_reserved_tool.Who = "toolman";
            a_reserved_tool.ReserveId = 2;

            Assert.AreEqual("12-21", a_reserved_tool.ReserveDate);
            Assert.AreEqual(1, a_reserved_tool.ItemId);
            Assert.AreEqual("toolman", a_reserved_tool.Who);
            Assert.AreEqual(2, a_reserved_tool.ReserveId);
        } 
    }
}
