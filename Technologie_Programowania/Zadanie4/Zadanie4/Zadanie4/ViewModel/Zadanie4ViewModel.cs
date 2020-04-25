using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uslugi;

namespace Zadanie4.ViewModel
{
    public class Zadanie4ViewModel : INotifyPropertyChanged
    {
        private DataRepository dataRepository = new DataRepository();


        public Zadanie4ViewModel()
        {
            SelectProductsCmd = new RelayCommand(pars => SelectProducts());
            InsertCmd = new RelayCommand(pars => Insert());
            UpdateCmd = new RelayCommand(pars => Update());
            DeleteCmd = new RelayCommand(pars => Delete());
            SelectReviewsCmd = new RelayCommand(pars => SelectReviews());
        }


        public event PropertyChangedEventHandler PropertyChanged = null;
        public ICommand SelectProductsCmd { get; set; }
        public ICommand InsertCmd { get; set; }
        public ICommand UpdateCmd { get; set; }
        public ICommand DeleteCmd { get; set; }
        public ICommand SelectReviewsCmd { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Select operation (products or reviews)
        /// </summary>

        private List<Product> products;

        public List<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
                this.OnPropertyChanged("Products");
            }
        }

        private List<ProductReview> review;

        public List<ProductReview> Review
        {
            get
            {
                return this.review;
            }
            set
            {
                this.review = value;
                this.OnPropertyChanged("Review");
            }
        }

        public void SelectProductsAsync()
        {
            Task.Run(() => {
                SelectProducts();
            });
        }
        public void SelectProducts()
        {
            this.Products = this.dataRepository.GetProducts();
        }

        public void SelectReviews()
        {
            Task.Run(() => {
                this.Review = this.dataRepository.GetReviews();
            });
        }

        /// <summary>
        /// Insert operation on product review
        /// </summary>

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                this.OnPropertyChanged("Email");
            }
        }

        private int raiting;
        public int Raiting
        {
            get
            {
                return this.raiting;
            }
            set
            {
                this.raiting = value;
                this.OnPropertyChanged("Raiting");
            }
        }
        private string comment;
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
                this.OnPropertyChanged("Comment");
            }
        }
        public void Insert()
        {
            Task.Run(() => {
                this.dataRepository.InsertProductReview(idProduct,name,email,raiting,comment);
                this.Review = this.dataRepository.GetReviews();
            });

        }


        /// <summary>
        /// Update operation 
        /// </summary>
        private int idProduct;
        public int IdProduct
        {
            get
            {
                return this.idProduct;
            }
            set
            {
                this.idProduct = value;
                this.OnPropertyChanged("IdProduct");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }
       

        public void Update()
        {
                Task.Run(() =>
                {
                    this.dataRepository.UpdateProductReview(idReview, comment);
                    this.Review = this.dataRepository.GetReviews();
                });
        }

        /// <summary>
        /// Delete operation 
        /// </summary>
        /// 
        private int idReview;
        public int IdReview
        {
            get
            {
                return this.idReview;
            }
            set
            {
                this.idReview = value;
                this.OnPropertyChanged("IdReview");
            }
        }
        public void Delete()
        {
                Task.Run(() =>
                {
                    this.dataRepository.DeleteProductReview(idReview);
                    this.Review = this.dataRepository.GetReviews();
                });
        }
    }
}
