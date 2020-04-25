using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3_EntityFramework
{
    public class Query
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            AdventureWorks2014Entities db = new AdventureWorks2014Entities();

            List<Product> list = (from p in db.Product
                                  where namePart == p.Name
                                  select p).ToList();

            return list;
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            List<Product> list = (from p in dc.Product
                                      from pv in dc.ProductVendor
                                      from v in dc.Vendor
                                      where vendorName == v.Name
                                      where p.ProductID == pv.ProductID
                                      where pv.BusinessEntityID == v.BusinessEntityID
                                      select p).ToList();

                return list;
          
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            List<String> list = (from p in dc.Product
                                     from pv in dc.ProductVendor
                                     from v in dc.Vendor
                                     where p.ProductID == pv.ProductID
                                     where pv.BusinessEntityID == v.BusinessEntityID
                                     where vendorName == v.Name
                                     select p.Name).ToList();

                return list;
            
        }

        public static List<String> GetProductVendorByProductName(string productName)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            List<String> name = (from p in dc.Product
                           from pv in dc.ProductVendor
                           from v in dc.Vendor
                               where p.ProductID == pv.ProductID
                               where pv.BusinessEntityID == v.BusinessEntityID
                               where productName == p.Name
                               select v.Name).ToList();

            

                return name;
            
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {

            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();
            List<Product> list = (from p in dc.Product
                                      where howManyReviews == (from pr in dc.ProductReview
                                                               where p.ProductID == pr.ProductID
                                                               select pr.ProductID).Count()
                                      select p).ToList();

                return list;
        
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {

            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();
            List<Product> list = (from p in dc.Product
                                      from pr in dc.ProductReview
                                      where p.ProductID == pr.ProductID
                                      orderby pr.ReviewDate descending
                                      select p).Take(howManyProducts).ToList();

                return list;
           
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int numberOfProducts)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            List<Product> list = (from p in dc.Product
                                      from ps in dc.ProductSubcategory
                                      from pc in dc.ProductCategory
                                      where p.ProductSubcategoryID == ps.ProductSubcategoryID
                                      where ps.ProductCategoryID == pc.ProductCategoryID
                                      where pc.Name == categoryName
                                      orderby pc.Name
                                      orderby p.Name
                                      select p).Take(numberOfProducts).ToList();

                return list;
           
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            int suma = (from p in dc.Product
                            from ps in dc.ProductSubcategory
                            from pc in dc.ProductCategory
                            where p.ProductSubcategoryID == ps.ProductSubcategoryID
                            where ps.ProductCategoryID == pc.ProductCategoryID
                            where pc == category
                            select (int)p.StandardCost).Distinct().Sum();


                return suma;
            
        }
    }
}
