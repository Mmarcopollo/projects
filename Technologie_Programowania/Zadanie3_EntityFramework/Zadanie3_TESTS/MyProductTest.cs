using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3_EntityFramework;

namespace Zadanie3_Testy
{
    [TestClass]
    public class MyProductTest
    {
        [TestMethod]
        public void GetProductsByName_GetProducts_ProductsEqualExpected()
        {
            const string name = "Freewheel";

            List<MyProduct> myProduct = new List<MyProduct>();

            myProduct = MyProduct.MyProductFiller();

           
                Assert.AreEqual(1, MyProduct.GetProductsByName(myProduct,name).Count);
            
        }

        [TestMethod]
        public void GetNRecentlyReviewedProducts_GetProducts_ProductsEqualRequested()
        {
            const int reviewsCount = 2;
            Assert.AreEqual(2, MyProduct.GetNRecentlyReviewedProducts(MyProduct.MyProductFiller(), reviewsCount).Count);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews_GetProducts_ProductsEqualExpected()
        {
            const int reviewsCount = 2;

            Assert.AreEqual(1, MyProduct.GetProductsWithNRecentReviews(MyProduct.MyProductFiller(), reviewsCount).Count);
        }
    }
}
