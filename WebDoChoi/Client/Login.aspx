<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebsiteDoChoi.Client.Login" %>

<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ToyLand - Đăng nhập & Đăng ký</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <script>
        tailwind.config = {
            theme: {
                extend: {
                    colors: {
                        primary: '#FF6B6B',
                        secondary: '#4ECDC4',
                        accent: '#FFE66D',
                        dark: '#1A535C',
                        light: '#F7FFF7',
                    }
                }
            }
        }
    </script>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Baloo+2:wght@400;500;600;700&display=swap');
        
        body {
            font-family: 'Baloo 2', cursive;
        }
        
        .auth-container {
            background: linear-gradient(135deg, #FF6B6B 0%, #4ECDC4 100%);
            min-height: 100vh;
        }
        
        .floating-toys {
            position: absolute;
            font-size: 2rem;
            color: rgba(255, 255, 255, 0.1);
            animation: float 6s ease-in-out infinite;
        }
        
        @keyframes float {
            0%, 100% { transform: translateY(0px); }
            50% { transform: translateY(-20px); }
        }
        
        .toy-1 { top: 10%; left: 10%; animation-delay: 0s; }
        .toy-2 { top: 20%; right: 15%; animation-delay: 1s; }
        .toy-3 { bottom: 30%; left: 20%; animation-delay: 2s; }
        .toy-4 { bottom: 15%; right: 10%; animation-delay: 3s; }
        .toy-5 { top: 50%; left: 5%; animation-delay: 4s; }
        .toy-6 { top: 40%; right: 5%; animation-delay: 5s; }

        .form-tab {
            display: none;
        }
        
        .form-tab.active {
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <!-- Header -->
        <header class="bg-white shadow-md relative z-10">
            <div class="container mx-auto px-4 py-3">
                <div class="flex justify-between items-center">
                    <asp:HyperLink ID="lnkHome" runat="server" 
                        NavigateUrl="~/Client/Default.aspx" 
                        CssClass="flex items-center">
                        <div class="text-3xl font-bold text-primary">
                            <span class="text-dark">Toy</span>Land
                            <i class="fas fa-rocket ml-1 text-secondary"></i>
                        </div>
                    </asp:HyperLink>
                </div>
            </div>
        </header>

        <!-- Main Auth Container -->
        <div class="auth-container relative overflow-hidden">
            <!-- Floating Toy Icons -->
            <div class="floating-toys toy-1"><i class="fas fa-rocket"></i></div>
            <div class="floating-toys toy-2"><i class="fas fa-robot"></i></div>
            <div class="floating-toys toy-3"><i class="fas fa-car"></i></div>
            <div class="floating-toys toy-4"><i class="fas fa-puzzle-piece"></i></div>
            <div class="floating-toys toy-5"><i class="fas fa-baby"></i></div>
            <div class="floating-toys toy-6"><i class="fas fa-basketball-ball"></i></div>

            <div class="flex items-center justify-center min-h-screen py-12 px-4">
                <div class="bg-white rounded-2xl shadow-2xl overflow-hidden max-w-4xl w-full">
                    <div class="flex flex-col md:flex-row">
                        <!-- Left Side - Welcome Panel -->
                        <div class="md:w-1/2 bg-gradient-to-br from-primary to-secondary p-8 text-white relative overflow-hidden">
                            <div class="relative z-10">
                                <div class="text-center mb-8">
                                    <div class="text-4xl font-bold mb-2">
                                        <span class="text-white">Toy</span>Land
                                        <i class="fas fa-rocket ml-2 text-accent"></i>
                                    </div>
                                    <p class="text-white/90">Thế giới đồ chơi tuyệt vời</p>
                                </div>
                                
                                <div class="text-center">
                                    <h2 id="welcomeTitle" class="text-2xl font-bold mb-4">Chào mừng trở lại!</h2>
                                    <p id="welcomeText" class="mb-6 text-white/90">Đăng nhập để khám phá thế giới đồ chơi tuyệt vời và nhận những ưu đãi đặc biệt dành riêng cho bạn.</p>
                                    <button id="switchBtn" type="button" onclick="switchForm()" class="border-2 border-white text-white px-8 py-3 rounded-full hover:bg-white hover:text-primary transition-all duration-300 font-medium">
                                        Đăng ký ngay
                                    </button>
                                </div>

                                <!-- Features -->
                                <div class="mt-8 space-y-3">
                                    <div class="flex items-center">
                                        <i class="fas fa-gift w-6 text-accent"></i>
                                        <span class="text-sm">Ưu đãi độc quyền cho thành viên</span>
                                    </div>
                                    <div class="flex items-center">
                                        <i class="fas fa-truck w-6 text-accent"></i>
                                        <span class="text-sm">Miễn phí vận chuyển đơn từ 500K</span>
                                    </div>
                                    <div class="flex items-center">
                                        <i class="fas fa-star w-6 text-accent"></i>
                                        <span class="text-sm">Tích điểm đổi quà hấp dẫn</span>
                                    </div>
                                </div>
                            </div>
                            
                            <!-- Background decoration -->
                            <div class="absolute top-0 right-0 w-32 h-32 bg-white/10 rounded-full -translate-y-16 translate-x-16"></div>
                            <div class="absolute bottom-0 left-0 w-24 h-24 bg-white/10 rounded-full translate-y-12 -translate-x-12"></div>
                        </div>

                        <!-- Right Side - Forms -->
                        <div class="md:w-1/2 p-8">
                            <!-- Login Form -->
                            <asp:UpdatePanel ID="upLogin" runat="server">
                                <ContentTemplate>
                                    <div id="loginTab" class="form-tab active">
                                        <div class="text-center mb-8">
                                            <h2 class="text-3xl font-bold text-dark mb-2">Đăng nhập</h2>
                                            <p class="text-gray-600">Chào mừng bạn quay trở lại ToyLand</p>
                                        </div>

                                        <!-- Error Message -->
                                        <asp:Panel ID="pnlLoginError" runat="server" 
                                            Visible="false" 
                                            CssClass="mb-4 p-4 bg-red-100 border border-red-400 text-red-700 rounded-lg">
                                            <asp:Label ID="lblLoginError" runat="server"></asp:Label>
                                        </asp:Panel>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Email của bạn</label>
                                            <asp:TextBox ID="txtLoginEmail" runat="server" 
                                                TextMode="Email"
                                                CssClass="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                placeholder="Nhập email của bạn" 
                                                Required="true" />
                                            <asp:RequiredFieldValidator ID="rfvLoginEmail" runat="server" 
                                                ControlToValidate="txtLoginEmail"
                                                ErrorMessage="Vui lòng nhập email"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="LoginGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Mật khẩu</label>
                                            <div class="relative">
                                                <asp:TextBox ID="txtLoginPassword" runat="server" 
                                                    TextMode="Password"
                                                    CssClass="w-full px-4 py-3 pr-12 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                    placeholder="Nhập mật khẩu" 
                                                    Required="true" />
                                                <button type="button" onclick="togglePassword('<%= txtLoginPassword.ClientID %>', this)" 
                                                        class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-primary">
                                                    <i class="fas fa-eye"></i>
                                                </button>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvLoginPassword" runat="server" 
                                                ControlToValidate="txtLoginPassword"
                                                ErrorMessage="Vui lòng nhập mật khẩu"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="LoginGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="flex items-center justify-between mb-6">
                                            <label class="flex items-center">
                                                <asp:CheckBox ID="chkRememberMe" runat="server" 
                                                    CssClass="w-4 h-4 text-primary border-gray-300 rounded focus:ring-primary" />
                                                <span class="ml-2 text-sm text-gray-600">Ghi nhớ đăng nhập</span>
                                            </label>
                                            <asp:HyperLink ID="lnkForgotPassword" runat="server" 
                                                NavigateUrl="#" 
                                                CssClass="text-sm text-primary hover:underline">Quên mật khẩu?</asp:HyperLink>
                                        </div>

                                        <asp:Button ID="btnLogin" runat="server" 
                                            Text="Đăng nhập" 
                                            CssClass="w-full bg-primary text-white py-3 rounded-lg hover:bg-primary/90 transition-colors font-medium mb-6"
                                            OnClick="btnLogin_Click" 
                                            ValidationGroup="LoginGroup" />

                                        <div class="relative mb-6">
                                            <div class="absolute inset-0 flex items-center">
                                                <div class="w-full border-t border-gray-300"></div>
                                            </div>
                                            <div class="relative flex justify-center text-sm">
                                                <span class="px-2 bg-white text-gray-500">Hoặc đăng nhập với</span>
                                            </div>
                                        </div>

                                        <div class="grid grid-cols-2 gap-3">
                                            <asp:Button ID="btnGoogleLogin" runat="server" 
                                                Text="Google" 
                                                CssClass="flex items-center justify-center px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
                                                OnClick="btnGoogleLogin_Click"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnFacebookLogin" runat="server" 
                                                Text="Facebook" 
                                                CssClass="flex items-center justify-center px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
                                                OnClick="btnFacebookLogin_Click"
                                                CausesValidation="false" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <!-- Register Form -->
                            <asp:UpdatePanel ID="upRegister" runat="server">
                                <ContentTemplate>
                                    <div id="registerTab" class="form-tab">
                                        <div class="text-center mb-8">
                                            <h2 class="text-3xl font-bold text-dark mb-2">Đăng ký</h2>
                                            <p class="text-gray-600">Tạo tài khoản để bắt đầu mua sắm</p>
                                        </div>

                                        <!-- Error Message -->
                                        <asp:Panel ID="pnlRegisterError" runat="server" 
                                            Visible="false" 
                                            CssClass="mb-4 p-4 bg-red-100 border border-red-400 text-red-700 rounded-lg">
                                            <asp:Label ID="lblRegisterError" runat="server"></asp:Label>
                                        </asp:Panel>

                                        <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
                                            <div>
                                                <label class="block text-gray-700 font-medium mb-2">Họ</label>
                                                <asp:TextBox ID="txtFirstName" runat="server"
                                                    CssClass="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                    placeholder="Nhập họ" 
                                                    Required="true" />
                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                                    ControlToValidate="txtFirstName"
                                                    ErrorMessage="Vui lòng nhập họ"
                                                    CssClass="text-red-500 text-sm"
                                                    ValidationGroup="RegisterGroup"
                                                    Display="Dynamic" />
                                            </div>
                                            <div>
                                                <label class="block text-gray-700 font-medium mb-2">Tên</label>
                                                <asp:TextBox ID="txtLastName" runat="server"
                                                    CssClass="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                    placeholder="Nhập tên" 
                                                    Required="true" />
                                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" 
                                                    ControlToValidate="txtLastName"
                                                    ErrorMessage="Vui lòng nhập tên"
                                                    CssClass="text-red-500 text-sm"
                                                    ValidationGroup="RegisterGroup"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Email</label>
                                            <asp:TextBox ID="txtRegisterEmail" runat="server"
                                                TextMode="Email"
                                                CssClass="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                placeholder="Nhập email" 
                                                Required="true" />
                                            <asp:RequiredFieldValidator ID="rfvRegisterEmail" runat="server" 
                                                ControlToValidate="txtRegisterEmail"
                                                ErrorMessage="Vui lòng nhập email"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                                ControlToValidate="txtRegisterEmail"
                                                ErrorMessage="Email không hợp lệ"
                                                ValidationExpression="^[^\s@]+@[^\s@]+\.[^\s@]+$"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Số điện thoại</label>
                                            <asp:TextBox ID="txtPhone" runat="server"
                                                CssClass="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                placeholder="Nhập số điện thoại" 
                                                Required="true" />
                                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" 
                                                ControlToValidate="txtPhone"
                                                ErrorMessage="Vui lòng nhập số điện thoại"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Mật khẩu</label>
                                            <div class="relative">
                                                <asp:TextBox ID="txtRegisterPassword" runat="server"
                                                    TextMode="Password"
                                                    CssClass="w-full px-4 py-3 pr-12 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                    placeholder="Nhập mật khẩu" 
                                                    Required="true" />
                                                <button type="button" onclick="togglePassword('<%= txtRegisterPassword.ClientID %>', this)"
                                                        class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-primary">
                                                    <i class="fas fa-eye"></i>
                                                </button>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvRegisterPassword" runat="server" 
                                                ControlToValidate="txtRegisterPassword"
                                                ErrorMessage="Vui lòng nhập mật khẩu"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-6">
                                            <label class="block text-gray-700 font-medium mb-2">Xác nhận mật khẩu</label>
                                            <div class="relative">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server"
                                                    TextMode="Password"
                                                    CssClass="w-full px-4 py-3 pr-12 border border-gray-300 rounded-lg focus:outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 transition-all" 
                                                    placeholder="Nhập lại mật khẩu" 
                                                    Required="true" />
                                                <button type="button" onclick="togglePassword('<%= txtConfirmPassword.ClientID %>', this)"
                                                        class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-primary">
                                                    <i class="fas fa-eye"></i>
                                                </button>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
                                                ControlToValidate="txtConfirmPassword"
                                                ErrorMessage="Vui lòng xác nhận mật khẩu"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                            <asp:CompareValidator ID="cvPassword" runat="server"
                                                ControlToValidate="txtConfirmPassword"
                                                ControlToCompare="txtRegisterPassword"
                                                ErrorMessage="Mật khẩu xác nhận không khớp"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-4">
                                            <label class="flex items-start">
                                                <asp:CheckBox ID="chkAgreeTerms" runat="server" 
                                                    CssClass="w-4 h-4 text-primary border-gray-300 rounded focus:ring-primary mt-1" />
                                                <span class="ml-2 text-sm text-gray-600">
                                                    Tôi đồng ý với 
                                                    <a href="#" class="text-primary hover:underline">Điều khoản dịch vụ</a> 
                                                    và 
                                                    <a href="#" class="text-primary hover:underline">Chính sách bảo mật</a>
                                                </span>
                                            </label>
                                            <asp:CustomValidator ID="cvAgreeTerms" runat="server"
                                                ErrorMessage="Vui lòng đồng ý với điều khoản dịch vụ"
                                                CssClass="text-red-500 text-sm"
                                                ValidationGroup="RegisterGroup"
                                                ClientValidationFunction="validateAgreeTerms"
                                                OnServerValidate="cvAgreeTerms_ServerValidate"
                                                Display="Dynamic" />
                                        </div>

                                        <div class="mb-6">
                                            <label class="flex items-start">
                                                <asp:CheckBox ID="chkNewsletter" runat="server" 
                                                    CssClass="w-4 h-4 text-primary border-gray-300 rounded focus:ring-primary mt-1" />
                                                <span class="ml-2 text-sm text-gray-600">
                                                    Đăng ký nhận thông tin khuyến mãi và sản phẩm mới
                                                </span>
                                            </label>
                                        </div>

                                        <asp:Button ID="btnRegister" runat="server" 
                                            Text="Tạo tài khoản" 
                                            CssClass="w-full bg-primary text-white py-3 rounded-lg hover:bg-primary/90 transition-colors font-medium mb-6"
                                            OnClick="btnRegister_Click" 
                                            ValidationGroup="RegisterGroup" />

                                        <div class="relative mb-6">
                                            <div class="absolute inset-0 flex items-center">
                                                <div class="w-full border-t border-gray-300"></div>
                                            </div>
                                            <div class="relative flex justify-center text-sm">
                                                <span class="px-2 bg-white text-gray-500">Hoặc đăng ký với</span>
                                            </div>
                                        </div>

                                        <div class="grid grid-cols-2 gap-3">
                                            <asp:Button ID="btnGoogleRegister" runat="server" 
                                                Text="Google" 
                                                CssClass="flex items-center justify-center px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
                                                OnClick="btnGoogleLogin_Click"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnFacebookRegister" runat="server" 
                                                Text="Facebook" 
                                                CssClass="flex items-center justify-center px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
                                                OnClick="btnFacebookLogin_Click"
                                                CausesValidation="false" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Success Modal -->
        <div id="successModal" class="fixed inset-0 z-50 flex items-center justify-center hidden">
            <div class="absolute inset-0 bg-black bg-opacity-50"></div>
            <div class="bg-white rounded-lg shadow-xl z-10 mx-4 max-w-md w-full p-6 text-center">
                <div class="w-16 h-16 bg-green-100 rounded-full flex items-center justify-center mx-auto mb-4">
                    <i class="fas fa-check text-2xl text-green-600"></i>
                </div>
                <h3 class="text-xl font-bold text-dark mb-2">Chúc mừng!</h3>
                <p id="successMessage" class="text-gray-600 mb-6"></p>
                <button type="button" onclick="closeModal()" class="bg-primary text-white px-6 py-2 rounded-lg hover:bg-primary/90 transition-colors">
                    Đồng ý
                </button>
            </div>
        </div>

        <!-- Footer -->
        <footer class="bg-gray-100 py-8">
            <div class="container mx-auto px-4">
                <div class="flex flex-col md:flex-row justify-between items-center">
                    <div class="mb-4 md:mb-0">
                        <div class="text-2xl font-bold text-primary mb-2">
                            <span class="text-dark">Toy</span>Land
                            <i class="fas fa-rocket ml-1 text-secondary"></i>
                        </div>
                        <p class="text-gray-600">© 2025 ToyLand. Tất cả quyền được bảo lưu.</p>
                    </div>
                    <div class="flex space-x-4">
                        <a href="#" class="text-gray-600 hover:text-primary transition-colors">
                            <i class="fab fa-facebook-f"></i>
                        </a>
                        <a href="#" class="text-gray-600 hover:text-primary transition-colors">
                            <i class="fab fa-instagram"></i>
                        </a>
                        <a href="#" class="text-gray-600 hover:text-primary transition-colors">
                            <i class="fab fa-youtube"></i>
                        </a>
                    </div>
                </div>
            </div>
        </footer>
    </form>

    <script>
        let isLoginForm = true;

        function switchForm() {
            const loginTab = document.getElementById('loginTab');
            const registerTab = document.getElementById('registerTab');
            const welcomeTitle = document.getElementById('welcomeTitle');
            const welcomeText = document.getElementById('welcomeText');
            const switchBtn = document.getElementById('switchBtn');

            if (isLoginForm) {
                loginTab.classList.remove('active');
                registerTab.classList.add('active');
                welcomeTitle.textContent = 'Tham gia cùng chúng tôi!';
                welcomeText.textContent = 'Tạo tài khoản để trải nghiệm mua sắm tuyệt vời và nhận những ưu đãi hấp dẫn từ ToyLand.';
                switchBtn.textContent = 'Đăng nhập';
                isLoginForm = false;
            } else {
                loginTab.classList.add('active');
                registerTab.classList.remove('active');
                welcomeTitle.textContent = 'Chào mừng trở lại!';
                welcomeText.textContent = 'Đăng nhập để khám phá thế giới đồ chơi tuyệt vười và nhận những ưu đãi đặc biệt dành riêng cho bạn.';
                switchBtn.textContent = 'Đăng ký ngay';
                isLoginForm = true;
            }
        }

        function togglePassword(inputId, button) {
            const input = document.getElementById(inputId);
            const icon = button.querySelector('i');
            
            if (input.type === 'password') {
                input.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                input.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        }

        function showModal(message) {
            document.getElementById('successMessage').textContent = message;
            document.getElementById('successModal').classList.remove('hidden');
        }

        function closeModal() {
            document.getElementById('successModal').classList.add('hidden');
            // Redirect to home page after successful login/register
            window.location.href = '<%= ResolveUrl("~/Client/Default.aspx") %>';
        }

        // Client-side validation for agree terms checkbox
        function validateAgreeTerms(sender, args) {
            const checkbox = document.getElementById('<%= chkAgreeTerms.ClientID %>');
            args.IsValid = checkbox.checked;
        }

        // Phone formatting
        document.addEventListener('DOMContentLoaded', function() {
            const phoneInput = document.getElementById('<%= txtPhone.ClientID %>');
            if (phoneInput) {
                phoneInput.addEventListener('input', function(e) {
                    let value = e.target.value.replace(/\D/g, '');
                    if (value.length > 10) value = value.slice(0, 10);
                    
                    if (value.length >= 3) {
                        value = value.replace(/(\d{3})(\d{3})(\d{4})/, '$1 $2 $3');
                    }
                    
                    e.target.value = value;
                });
            }
        });
    </script>
</body>
</html>