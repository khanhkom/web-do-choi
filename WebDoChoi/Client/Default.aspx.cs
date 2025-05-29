using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteDoChoi.Client
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            LoadCategories();
            LoadBestSellingProducts();
            LoadFeaturedProducts();
            LoadNewProducts();
            LoadCartItems();
            LoadStatistics();
        }

        private void LoadCategories()
        {
            // Tạo dữ liệu mẫu cho danh mục
            var categories = new List<object>
            {
                new { Id = 1, Name = "Đồ chơi giáo dục", Icon = "fas fa-puzzle-piece", ProductCount = 45 },
                new { Id = 2, Name = "Xe & Điều khiển", Icon = "fas fa-car", ProductCount = 32 },
                new { Id = 3, Name = "Đồ chơi cho bé 0-3 tuổi", Icon = "fas fa-baby", ProductCount = 28 },
                new { Id = 4, Name = "Robot & Nhân vật", Icon = "fas fa-robot", ProductCount = 56 },
                new { Id = 5, Name = "Đồ chơi thông minh", Icon = "fas fa-chess", ProductCount = 19 },
                new { Id = 6, Name = "Đồ chơi sáng tạo", Icon = "fas fa-paint-brush", ProductCount = 37 },
                new { Id = 7, Name = "Đồ chơi vận động", Icon = "fas fa-basketball-ball", ProductCount = 22 },
                new { Id = 8, Name = "Đồ chơi xây dựng", Icon = "fas fa-building", ProductCount = 41 }
            };

            rptCategories.DataSource = categories;
            rptCategories.DataBind();
        }

        private void LoadBestSellingProducts()
        {
            // Tạo dữ liệu mẫu cho sản phẩm bán chạy
            var products = new List<object>
            {
                new {
                    Id = 1,
                    Name = "Robot biến hình thông minh",
                    Price = 450000,
                    OriginalPrice = 550000,
                    DiscountPercent = 18,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Robot",
                    Rating = 4.5,
                    ReviewCount = 124,
                    IsNew = false
                },
                new {
                    Id = 2,
                    Name = "Bộ xếp hình thành phố 520 chi tiết",
                    Price = 690000,
                    OriginalPrice = 850000,
                    DiscountPercent = 19,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Lego",
                    Rating = 5.0,
                    ReviewCount = 89,
                    IsNew = false
                },
                new {
                    Id = 3,
                    Name = "Xe đua điều khiển từ xa địa hình",
                    Price = 520000,
                    OriginalPrice = 0,
                    DiscountPercent = 0,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Car",
                    Rating = 4.0,
                    ReviewCount = 56,
                    IsNew = true
                },
                new {
                    Id = 4,
                    Name = "Bộ ghép hình thông minh phát triển trí tuệ",
                    Price = 290000,
                    OriginalPrice = 350000,
                    DiscountPercent = 17,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Puzzle",
                    Rating = 3.5,
                    ReviewCount = 38,
                    IsNew = false
                }
            };

            rptBestSellingProducts.DataSource = products;
            rptBestSellingProducts.DataBind();
        }

        private void LoadFeaturedProducts()
        {
            // Tạo dữ liệu mẫu cho sản phẩm nổi bật
            var products = new List<object>
            {
                new {
                    Id = 5,
                    Name = "Bộ đồ chơi học toán vui nhộn",
                    Price = 195000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Educational",
                    Rating = 5.0,
                    ReviewCount = 78,
                    IsHot = true
                },
                new {
                    Id = 6,
                    Name = "Đồ chơi nhà bếp mini cao cấp",
                    Price = 380000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=Kitchen",
                    Rating = 4.2,
                    ReviewCount = 45,
                    IsHot = false
                },
                new {
                    Id = 7,
                    Name = "Bộ xếp hình không gian 3D",
                    Price = 420000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=3D",
                    Rating = 4.8,
                    ReviewCount = 67,
                    IsHot = true
                }
            };

            rptFeaturedProducts.DataSource = products;
            rptFeaturedProducts.DataBind();
        }

        private void LoadNewProducts()
        {
            // Tạo dữ liệu mẫu cho sản phẩm mới
            var products = new List<object>
            {
                new {
                    Id = 8,
                    Name = "Đồ chơi nhà bếp mini",
                    Price = 230000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=New1",
                    Rating = 5.0
                },
                new {
                    Id = 9,
                    Name = "Máy bay điều khiển từ xa",
                    Price = 480000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=New2",
                    Rating = 4.0
                },
                new {
                    Id = 10,
                    Name = "Búp bê cảm ứng thông minh",
                    Price = 350000,
                    ImageUrl = "https://api.placeholder.com/300/300?text=New3",
                    Rating = 3.5
                }
            };

            rptNewProducts.DataSource = products;
            rptNewProducts.DataBind();
        }

        private void LoadCartItems()
        {
            // Kiểm tra session giỏ hàng
            var cartItems = Session["CartItems"] as List<object>;

            if (cartItems == null || cartItems.Count == 0)
            {
                pnlCartEmpty.Visible = true;
                pnlCartItems.Visible = false;
                return;
            }

            pnlCartEmpty.Visible = false;
            pnlCartItems.Visible = true;

            rptCartItems.DataSource = cartItems;
            rptCartItems.DataBind();

            // Tính tổng tiền
            decimal total = 0;
            foreach (dynamic item in cartItems)
            {
                total += Convert.ToDecimal(item.Price) * Convert.ToInt32(item.Quantity);
            }
            lblCartTotal.Text = total.ToString("N0");
        }

        private void LoadStatistics()
        {
            // Load thống kê từ database hoặc cache
            lblOnlineUsers.Text = "257";
            lblTotalProducts.Text = "2,548";
            lblTodayOrders.Text = "36";
        }

        // Hàm tạo ngôi sao đánh giá
        protected string GenerateStars(double rating)
        {
            string stars = "";
            int fullStars = (int)Math.Floor(rating);
            bool hasHalfStar = (rating - fullStars) >= 0.5;

            // Thêm sao đầy
            for (int i = 0; i < fullStars; i++)
            {
                stars += "<i class='fas fa-star'></i>";
            }

            // Thêm nửa sao
            if (hasHalfStar)
            {
                stars += "<i class='fas fa-star-half-alt'></i>";
            }

            // Thêm sao rỗng
            int totalStars = fullStars + (hasHalfStar ? 1 : 0);
            for (int i = totalStars; i < 5; i++)
            {
                stars += "<i class='far fa-star'></i>";
            }

            return stars;
        }

        // Event handlers cho các nút
        protected void btnGridView_Click(object sender, EventArgs e)
        {
            // Chuyển sang chế độ hiển thị lưới
            Response.Redirect("~/Client/ProductList.aspx?view=grid");
        }

        protected void btnListView_Click(object sender, EventArgs e)
        {
            // Chuyển sang chế độ hiển thị danh sách
            Response.Redirect("~/Client/ProductList.aspx?view=list");
        }

        protected void btnPrevPage_Click(object sender, EventArgs e)
        {
            // Xử lý phân trang - trang trước
            int currentPage = Convert.ToInt32(ViewState["CurrentPage"] ?? 1);
            if (currentPage > 1)
            {
                ViewState["CurrentPage"] = currentPage - 1;
                LoadFeaturedProducts();
            }
        }

        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            // Xử lý phân trang - trang sau
            int currentPage = Convert.ToInt32(ViewState["CurrentPage"] ?? 1);
            int totalPages = Convert.ToInt32(ViewState["TotalPages"] ?? 4);
            if (currentPage < totalPages)
            {
                ViewState["CurrentPage"] = currentPage + 1;
                LoadFeaturedProducts();
            }
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            // Xử lý phân trang - chọn trang cụ thể
            LinkButton btn = (LinkButton)sender;
            int pageNumber = Convert.ToInt32(btn.CommandArgument);
            ViewState["CurrentPage"] = pageNumber;
            LoadFeaturedProducts();
        }

        protected void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            // Xóa sản phẩm khỏi giỏ hàng
            LinkButton btn = (LinkButton)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);

            var cartItems = Session["CartItems"] as List<object>;
            if (cartItems != null)
            {
                // Logic xóa sản phẩm khỏi giỏ hàng
                // cartItems.RemoveAll(x => x.Id == productId);
                Session["CartItems"] = cartItems;
                LoadCartItems();
            }
        }

        // Event handlers cho Repeater ItemCommand
        protected void rptBestSellingProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "AddToCart")
            {
                AddToCart(productId);
            }
            else if (e.CommandName == "AddToWishlist")
            {
                AddToWishlist(productId);
            }
        }

        protected void rptFeaturedProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "AddToCart")
            {
                AddToCart(productId);
            }
            else if (e.CommandName == "AddToWishlist")
            {
                AddToWishlist(productId);
            }
        }

        private void AddToCart(int productId)
        {
            // Logic thêm sản phẩm vào giỏ hàng
            var cartItems = Session["CartItems"] as List<object> ?? new List<object>();

            // Tìm sản phẩm theo ID và thêm vào giỏ hàng
            // Logic thêm sản phẩm...

            Session["CartItems"] = cartItems;
            LoadCartItems();

            // Hiển thị thông báo thành công
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success",
                "alert('Đã thêm sản phẩm vào giỏ hàng!');", true);
        }

        private void AddToWishlist(int productId)
        {
            // Logic thêm sản phẩm vào danh sách yêu thích
            var wishlistItems = Session["WishlistItems"] as List<int> ?? new List<int>();

            if (!wishlistItems.Contains(productId))
            {
                wishlistItems.Add(productId);
                Session["WishlistItems"] = wishlistItems;

                // Hiển thị thông báo thành công
                ScriptManager.RegisterStartupScript(this, this.GetType(), "wishlist",
                    "alert('Đã thêm sản phẩm vào danh sách yêu thích!');", true);
            }
            else
            {
                // Hiển thị thông báo đã tồn tại
                ScriptManager.RegisterStartupScript(this, this.GetType(), "wishlist_exists",
                    "alert('Sản phẩm đã có trong danh sách yêu thích!');", true);
            }
        }
    }
}