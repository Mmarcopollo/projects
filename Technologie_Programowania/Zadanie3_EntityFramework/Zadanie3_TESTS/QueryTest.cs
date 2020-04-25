using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3_EntityFramework;

namespace Zadanie3_Testy
{
    [TestClass]
    public class QueryTest
    {

        [TestMethod]
        public void GetProductsByName_GetProducts_ProductsNameEqualRequested()
        {
            const string name = "Freewheel";
            for (int i = 0; i < Query.GetProductsByName(name).Count; ++i)
            {
                Assert.AreEqual(name, Query.GetProductsByName(name)[i].Name);
            }
        }

        [TestMethod]
        public void GetProductsByVendorName_GetProducts_ProductsVendorNameEqualRequested()
        {
            const string name = "International";

            Assert.AreEqual(1, Query.GetProductsByVendorName(name).Count);
        }

        [TestMethod]
        public void GetProductNamesByVendorName_GetProducts_ProductsNameEqualRequested()
        {
            const string name = "International";
            Assert.AreEqual(1, Query.GetProductNamesByVendorName(name).Count);
        }

        [TestMethod]
        public void GetProductVendorByProductName_GetVendor_VendorNameEqualRequested()
        {
            const string name = "Chainring";
            Assert.AreEqual(3, Query.GetProductVendorByProductName(name).Count);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews_GetProducts_ProductsEqualRequested()
        {
            const int reviewsCount = 2;

            Assert.AreEqual(1, Query.GetProductsWithNRecentReviews(reviewsCount).Count);
        }

        [TestMethod]
        public void GetNRecentlyReviewedProducts_GetProducts_ProductsEqualRequested()
        {
            const int reviewsCount = 2;
            Assert.AreEqual(2, Query.GetNRecentlyReviewedProducts(reviewsCount).Count);
        }

        [TestMethod]
        public void GetNProductsFromCategory_GetProducts_ProductsEqualRequested()
        {
            const string name = "Bikes";
            const int productCount = 2;

            Assert.AreEqual(2, Query.GetNProductsFromCategory(name, productCount).Count);
        }





    }
}
