using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3_EntityFramework
{
    public static class ExtensionMethods
    {
        public static List<Product> GetProductsWithUnassignedCategory(this List<Product> list)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            var listOfProducts = (from p in dc.Product
                                  where p.ProductSubcategoryID == null
                                  select p).ToList();

            list = listOfProducts;

            return listOfProducts;
        }
        public static string ProductsWithVendor(this List<Product> List)
        {
            List<Vendor> vendors = new List<Vendor>();
            List<ProductVendor> productVendor = new List<ProductVendor>();

            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();
            vendors = dc.Vendor.ToList();
                productVendor = dc.ProductVendor.ToList();

            

            var productsWithVendors =
             (from p in List
              from pv in productVendor
              where p.ProductID == pv.ProductID
              select new { Product = p.Name, Vendor = pv.Vendor.Name }).Distinct();


            var productsWithVendorsString =
                (from p in productsWithVendors
                 select p.Product + "-" + p.Vendor);


            var productsWithVendorsToString = productsWithVendorsString.Aggregate((a, b) => a + "\n" + b);


            return productsWithVendorsToString;

        }


        public static List<List<Product>> DivideProductsOnSites(this List<Product> listOfProducts, int sizeOfPage, int numberOfPage)
        {
            List<List<Product>> divideProductsOnSites = new List<List<Product>>();
            divideProductsOnSites.Add(new List<Product>());
            for (int i = 0; i < listOfProducts.Count; i++)
            {
                if (i / sizeOfPage > divideProductsOnSites.Count - 1)
                    divideProductsOnSites.Add(new List<Product>());
                divideProductsOnSites[divideProductsOnSites.Count - 1].Add(listOfProducts[i]);
            }
            return divideProductsOnSites;
        }
    }
}
