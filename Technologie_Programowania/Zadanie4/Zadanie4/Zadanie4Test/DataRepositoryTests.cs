using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uslugi;

namespace Zadanie4Test
{
    [TestClass]
    public class DataRepositoryTests
    {
        //coTestujesz_konteskst(czyli co robisz)_jakiPowinienByćWynik
        [TestMethod]
        public void GetProducts_GetProducts_ProductsNumberEqualRequested()
        {
            DataRepository dataRepository = new DataRepository();
            List<Product> buffer = dataRepository.GetProducts();

            Assert.AreEqual(504, buffer.Count);
            
        }
        [TestMethod]
        public void GetReviews_GetProductReviews_ReviewsNumberEqualRequested()
        {
            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                DataRepository dataRepository = new DataRepository();
                List<ProductReview> buffer = dataRepository.GetReviews();

                List<ProductReview> productReviews = (from p in db.ProductReviews
                                                      select p).ToList();

                Assert.AreEqual(productReviews.Count, buffer.Count);
            }
        }

        [TestMethod]
        public void InsertProduct_InsertingProduct_NumberOfReviecsBoforeAndAfterInsert()
        {
            DataRepository dataRepository = new DataRepository();
            List<ProductReview> buffer = dataRepository.GetReviews();
            int numberOfElements = buffer.Count;
            dataRepository.InsertProductReview(1, "Marek", "test@test", 3, "Test");
            buffer = dataRepository.GetReviews();

            Assert.AreEqual(numberOfElements+1, buffer.Count);

        }

        [TestMethod]
        public void UpdateProductReview_UpdateReview_CheckNewReview()
        {
            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                DataRepository dataRepository = new DataRepository();
                dataRepository.UpdateProductReview(2, "TestowaRecenzja");

                ProductReview buffer = (from p in db.ProductReviews
                             where p.ProductReviewID == 2
                             select p).First();

                Assert.AreEqual("TestowaRecenzja", buffer.Comments);

            }
        }


        [TestMethod]
        public void DeleteProductReview_DeleteReview_CheckNumberOfReview()
        {

            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                int biggestNumber = (from p in db.ProductReviews
                                     select p.ProductReviewID).Max();

                DataRepository dataRepository = new DataRepository();
                List<ProductReview> buffer = dataRepository.GetReviews();
                int numberOfElements = buffer.Count;

                dataRepository.DeleteProductReview(biggestNumber);
                buffer = dataRepository.GetReviews();

                Assert.AreEqual(numberOfElements - 1, buffer.Count);
            }
        }

    }
}
