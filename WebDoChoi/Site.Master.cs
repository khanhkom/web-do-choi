using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebsiteDoChoi
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Khởi tạo dữ liệu cho Master Page
                InitializeMasterPage();
            }
        }

        private void InitializeMasterPage()
        {
            // Set default search placeholder
            if (txtSearch != null)
            {
                txtSearch.Attributes.Add("placeholder", "Tìm kiếm đồ chơi...");
            }

            // Load cart count from session
            UpdateCartCount();

            // Set current page active navigation
            SetActiveNavigation();
        }

        private void UpdateCartCount()
        {
            // Cập nhật số lượng sản phẩm trong giỏ hàng
            var cartItems = Session["CartItems"] as List<object>;
            int cartCount = cartItems?.Count ?? 0;

            // Tìm và cập nhật cart count badge
            // Có thể sử dụng JavaScript để cập nhật
            if (cartCount > 0)
            {
                string script = $"document.querySelectorAll('.cart-count').forEach(el => el.textContent = '{cartCount}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "updateCartCount", script, true);
            }
        }

        private void SetActiveNavigation()
        {
            // Lấy tên trang hiện tại
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            // Set active class cho navigation tương ứng
            string script = @"
                document.addEventListener('DOMContentLoaded', function() {
                    const currentPage = '" + currentPage + @"';
                    const navItems = document.querySelectorAll('.nav-item');
                    
                    navItems.forEach(item => {
                        const href = item.getAttribute('href');
                        if (href && href.includes(currentPage)) {
                            item.classList.add('active');
                            item.classList.remove('text-gray-600');
                        }
                    });
                });";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "setActiveNav", script, true);
        }

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            // Xử lý đăng ký newsletter
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newsletter_error",
                    "alert('Vui lòng nhập email!');", true);
                return;
            }

            if (!IsValidEmail(email))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newsletter_invalid",
                    "alert('Email không hợp lệ!');", true);
                return;
            }

            try
            {
                // Logic lưu email vào database
                SaveNewsletterSubscription(email);

                // Clear textbox
                txtEmail.Text = "";

                // Hiển thị thông báo thành công
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newsletter_success",
                    "alert('Đăng ký thành công! Cảm ơn bạn đã quan tâm đến ToyLand.');", true);
            }
            catch (Exception ex)
            {
                // Log error và hiển thị thông báo lỗi
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newsletter_error",
                    "alert('Có lỗi xảy ra. Vui lòng thử lại sau!');", true);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void SaveNewsletterSubscription(string email)
        {
            // Logic lưu email vào database
            // Ví dụ:
            /*
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO NewsletterSubscriptions (Email, SubscriptionDate) VALUES (@Email, @Date)", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.ExecuteNonQuery();
            }
            */

            // Tạm thời lưu vào Session hoặc Application state
            var subscribers = Application["NewsletterSubscribers"] as List<string> ?? new List<string>();
            if (!subscribers.Contains(email))
            {
                subscribers.Add(email);
                Application["NewsletterSubscribers"] = subscribers;
            }
        }

        // Method để các trang con có thể gọi để cập nhật cart count
        public void RefreshCartCount()
        {
            UpdateCartCount();
        }

        // Method để set page title từ các trang con
        public void SetPageTitle(string title)
        {
            Page.Title = title + " - ToyLand";
        }

        // Method để thêm meta tags từ các trang con
        public void AddMetaTag(string name, string content)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = name;
            meta.Content = content;
            Page.Header.Controls.Add(meta);
        }

        // Method để thêm custom CSS từ các trang con
        public void AddCustomCSS(string cssPath)
        {
            HtmlLink link = new HtmlLink();
            link.Href = ResolveUrl(cssPath);
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(link);
        }

        // Method để thêm custom JavaScript từ các trang con
        public void AddCustomJS(string jsPath)
        {
            string script = $"<script src='{ResolveUrl(jsPath)}' type='text/javascript'></script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "customJS_" + Guid.NewGuid().ToString(), script, false);
        }
    }
}