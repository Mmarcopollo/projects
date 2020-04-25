using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Zadanie3_EntityFramework;

namespace Zadanie3_Testy
{
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void GetProductsWithUnassignedCategory_GetProducts_ProductsEqualExpected()
        {
            List<Product> unassignedCategory = new List<Product>();

            Assert.AreEqual(209, unassignedCategory.GetProductsWithUnassignedCategory().Count);
        }
    }
}
