using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uslugi;
using Zadanie4.ViewModel;

namespace Zadanie4Test
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void SelectProducts_SelectProducts_CheckTimeOfSelecting()
        {
            Zadanie4ViewModel vm = new Zadanie4ViewModel();

            vm.SelectProducts();
            Thread.Sleep(3000);
            Assert.IsTrue(vm.Products.Count() > 0);

        }
        [TestMethod]
        public void SelectReviews_SelectReviews_CheckTimeOfSelecting()
        {
            Zadanie4ViewModel vm = new Zadanie4ViewModel();

            vm.SelectReviews();
            Thread.Sleep(3000);
            Assert.IsTrue(vm.Review.Count() > 0);

        }

        [TestMethod]
        public void Insert_InsertNewReview_CheckTimeOfInsertingAndNumberOfReviews()
        {
            Zadanie4ViewModel vm = new Zadanie4ViewModel();

            vm.IdProduct = 1;
            vm.Name = "Marek";
            vm.Email= "test@test";
            vm.Raiting = 3;
            vm.Comment = "Test";

            vm.SelectReviews();
            Thread.Sleep(5000);

            int numberOfElements = vm.Review.Count;

            vm.Insert();
            Thread.Sleep(5000);
            
            Assert.AreEqual(numberOfElements + 1, vm.Review.Count);

        }

        [TestMethod]
        public void Update_UpdateReview_CheckTimeOfUpdating()
        {
            Zadanie4ViewModel vm = new Zadanie4ViewModel();

            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                vm.IdReview = 2;
                vm.Comment = "Test";
                vm.Update();
                Thread.Sleep(3000);
                ProductReview buffer = (from p in db.ProductReviews
                                        where p.ProductReviewID == 2
                                        select p).First();
                Assert.AreEqual("Test", buffer.Comments);

            }

        }


        [TestMethod]
        public void Delete_DeleteReview_CheckTimeOfDeleting()
        {
            Zadanie4ViewModel vm = new Zadanie4ViewModel();

            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                int biggestNumber = (from p in db.ProductReviews
                                     select p.ProductReviewID).Max();

                vm.IdReview = biggestNumber;
                vm.SelectReviews();
                Thread.Sleep(3000);
                int numberOfElements = vm.Review.Count;

                vm.Delete();
                Thread.Sleep(5000);
                Assert.AreEqual(numberOfElements - 1, vm.Review.Count);
            }

        }
    }
}
