using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace WebsiteDoChoi.Client
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private int productId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get product ID from query string
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"].ToString(), out productId))
                {
                    LoadProductDetails(productId);
                    LoadProductImages(productId);
                    LoadProductFeatures(productId);
                    LoadProductSpecifications(productId);
                    LoadProductReviews(productId);
                    LoadRelatedProducts(productId);
                    LoadShippingOptions();
                    LoadReturnPolicies();
                    UpdateCartCount();
                }
                else
                {
                    // Redirect to home if no valid product ID
                    Response.Redirect("/Client/Default.aspx");
                }
            }
        }

        #region Load Data Methods

        private void LoadProductDetails(int productId)
        {
            try
            {
                // Sample data - replace with actual database call
                var product = GetSampleProductData();

                lblProductName.Text = product.Name;
                Page.Title = product.Name + " - ToyLand";
                imgMainProduct.ImageUrl = product.MainImage;
                imgMainProduct.AlternateText = product.Name;

                lblBreadcrumbCategory.Text = product.CategoryName;
                lblBreadcrumbProduct.Text = product.Name;

                lblRating.Text = product.Rating.ToString("0.0");
                litRatingStars.Text = GenerateStars(product.Rating);
                lblReviewCount.Text = product.ReviewCount.ToString();
                lblStockStatus.Text = product.InStock ? "Còn hàng" : "Hết hàng";

                lblPrice.Text = product.Price.ToString("N0");

                if (product.OriginalPrice > product.Price)
                {
                    pnlOriginalPrice.Visible = true;
                    lblOriginalPrice.Text = product.OriginalPrice.ToString("N0");
                    decimal discount = ((product.OriginalPrice - product.Price) / product.OriginalPrice) * 100;
                    lblDiscountPercent.Text = Math.Round(discount).ToString();
                }

                lblAgeGroup.Text = product.AgeGroup;
                litProductDescription.Text = product.Description;
                lblTabReviewCount.Text = product.ReviewCount.ToString();
            }
            catch (Exception ex)
            {
                // Log error and show message
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải thông tin sản phẩm: {ex.Message}');", true);
            }
        }

        private void LoadProductImages(int productId)
        {
            try
            {
                var images = GetSampleProductImages();
                rptProductImages.DataSource = images;
                rptProductImages.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải hình ảnh sản phẩm: {ex.Message}');", true);
            }
        }

        private void LoadProductFeatures(int productId)
        {
            try
            {
                var features = GetSampleProductFeatures();
                rptProductFeatures.DataSource = features;
                rptProductFeatures.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải tính năng sản phẩm: {ex.Message}');", true);
            }
        }

        private void LoadProductSpecifications(int productId)
        {
            try
            {
                var specifications = GetSampleSpecifications();
                var leftSpecs = specifications.Take(5).ToList();
                var rightSpecs = specifications.Skip(5).ToList();

                rptSpecificationsLeft.DataSource = leftSpecs;
                rptSpecificationsLeft.DataBind();

                rptSpecificationsRight.DataSource = rightSpecs;
                rptSpecificationsRight.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải thông số kỹ thuật: {ex.Message}');", true);
            }
        }

        private void LoadProductReviews(int productId)
        {
            try
            {
                var reviews = GetSampleReviews();
                var ratingSummary = GetSampleRatingSummary();

                lblAverageRating.Text = "4.5";
                litAverageRatingStars.Text = GenerateStars(4.5);
                lblTotalReviews.Text = reviews.Count.ToString();

                rptRatingSummary.DataSource = ratingSummary;
                rptRatingSummary.DataBind();

                rptReviews.DataSource = reviews;
                rptReviews.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải đánh giá sản phẩm: {ex.Message}');", true);
            }
        }

        private void LoadRelatedProducts(int productId)
        {
            try
            {
                var relatedProducts = GetSampleRelatedProducts();
                rptRelatedProducts.DataSource = relatedProducts;
                rptRelatedProducts.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải sản phẩm liên quan: {ex.Message}');", true);
            }
        }

        private void LoadShippingOptions()
        {
            try
            {
                var shippingOptions = new List<object>
                {
                    new {
                        ShippingName = "Giao hàng tiêu chuẩn",
                        DeliveryTime = "3-5 ngày làm việc",
                        Price = 30000,
                        IsFree = false,
                        BackgroundColor = "gray"
                    },
                    new {
                        ShippingName = "Giao hàng nhanh",
                        DeliveryTime = "1-2 ngày làm việc",
                        Price = 50000,
                        IsFree = false,
                        BackgroundColor = "gray"
                    },
                    new {
                        ShippingName = "Miễn phí vận chuyển",
                        DeliveryTime = "Cho đơn hàng từ 500.000đ",
                        Price = 0,
                        IsFree = true,
                        BackgroundColor = "green"
                    }
                };

                rptShippingOptions.DataSource = shippingOptions;
                rptShippingOptions.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải thông tin vận chuyển: {ex.Message}');", true);
            }
        }

        private void LoadReturnPolicies()
        {
            try
            {
                var policies = new List<object>
                {
                    new {
                        PolicyTitle = "Đổi trả miễn phí",
                        PolicyDescription = "Trong vòng 7 ngày kể từ ngày nhận hàng"
                    },
                    new {
                        PolicyTitle = "Hoàn tiền 100%",
                        PolicyDescription = "Nếu sản phẩm có lỗi từ nhà sản xuất"
                    },
                    new {
                        PolicyTitle = "Hỗ trợ 24/7",
                        PolicyDescription = "Tư vấn và hỗ trợ khách hàng mọi lúc"
                    }
                };

                rptReturnPolicies.DataSource = policies;
                rptReturnPolicies.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải chính sách đổi trả: {ex.Message}');", true);
            }
        }

        private void UpdateCartCount()
        {
            try
            {
                // Get cart count from session or database
                int cartCount = Session["CartCount"] != null ? (int)Session["CartCount"] : 0;
            }
            catch (Exception ex)
            {
            }
        }


        #endregion

        #region Sample Data Methods

        private ProductInfo GetSampleProductData()
        {
            return new ProductInfo
            {
                Id = 1,
                Name = "Robot biến hình thông minh Transformer Pro",
                CategoryName = "Robot & Nhân vật",
                MainImage = "https://api.placeholder.com/600/600/FF6B6B/FFFFFF?text=Robot+Transformer",
                Price = 450000m,
                OriginalPrice = 550000m,
                Rating = 4.5,
                ReviewCount = 124,
                InStock = true,
                AgeGroup = "6-12 tuổi",
                Description = @"
                    <p class=""mb-4"">
                        Robot biến hình thông minh Transformer Pro là món đồ chơi tuyệt vời dành cho các bé yêu thích 
                        robot và công nghệ. Với khả năng biến hình đa dạng, robot có thể chuyển đổi giữa nhiều hình dạng 
                        khác nhau như xe hơi, máy bay, robot chiến đấu và nhiều hình thái thú vị khác.
                    </p>
                    <p class=""mb-4"">
                        Sản phẩm được trang bị công nghệ điều khiển giọng nói tiên tiến, cho phép trẻ em ra lệnh 
                        bằng tiếng Việt để robot thực hiện các hành động. Hệ thống đèn LED rực rỡ và âm thanh 
                        sống động tạo nên trải nghiệm chơi đầy thú vị và hấp dẫn.
                    </p>
                    <p class=""mb-4"">
                        Được làm từ chất liệu nhựa ABS cao cấp, an toàn tuyệt đối cho trẻ em, không chứa 
                        các chất độc hại. Thiết kế chắc chắn, bền bỉ, có thể chịu được va đập trong quá trình chơi.
                    </p>
                    <h4 class=""text-lg font-bold text-dark mb-2"">Lợi ích giáo dục:</h4>
                    <ul class=""list-disc list-inside space-y-1 mb-4"">
                        <li>Phát triển tư duy logic và khả năng giải quyết vấn đề</li>
                        <li>Kích thích trí tưởng tượng và sức sáng tạo</li>
                        <li>Rèn luyện kỹ năng vận động tinh và thô</li>
                        <li>Học hỏi về công nghệ và robot học cơ bản</li>
                        <li>Phát triển khả năng giao tiếp và ngôn ngữ</li>
                    </ul>"
            };
        }

        private List<ProductImage> GetSampleProductImages()
        {
            return new List<ProductImage>
            {
                new ProductImage { ImageUrl = "https://api.placeholder.com/600/600/FF6B6B/FFFFFF?text=Robot+1" },
                new ProductImage { ImageUrl = "https://api.placeholder.com/600/600/4ECDC4/FFFFFF?text=Robot+2" },
                new ProductImage { ImageUrl = "https://api.placeholder.com/600/600/FFE66D/1A535C?text=Robot+3" },
                new ProductImage { ImageUrl = "https://api.placeholder.com/600/600/1A535C/FFFFFF?text=Robot+4" },
                new ProductImage { ImageUrl = "https://api.placeholder.com/600/600/FF6B6B/FFFFFF?text=Robot+5" }
            };
        }

        private List<ProductFeature> GetSampleProductFeatures()
        {
            return new List<ProductFeature>
            {
                new ProductFeature { FeatureName = "Chế độ biến hình đa dạng" },
                new ProductFeature { FeatureName = "Điều khiển bằng giọng nói" },
                new ProductFeature { FeatureName = "Đèn LED và âm thanh sống động" },
                new ProductFeature { FeatureName = "Chất liệu an toàn, không độc hại" },
                new ProductFeature { FeatureName = "Phù hợp cho trẻ từ 6 tuổi trở lên" }
            };
        }

        private List<ProductSpecification> GetSampleSpecifications()
        {
            return new List<ProductSpecification>
            {
                new ProductSpecification { SpecName = "Kích thước", SpecValue = "25 x 15 x 30 cm" },
                new ProductSpecification { SpecName = "Trọng lượng", SpecValue = "800g" },
                new ProductSpecification { SpecName = "Chất liệu", SpecValue = "Nhựa ABS cao cấp" },
                new ProductSpecification { SpecName = "Nguồn điện", SpecValue = "Pin AA x 4 (không bao gồm)" },
                new ProductSpecification { SpecName = "Thời gian chơi", SpecValue = "4-6 giờ liên tục" },
                new ProductSpecification { SpecName = "Độ tuổi", SpecValue = "6+ tuổi" },
                new ProductSpecification { SpecName = "Màu sắc", SpecValue = "Đỏ, Xanh, Vàng" },
                new ProductSpecification { SpecName = "Xuất xứ", SpecValue = "Trung Quốc" },
                new ProductSpecification { SpecName = "Bảo hành", SpecValue = "12 tháng" },
                new ProductSpecification { SpecName = "Chứng nhận", SpecValue = "CE, RoHS, EN71" }
            };
        }

        private List<ProductReview> GetSampleReviews()
        {
            return new List<ProductReview>
            {
                new ProductReview
                {
                    Id = 1,
                    CustomerName = "Linh Nguyen",
                    Rating = 5,
                    ReviewContent = "Con trai mình rất thích sản phẩm này! Robot có thể biến hình thành nhiều dạng khác nhau, âm thanh và đèn LED rất đẹp. Chất lượng tốt, đóng gói cẩn thận.",
                    ReviewDate = DateTime.Now.AddDays(-3),
                    LikeCount = 12
                },
                new ProductReview
                {
                    Id = 2,
                    CustomerName = "Tuấn Minh",
                    Rating = 4,
                    ReviewContent = "Sản phẩm đúng như mô tả, robot khá đẹp và chắc chắn. Tuy nhiên giá hơi cao so với chất lượng. Nhìn chung vẫn ổn, con thích chơi.",
                    ReviewDate = DateTime.Now.AddDays(-7),
                    LikeCount = 8
                },
                new ProductReview
                {
                    Id = 3,
                    CustomerName = "Hồng Anh",
                    Rating = 5,
                    ReviewContent = "Tuyệt vời! Bé nhà mình 7 tuổi rất thích. Robot điều khiển giọng nói rất thú vị, biến hình mượt mà. Giao hàng nhanh, đóng gói kỹ càng. Sẽ ủng hộ shop tiếp.",
                    ReviewDate = DateTime.Now.AddDays(-14),
                    LikeCount = 15
                }
            };
        }

        private List<RatingSummary> GetSampleRatingSummary()
        {
            return new List<RatingSummary>
            {
                new RatingSummary { Stars = 5, Count = 93, Percentage = 75 },
                new RatingSummary { Stars = 4, Count = 19, Percentage = 15 },
                new RatingSummary { Stars = 3, Count = 10, Percentage = 8 },
                new RatingSummary { Stars = 2, Count = 1, Percentage = 1 },
                new RatingSummary { Stars = 1, Count = 1, Percentage = 1 }
            };
        }

        private List<RelatedProduct> GetSampleRelatedProducts()
        {
            return new List<RelatedProduct>
            {
                new RelatedProduct
                {
                    Id = 2,
                    ProductName = "Robot khủng long biến hình",
                    ImageUrl = "https://api.placeholder.com/300/300/4ECDC4/FFFFFF?text=Robot+Khủng+long",
                    Price = 380000,
                    Rating = 4.0,
                    ReviewCount = 67,
                    Badge = "15%"
                },
                new RelatedProduct
                {
                    Id = 3,
                    ProductName = "Robot siêu anh hùng có đèn LED",
                    ImageUrl = "https://api.placeholder.com/300/300/FFE66D/1A535C?text=Siêu+anh+hùng",
                    Price = 520000,
                    Rating = 5.0,
                    ReviewCount = 89,
                    Badge = "Mới"
                },
                new RelatedProduct
                {
                    Id = 4,
                    ProductName = "Robot điều khiển từ xa thông minh",
                    ImageUrl = "https://api.placeholder.com/300/300/1A535C/FFFFFF?text=Điều+khiển",
                    Price = 650000,
                    Rating = 3.5,
                    ReviewCount = 45,
                    Badge = "Hot"
                },
                new RelatedProduct
                {
                    Id = 5,
                    ProductName = "Robot lập trình giáo dục cho trẻ em",
                    ImageUrl = "https://api.placeholder.com/300/300/FF6B6B/FFFFFF?text=Lập+trình",
                    Price = 750000,
                    Rating = 5.0,
                    ReviewCount = 92,
                    Badge = "10%"
                }
            };
        }

        #endregion

        #region Event Handlers

        protected void btnSearch_Click(object sender, EventArgs e)
        {
                Response.Redirect($"/Client/ProductList.aspx?search={HttpUtility.UrlEncode("")}");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Client/Login.aspx");
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                int productId = Convert.ToInt32(Request.QueryString["id"]);

                // Add to cart logic here
                // AddToCart(productId, quantity);

                // Update cart count
                int currentCartCount = Session["CartCount"] != null ? (int)Session["CartCount"] : 0;
                Session["CartCount"] = currentCartCount + quantity;
                UpdateCartCount();

                // Show success message with animation
                ClientScript.RegisterStartupScript(this.GetType(), "addToCart",
                    "animateAddToCart(); alert('Đã thêm sản phẩm vào giỏ hàng!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}');", true);
            }
        }

        protected void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("/Client/Login.aspx");
                    return;
                }

                int productId = Convert.ToInt32(Request.QueryString["id"]);

                // Add to wishlist logic here
                // AddToWishlist(userId, productId);

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Đã thêm sản phẩm vào danh sách yêu thích!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi thêm vào yêu thích: {ex.Message}');", true);
            }
        }

        protected void btnSubscribeNewsletter_Click(object sender, EventArgs e)
        {
            try
            {
         
                    // Subscribe to newsletter logic here
                    // SubscribeNewsletter(email);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Đăng ký nhận tin thành công!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi đăng ký nhận tin: {ex.Message}');", true);
            }
        }

        protected void btnLikeReview_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int reviewId = Convert.ToInt32(e.CommandArgument);

                // Like review logic here
                // LikeReview(reviewId);

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Đã thích đánh giá!');", true);
                LoadProductReviews(Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi thích đánh giá: {ex.Message}');", true);
            }
        }

        protected void btnLoadMoreReviews_Click(object sender, EventArgs e)
        {
            try
            {
                // Load more reviews logic here
                // LoadMoreReviews();

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Đã tải thêm đánh giá!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Lỗi khi tải thêm đánh giá: {ex.Message}');", true);
            }
        }

        #endregion

        #region Helper Methods

        protected string GenerateStars(double rating)
        {
            StringBuilder stars = new StringBuilder();
            int fullStars = (int)rating;
            bool hasHalfStar = (rating - fullStars) >= 0.5;

            for (int i = 0; i < fullStars; i++)
            {
                stars.Append("<i class=\"fas fa-star\"></i>");
            }

            if (hasHalfStar)
            {
                stars.Append("<i class=\"fas fa-star-half-alt\"></i>");
                fullStars++;
            }

            for (int i = fullStars; i < 5; i++)
            {
                stars.Append("<i class=\"far fa-star\"></i>");
            }

            return stars.ToString();
        }

        protected string GetAvatarColor(int index)
        {
            string[] colors = { "primary", "secondary", "accent" };
            return colors[index % colors.Length];
        }

        protected string GetUserInitial(string name)
        {
            if (string.IsNullOrEmpty(name)) return "U";
            return name.Substring(0, 1).ToUpper();
        }

        protected string GetBadgeColor(string badge)
        {
            if (string.IsNullOrEmpty(badge)) return "gray";

            switch (badge.ToLower())
            {
                case "mới":
                case "new":
                    return "green";
                case "hot":
                    return "blue";
                case "sale":
                    return "red";
                default:
                    if (badge.Contains("%"))
                        return "red";
                    return "gray";
            }
        }

        #endregion

        #region Data Classes

        public class ProductInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public string MainImage { get; set; }
            public decimal Price { get; set; }
            public decimal OriginalPrice { get; set; }
            public double Rating { get; set; }
            public int ReviewCount { get; set; }
            public bool InStock { get; set; }
            public string AgeGroup { get; set; }
            public string Description { get; set; }
        }

        public class ProductImage
        {
            public string ImageUrl { get; set; }
        }

        public class ProductFeature
        {
            public string FeatureName { get; set; }
        }

        public class ProductSpecification
        {
            public string SpecName { get; set; }
            public string SpecValue { get; set; }
        }

        public class ProductReview
        {
            public int Id { get; set; }
            public string CustomerName { get; set; }
            public int Rating { get; set; }
            public string ReviewContent { get; set; }
            public DateTime ReviewDate { get; set; }
            public int LikeCount { get; set; }
        }

        public class RatingSummary
        {
            public int Stars { get; set; }
            public int Count { get; set; }
            public int Percentage { get; set; }
        }

        public class RelatedProduct
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public string ImageUrl { get; set; }
            public decimal Price { get; set; }
            public double Rating { get; set; }
            public int ReviewCount { get; set; }
            public string Badge { get; set; }
        }

        #endregion
    }
}