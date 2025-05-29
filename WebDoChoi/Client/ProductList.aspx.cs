using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteDoChoi.Client
{
    public partial class ProductList : System.Web.UI.Page
    {
        private int CurrentPage
        {
            get { return ViewState["CurrentPage"] as int? ?? 1; }
            set { ViewState["CurrentPage"] = value; }
        }

        private int PageSize
        {
            get { return ViewState["PageSize"] as int? ?? 24; }
            set { ViewState["PageSize"] = value; }
        }

        private string SortBy
        {
            get { return ViewState["SortBy"] as string ?? "popular"; }
            set { ViewState["SortBy"] = value; }
        }

        private string ViewMode
        {
            get { return ViewState["ViewMode"] as string ?? "grid"; }
            set { ViewState["ViewMode"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializePage();
                LoadFilters();
                LoadProducts();
            }
        }

        private void InitializePage()
        {
            // Get parameters from URL
            string categoryId = Request.QueryString["categoryId"];
            string filter = Request.QueryString["filter"];
            string search = Request.QueryString["search"];

            // Set page title and breadcrumb based on parameters
            if (!string.IsNullOrEmpty(categoryId))
            {
                SetCategoryPage(categoryId);
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                SetFilterPage(filter);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                SetSearchPage(search);
            }
            else
            {
                SetDefaultPage();
            }

            // Initialize dropdown values
            ddlSortBy.SelectedValue = SortBy;
            ddlPageSize.SelectedValue = PageSize.ToString();
        }

        private void SetCategoryPage(string categoryId)
        {
            // Set category-specific content
            switch (categoryId)
            {
                case "1":
                    lblCategoryTitle.Text = "Đồ chơi giáo dục";
                    lblCategoryDescription.Text = "Phát triển trí tuệ và kỹ năng học tập cho trẻ em";
                    lblBreadcrumb.Text = "Đồ chơi giáo dục";
                    Page.Title = "Đồ chơi giáo dục - ToyLand";
                    break;
                case "2":
                    lblCategoryTitle.Text = "Xe & Điều khiển";
                    lblCategoryDescription.Text = "Các loại xe đồ chơi và xe điều khiển từ xa";
                    lblBreadcrumb.Text = "Xe & Điều khiển";
                    Page.Title = "Xe & Điều khiển - ToyLand";
                    break;
                // Add more cases for other categories
                default:
                    SetDefaultPage();
                    break;
            }
        }

        private void SetFilterPage(string filter)
        {
            switch (filter)
            {
                case "bestseller":
                    lblCategoryTitle.Text = "Sản phẩm bán chạy";
                    lblCategoryDescription.Text = "Những đồ chơi được yêu thích nhất";
                    lblBreadcrumb.Text = "Bán chạy";
                    Page.Title = "Sản phẩm bán chạy - ToyLand";
                    break;
                case "promotion":
                    lblCategoryTitle.Text = "Khuyến mãi";
                    lblCategoryDescription.Text = "Sản phẩm đang có giá ưu đãi";
                    lblBreadcrumb.Text = "Khuyến mãi";
                    Page.Title = "Khuyến mãi - ToyLand";
                    break;
                case "new":
                    lblCategoryTitle.Text = "Sản phẩm mới";
                    lblCategoryDescription.Text = "Những đồ chơi mới nhất";
                    lblBreadcrumb.Text = "Sản phẩm mới";
                    Page.Title = "Sản phẩm mới - ToyLand";
                    break;
                default:
                    SetDefaultPage();
                    break;
            }
        }

        private void SetSearchPage(string search)
        {
            lblCategoryTitle.Text = $"Kết quả tìm kiếm: \"{search}\"";
            lblCategoryDescription.Text = "Các sản phẩm phù hợp với từ khóa tìm kiếm";
            lblBreadcrumb.Text = "Tìm kiếm";
            Page.Title = $"Tìm kiếm: {search} - ToyLand";
        }

        private void SetDefaultPage()
        {
            lblCategoryTitle.Text = "Tất cả sản phẩm";
            lblCategoryDescription.Text = "Khám phá bộ sưu tập đồ chơi đa dạng";
            lblBreadcrumb.Text = "Sản phẩm";
            Page.Title = "Sản phẩm - ToyLand";
        }

        private void LoadFilters()
        {
            LoadCategoryFilters();
            LoadAgeGroupFilters();
            LoadRatingFilters();
            LoadBrandFilters();
        }

        private void LoadCategoryFilters()
        {
            var categories = new List<object>
            {
                new { Value = "1", Text = "Đồ chơi toán học", Count = 12 },
                new { Value = "2", Text = "Đồ chơi khoa học", Count = 8 },
                new { Value = "3", Text = "Đồ chơi ngôn ngữ", Count = 15 },
                new { Value = "4", Text = "Đồ chơi ghép hình", Count = 10 }
            };

            cblCategories.Items.Clear();
            foreach (var category in categories)
            {
                var item = new ListItem($"{category.GetType().GetProperty("Text").GetValue(category)} ({category.GetType().GetProperty("Count").GetValue(category)})",
                                      category.GetType().GetProperty("Value").GetValue(category).ToString());
                cblCategories.Items.Add(item);
            }
        }

        private void LoadAgeGroupFilters()
        {
            var ageGroups = new List<object>
            {
                new { Value = "0-2", Text = "0-2 tuổi", Count = 5 },
                new { Value = "3-5", Text = "3-5 tuổi", Count = 18 },
                new { Value = "6-8", Text = "6-8 tuổi", Count = 12 },
                new { Value = "9+", Text = "9+ tuổi", Count = 10 }
            };

            cblAgeGroups.Items.Clear();
            foreach (var ageGroup in ageGroups)
            {
                var item = new ListItem($"{ageGroup.GetType().GetProperty("Text").GetValue(ageGroup)} ({ageGroup.GetType().GetProperty("Count").GetValue(ageGroup)})",
                                      ageGroup.GetType().GetProperty("Value").GetValue(ageGroup).ToString());
                cblAgeGroups.Items.Add(item);
            }
        }

        private void LoadRatingFilters()
        {
            var ratings = new List<object>
            {
                new { Value = "5", Text = "5 sao", Count = 8 },
                new { Value = "4", Text = "4 sao trở lên", Count = 15 },
                new { Value = "3", Text = "3 sao trở lên", Count = 22 }
            };

            rblRating.Items.Clear();
            foreach (var rating in ratings)
            {
                var item = new ListItem($"{rating.GetType().GetProperty("Text").GetValue(rating)} ({rating.GetType().GetProperty("Count").GetValue(rating)})",
                                      rating.GetType().GetProperty("Value").GetValue(rating).ToString());
                rblRating.Items.Add(item);
            }
        }

        private void LoadBrandFilters()
        {
            var brands = new List<object>
            {
                new { Value = "toyland", Text = "ToyLand", Count = 20 },
                new { Value = "eduplay", Text = "EduPlay", Count = 15 },
                new { Value = "smartkids", Text = "SmartKids", Count = 10 }
            };

            cblBrands.Items.Clear();
            foreach (var brand in brands)
            {
                var item = new ListItem($"{brand.GetType().GetProperty("Text").GetValue(brand)} ({brand.GetType().GetProperty("Count").GetValue(brand)})",
                                      brand.GetType().GetProperty("Value").GetValue(brand).ToString());
                cblBrands.Items.Add(item);
            }
        }

        private void LoadProducts()
        {
            // Get sample products data
            var allProducts = GetSampleProducts();

            // Apply filters
            var filteredProducts = ApplyFilters(allProducts);

            // Apply sorting
            var sortedProducts = ApplySorting(filteredProducts);

            // Apply pagination
            var pagedProducts = ApplyPagination(sortedProducts);

            // Bind to repeater
            rptProducts.DataSource = pagedProducts;
            rptProducts.DataBind();

            // Update UI elements
            UpdateProductCount(filteredProducts.Count());
            LoadPagination(filteredProducts.Count());

            // Show/hide no products message
            pnlNoProducts.Visible = !pagedProducts.Any();
        }

        private IEnumerable<object> GetSampleProducts()
        {
            return new List<object>
            {
                new {
                    Id = 1, Name = "Bộ thí nghiệm khoa học an toàn cho trẻ",
                    Price = 450000, OriginalPrice = 0, DiscountPercent = 0,
                    ImageUrl = "https://api.placeholder.com/300/300/FFE66D/1A535C?text=Toán+học",
                    Rating = 5.0, ReviewCount = 124, AgeGroup = "6+ tuổi",
                    IsNew = true, IsHot = false, Category = "science", Brand = "toyland"
                },
                new {
                    Id = 2, Name = "Bảng chữ cái thông minh có âm thanh",
                    Price = 320000, OriginalPrice = 380000, DiscountPercent = 16,
                    ImageUrl = "https://api.placeholder.com/300/300/FF6B6B/FFFFFF?text=Chữ+cái",
                    Rating = 4.0, ReviewCount = 67, AgeGroup = "2-5 tuổi",
                    IsNew = false, IsHot = false, Category = "language", Brand = "eduplay"
                },
                new {
                    Id = 3, Name = "Bộ ghép hình động vật 3D bằng gỗ",
                    Price = 195000, OriginalPrice = 0, DiscountPercent = 0,
                    ImageUrl = "https://api.placeholder.com/300/300/1A535C/FFFFFF?text=Ghép+hình",
                    Rating = 3.5, ReviewCount = 43, AgeGroup = "3-8 tuổi",
                    IsNew = false, IsHot = true, Category = "puzzle", Brand = "smartkids"
                },
                new {
                    Id = 4, Name = "Máy học từ vựng tiếng Anh cho trẻ",
                    Price = 280000, OriginalPrice = 350000, DiscountPercent = 20,
                    ImageUrl = "https://api.placeholder.com/300/300/FFE66D/1A535C?text=Từ+vựng",
                    Rating = 5.0, ReviewCount = 156, AgeGroup = "4-10 tuổi",
                    IsNew = false, IsHot = false, Category = "language", Brand = "toyland"
                },
                new {
                    Id = 5, Name = "Bộ đồ chơi phát triển tư duy logic",
                    Price = 365000, OriginalPrice = 0, DiscountPercent = 0,
                    ImageUrl = "https://api.placeholder.com/300/300/4ECDC4/FFFFFF?text=Logic",
                    Rating = 4.0, ReviewCount = 92, AgeGroup = "5+ tuổi",
                    IsNew = false, IsHot = false, Category = "logic", Brand = "eduplay"
                },
                new {
                    Id = 6, Name = "Bảng từ tính học chữ cái và số",
                    Price = 170000, OriginalPrice = 200000, DiscountPercent = 15,
                    ImageUrl = "https://api.placeholder.com/300/300/FF6B6B/FFFFFF?text=Từ+tính",
                    Rating = 3.5, ReviewCount = 78, AgeGroup = "3-7 tuổi",
                    IsNew = false, IsHot = false, Category = "math", Brand = "smartkids"
                },
                new {
                    Id = 7, Name = "Bộ khối gỗ màu sắc hình học",
                    Price = 225000, OriginalPrice = 0, DiscountPercent = 0,
                    ImageUrl = "https://api.placeholder.com/300/300/1A535C/FFFFFF?text=Khối+gỗ",
                    Rating = 5.0, ReviewCount = 201, AgeGroup = "1-5 tuổi",
                    IsNew = false, IsHot = false, Category = "puzzle", Brand = "toyland"
                }
            };
        }

        private IEnumerable<object> ApplyFilters(IEnumerable<object> products)
        {
            var filteredProducts = products;

            // Category filter
            var selectedCategories = cblCategories.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToList();

            if (selectedCategories.Any())
            {
                // Apply category filter logic
                // filteredProducts = filteredProducts.Where(p => selectedCategories.Contains(p.Category));
            }

            // Age group filter
            var selectedAgeGroups = cblAgeGroups.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToList();

            // Brand filter
            var selectedBrands = cblBrands.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToList();

            // Rating filter
            var selectedRating = rblRating.SelectedValue;

            // Price range filter
            if (!string.IsNullOrEmpty(txtPriceFrom.Text) && !string.IsNullOrEmpty(txtPriceTo.Text))
            {
                if (decimal.TryParse(txtPriceFrom.Text, out decimal priceFrom) &&
                    decimal.TryParse(txtPriceTo.Text, out decimal priceTo))
                {
                    // Apply price range filter
                    // filteredProducts = filteredProducts.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
                }
            }

            return filteredProducts;
        }

        private IEnumerable<object> ApplySorting(IEnumerable<object> products)
        {
            switch (SortBy)
            {
                case "newest":
                    // Sort by newest (assuming we have a CreatedDate property)
                    return products.OrderByDescending(p => p.GetType().GetProperty("Id").GetValue(p));
                case "price-low":
                    return products.OrderBy(p => p.GetType().GetProperty("Price").GetValue(p));
                case "price-high":
                    return products.OrderByDescending(p => p.GetType().GetProperty("Price").GetValue(p));
                case "rating":
                    return products.OrderByDescending(p => p.GetType().GetProperty("Rating").GetValue(p));
                case "popular":
                default:
                    return products.OrderByDescending(p => p.GetType().GetProperty("ReviewCount").GetValue(p));
            }
        }

        private IEnumerable<object> ApplyPagination(IEnumerable<object> products)
        {
            int skip = (CurrentPage - 1) * PageSize;
            return products.Skip(skip).Take(PageSize);
        }

        private void UpdateProductCount(int totalCount)
        {
            int startIndex = (CurrentPage - 1) * PageSize + 1;
            int endIndex = Math.Min(CurrentPage * PageSize, totalCount);

            lblCurrentRange.Text = $"{startIndex}-{endIndex}";
            lblTotalProducts.Text = totalCount.ToString();
        }

        private void LoadPagination(int totalCount)
        {
            int totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            var paginationData = new List<object>();

            for (int i = 1; i <= totalPages; i++)
            {
                paginationData.Add(new
                {
                    PageNumber = i,
                    IsCurrentPage = i == CurrentPage
                });
            }

            rptPagination.DataSource = paginationData;
            rptPagination.DataBind();

            // Enable/disable navigation buttons
            btnPrevPage.Enabled = CurrentPage > 1;
            btnNextPage.Enabled = CurrentPage < totalPages;
        }

        // Helper method to generate star ratings
        protected string GenerateStars(double rating)
        {
            string stars = "";
            int fullStars = (int)Math.Floor(rating);
            bool hasHalfStar = (rating - fullStars) >= 0.5;

            // Add full stars
            for (int i = 0; i < fullStars; i++)
            {
                stars += "<i class='fas fa-star'></i>";
            }

            // Add half star
            if (hasHalfStar)
            {
                stars += "<i class='fas fa-star-half-alt'></i>";
            }

            // Add empty stars
            int totalStars = fullStars + (hasHalfStar ? 1 : 0);
            for (int i = totalStars; i < 5; i++)
            {
                stars += "<i class='far fa-star'></i>";
            }

            return stars;
        }

        // Event Handlers
        protected void btnApplyFilters_Click(object sender, EventArgs e)
        {
            CurrentPage = 1; // Reset to first page when applying filters
            LoadProducts();
        }

        protected void btnClearFilters_Click(object sender, EventArgs e)
        {
            // Clear all filter selections
            foreach (ListItem item in cblCategories.Items)
                item.Selected = false;
            foreach (ListItem item in cblAgeGroups.Items)
                item.Selected = false;
            foreach (ListItem item in cblBrands.Items)
                item.Selected = false;

            rblRating.ClearSelection();
            txtPriceFrom.Text = "";
            txtPriceTo.Text = "";

            CurrentPage = 1;
            LoadProducts();
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortBy = ddlSortBy.SelectedValue;
            CurrentPage = 1;
            LoadProducts();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            CurrentPage = 1;
            LoadProducts();
        }

        protected void btnGridView_Click(object sender, EventArgs e)
        {
            ViewMode = "grid";
            btnGridView.CssClass = "p-2 border rounded hover:bg-gray-50 transition-colors bg-primary text-white";
            btnListView.CssClass = "p-2 border rounded hover:bg-gray-50 transition-colors";
        }

        protected void btnListView_Click(object sender, EventArgs e)
        {
            ViewMode = "list";
            btnListView.CssClass = "p-2 border rounded hover:bg-gray-50 transition-colors bg-primary text-white";
            btnGridView.CssClass = "p-2 border rounded hover:bg-gray-50 transition-colors";
        }

        protected void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadProducts();
            }
        }

        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            LoadProducts();
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            CurrentPage = Convert.ToInt32(btn.CommandArgument);
            LoadProducts();
        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
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
            // Get cart from session
            var cartItems = Session["CartItems"] as List<object> ?? new List<object>();

            // Find product and add to cart
            var products = GetSampleProducts();
            var product = products.FirstOrDefault(p => Convert.ToInt32(p.GetType().GetProperty("Id").GetValue(p)) == productId);

            if (product != null)
            {
                // Add to cart logic here
                // cartItems.Add(new CartItem { ProductId = productId, Quantity = 1, ... });
                Session["CartItems"] = cartItems;

                // Show success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success",
                    "alert('Đã thêm sản phẩm vào giỏ hàng!');", true);
            }
        }

        private void AddToWishlist(int productId)
        {
            // Get wishlist from session
            var wishlistItems = Session["WishlistItems"] as List<int> ?? new List<int>();

            if (!wishlistItems.Contains(productId))
            {
                wishlistItems.Add(productId);
                Session["WishlistItems"] = wishlistItems;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "wishlist",
                    "alert('Đã thêm sản phẩm vào danh sách yêu thích!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "wishlist_exists",
                    "alert('Sản phẩm đã có trong danh sách yêu thích!');", true);
            }
        }

        // Filter event handlers
        protected void cblCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadProducts();
        }

        protected void cblAgeGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadProducts();
        }

        protected void cblBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadProducts();
        }

        protected void rblRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadProducts();
        }
    }
}