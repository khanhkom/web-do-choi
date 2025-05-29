using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteDoChoi.Client
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                //if (User.Identity.IsAuthenticated)
                //{
                //    Response.Redirect("~/Client/Default.aspx");
                //}

                // Check for return URL
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Session["ReturnUrl"] = returnUrl;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = txtLoginEmail.Text.Trim();
                string password = txtLoginPassword.Text;
                bool rememberMe = chkRememberMe.Checked;

                try
                {
                    // Authenticate user
                    var user = AuthenticateUser(email, password);

                    if (user != null)
                    {
                        // Create authentication ticket
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1,                              // version
                            user.Email,                     // name
                            DateTime.Now,                   // issue time
                            DateTime.Now.AddMinutes(rememberMe ? 10080 : 30), // expiration (7 days or 30 min)
                            rememberMe,                     // persistent
                            user.Role,                      // user data (role)
                            FormsAuthentication.FormsCookiePath
                        );

                        // Encrypt ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        // Create cookie
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        if (rememberMe)
                        {
                            authCookie.Expires = DateTime.Now.AddDays(7);
                        }
                        Response.Cookies.Add(authCookie);

                        // Store user info in session
                        Session["UserId"] = user.Id;
                        Session["UserName"] = $"{user.FirstName} {user.LastName}";
                        Session["UserEmail"] = user.Email;
                        Session["UserRole"] = user.Role;

                        // Update last login
                        UpdateLastLogin(user.Id);

                        // Show success message and redirect
                        string script = "showModal('Đăng nhập thành công! Chào mừng bạn trở lại ToyLand.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "loginSuccess", script, true);
                    }
                    else
                    {
                        ShowLoginError("Email hoặc mật khẩu không đúng!");
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                    ShowLoginError("Có lỗi xảy ra trong quá trình đăng nhập. Vui lòng thử lại!");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string email = txtRegisterEmail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string password = txtRegisterPassword.Text;
                bool newsletter = chkNewsletter.Checked;

                try
                {
                    // Check if email already exists
                    if (IsEmailExists(email))
                    {
                        ShowRegisterError("Email này đã được sử dụng. Vui lòng chọn email khác!");
                        return;
                    }

                    // Create new user
                    var newUser = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Phone = phone,
                        Password = HashPassword(password),
                        Role = "Customer",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        NewsletterSubscribed = newsletter
                    };

                    int userId = CreateUser(newUser);

                    if (userId > 0)
                    {
                        // Auto login after registration
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1,
                            email,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            false,
                            "Customer",
                            FormsAuthentication.FormsCookiePath
                        );

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(authCookie);

                        // Store user info in session
                        Session["UserId"] = userId;
                        Session["UserName"] = $"{firstName} {lastName}";
                        Session["UserEmail"] = email;
                        Session["UserRole"] = "Customer";

                        // Show success message
                        string script = "showModal('Đăng ký thành công! Chào mừng bạn đến với ToyLand.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "registerSuccess", script, true);
                    }
                    else
                    {
                        ShowRegisterError("Có lỗi xảy ra trong quá trình đăng ký. Vui lòng thử lại!");
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    System.Diagnostics.Debug.WriteLine($"Register error: {ex.Message}");
                    ShowRegisterError("Có lỗi xảy ra trong quá trình đăng ký. Vui lòng thử lại!");
                }
            }
        }

        protected void btnGoogleLogin_Click(object sender, EventArgs e)
        {
            // Implement Google OAuth login
            // For now, just show a message
            string script = "alert('Tính năng đăng nhập Google sẽ được cập nhật sớm!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "googleLogin", script, true);
        }

        protected void btnFacebookLogin_Click(object sender, EventArgs e)
        {
            // Implement Facebook OAuth login
            // For now, just show a message
            string script = "alert('Tính năng đăng nhập Facebook sẽ được cập nhật sớm!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "facebookLogin", script, true);
        }

        protected void cvAgreeTerms_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = chkAgreeTerms.Checked;
        }

        // Helper Methods
        private User AuthenticateUser(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            // For demo purposes, using hardcoded users
            // In production, query from database
            var demoUsers = new List<User>
            {
                new User { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@toyland.vn", Password = HashPassword("admin123"), Role = "Admin", IsActive = true },
                new User { Id = 2, FirstName = "Khách", LastName = "Hàng", Email = "customer@toyland.vn", Password = HashPassword("customer123"), Role = "Customer", IsActive = true }
            };

            return demoUsers.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == hashedPassword && u.IsActive);

            /*
            // Production code with database
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT Id, FirstName, LastName, Email, Password, Role, IsActive 
                               FROM Users 
                               WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        IsActive = (bool)reader["IsActive"]
                    };
                }
            }
            return null;
            */
        }

        private bool IsEmailExists(string email)
        {
            // For demo purposes, check against hardcoded list
            var existingEmails = new List<string> { "admin@toyland.vn", "customer@toyland.vn" };
            return existingEmails.Contains(email.ToLower());

            /*
            // Production code with database
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            */
        }

        private int CreateUser(User user)
        {
            // For demo purposes, return a mock ID
            return new Random().Next(1000, 9999);

            /*
            // Production code with database
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Users (FirstName, LastName, Email, Phone, Password, Role, IsActive, CreatedDate, NewsletterSubscribed) 
                               VALUES (@FirstName, @LastName, @Email, @Phone, @Password, @Role, @IsActive, @CreatedDate, @NewsletterSubscribed);
                               SELECT SCOPE_IDENTITY();";
                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                cmd.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@NewsletterSubscribed", user.NewsletterSubscribed);
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            */
        }

        private void UpdateLastLogin(int userId)
        {
            /*
            // Production code with database
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET LastLoginDate = @LastLoginDate WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LastLoginDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id", userId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            */
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + "ToyLandSalt2025"));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void ShowLoginError(string message)
        {
            lblLoginError.Text = message;
            pnlLoginError.Visible = true;
            pnlRegisterError.Visible = false;
        }

        private void ShowRegisterError(string message)
        {
            lblRegisterError.Text = message;
            pnlRegisterError.Visible = true;
            pnlLoginError.Visible = false;
        }

        // User class
        public class User
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public bool NewsletterSubscribed { get; set; }
        }
    }
}