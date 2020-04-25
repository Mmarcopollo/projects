using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi
{
    public class DataRepository
    {
        public List<Product> GetProducts()
        {
            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                List<Product> products = (from p in db.Products
                                                select p).ToList();
                return products;
            }
        }
        public List<ProductReview> GetReviews()
        {
            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                List<ProductReview> reviews = (from p in db.ProductReviews
                                                 select p).ToList();
                return reviews;

            }
        }

        public void InsertProductReview(int idProduct,string name,string email,int raiting,string comment)
        {

            AdventureWorks2014 db = new AdventureWorks2014();

            ProductReview prod = new ProductReview();
            prod.ProductID = idProduct;
            prod.ReviewerName = name;
            prod.ReviewDate = DateTime.Now;
            prod.EmailAddress = email;
            prod.Rating = raiting;
            prod.Comments = comment;
            prod.ModifiedDate = DateTime.Now;


            db.ProductReviews.Add(prod);
                db.SaveChanges();


        }

        public void UpdateProductReview( int idReview, string comment)
        {
            AdventureWorks2014 db = new AdventureWorks2014();

            ProductReview review = db.ProductReviews.Where(d => d.ProductReviewID == idReview).First();

            if (review != null)
            {
                review.Comments = comment;
                db.SaveChanges();
            }

        }

        public void DeleteProductReview(int idReview)
        {
            AdventureWorks2014 db = new AdventureWorks2014();

            

            ProductReview delete = db.ProductReviews.Where(d => d.ProductReviewID == idReview).First();

            if (delete != null)
            {
                db.ProductReviews.Remove(delete);
                db.SaveChanges();
            }
        }

        public bool CheckExistsOfIdReview (int idReview)
        {
            bool x = false;

            using (AdventureWorks2014 db = new AdventureWorks2014())
            {
                List<ProductReview> reviews = (from p in db.ProductReviews
                                          select p).ToList();

                foreach( ProductReview i in reviews)
                {
                    if(i.ProductID == idReview)
                    {
                        x = true;
                        break;
                    }
                }
            }

            return x;
        }



    }
}
