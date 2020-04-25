using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3_EntityFramework
{
    [Table(name:"MyProduct")]
    public class MyProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public System.Nullable<decimal> Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public System.Nullable<int> ProductSubcategoryID { get; set; }
        public System.Nullable<int> ProductModelID { get; set; }
        public System.DateTime SellStartDate { get; set; }
        public System.Nullable<System.DateTime> SellEndDate { get; set; }
        public System.Nullable<System.DateTime> DiscontinuedDate { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public MyProduct()
        {

        }
        /*
        public MyProduct(Product product)
        {
            ProductID = product.ProductID;
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            MakeFlag = product.MakeFlag;
            FinishedGoodsFlag = product.FinishedGoodsFlag;
            Color = product.Color;
            SafetyStockLevel = product.SafetyStockLevel;
            ReorderPoint = product.ReorderPoint;
            StandardCost = product.StandardCost;
            ListPrice = product.ListPrice;
            Size = product.Size;
            SizeUnitMeasureCode = product.SizeUnitMeasureCode;
            WeightUnitMeasureCode = product.WeightUnitMeasureCode;
            Weight = product.Weight;
            DaysToManufacture = product.DaysToManufacture;
            ProductLine = product.ProductLine;
            Class = product.Class;
            Style = product.Style;
            ProductSubcategoryID = product.ProductSubcategoryID;
            ProductModelID = product.ProductModelID;
            SellStartDate = product.SellStartDate;
            SellEndDate = product.SellEndDate;
            DiscontinuedDate = product.DiscontinuedDate;
            rowguid = product.rowguid;
            ModifiedDate = product.ModifiedDate;
        }
        */
        public static List<MyProduct> MyProductFiller()
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

                List<MyProduct> listOfMyProducts = (from product in dc.Product
                                                    select new MyProduct()
                                                    {
                                                        ProductID = product.ProductID,
                                                        Name = product.Name,
                                                        ProductNumber = product.ProductNumber,
                                                        MakeFlag = product.MakeFlag,
                                                        FinishedGoodsFlag = product.FinishedGoodsFlag,
                                                        Color = product.Color,
                                                        SafetyStockLevel = product.SafetyStockLevel,
                                                        ReorderPoint = product.ReorderPoint,
                                                        StandardCost = product.StandardCost,
                                                        ListPrice = product.ListPrice,
                                                        Size = product.Size,
                                                        SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                                                        WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                                                        Weight = product.Weight,
                                                        DaysToManufacture = product.DaysToManufacture,
                                                        ProductLine = product.ProductLine,
                                                        Class = product.Class,
                                                        Style = product.Style,
                                                        ProductSubcategoryID = product.ProductSubcategoryID,
                                                        ProductModelID = product.ProductModelID,
                                                        SellStartDate = product.SellStartDate,
                                                        SellEndDate = product.SellEndDate,
                                                        DiscontinuedDate = product.DiscontinuedDate,
                                                        rowguid = product.rowguid,
                                                        ModifiedDate = product.ModifiedDate
                                                        }).ToList();

        

                return listOfMyProducts;
            
        }

        public static List<MyProduct> GetProductsByName(List<MyProduct> listOfMyProducts, string namePart)
        {

            List<MyProduct> list = (from p in listOfMyProducts
                                    where p.Name == namePart
                                    select p).ToList();

            return list;
        }

        public static List<MyProduct> GetProductsWithNRecentReviews(List<MyProduct> listOfMyProducts, int howManyReviews)
        {
            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();

            List<MyProduct> list = (from p in listOfMyProducts
                                        where howManyReviews == (from pr in dc.ProductReview
                                                                 where p.ProductID == pr.ProductID
                                                                 select pr.ProductID).Count()
                                        select p).ToList();

                return list;
            
        }
        public static List<MyProduct> GetNRecentlyReviewedProducts(List<MyProduct> listOfMyProducts, int howManyProducts)
        {

            AdventureWorks2014Entities dc = new AdventureWorks2014Entities();
            List<MyProduct> list = (from p in listOfMyProducts
                                  from pr in dc.ProductReview
                                  where p.ProductID == pr.ProductID
                                  orderby pr.ReviewDate descending
                                  select p).Take(howManyProducts).ToList();

            return list;

        }
    }
}
