<%@ Page Title="ToyLand - Shop Đồ Chơi Trẻ Em" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebsiteDoChoi.Client.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Banner Slideshow -->
    <div class="container mx-auto px-4 py-4">
        <div class="rounded-lg overflow-hidden relative h-64 md:h-96 shadow-lg">
            <div class="banner-slide absolute inset-0 bg-cover bg-center" style="background-image: url('https://api.placeholder.com/1200/400/FF6B6B/FFFFFF?text=Đồ+chơi+giáo+dục+cho+bé')"></div>
            <div class="banner-slide absolute inset-0 bg-cover bg-center opacity-0" style="background-image: url('https://api.placeholder.com/1200/400/4ECDC4/FFFFFF?text=Khuyến+mãi+mùa+hè')"></div>
            <div class="banner-slide absolute inset-0 bg-cover bg-center opacity-0" style="background-image: url('https://api.placeholder.com/1200/400/FFE66D/1A535C?text=Đồ+chơi+thông+minh')"></div>
            <div class="absolute bottom-4 left-0 right-0 flex justify-center space-x-2">
                <button class="w-3 h-3 rounded-full bg-white opacity-50 hover:opacity-100"></button>
                <button class="w-3 h-3 rounded-full bg-white opacity-50 hover:opacity-100"></button>
                <button class="w-3 h-3 rounded-full bg-white opacity-50 hover:opacity-100"></button>
            </div>
        </div>
    </div>

    <!-- Main Content Area -->
    <div class="container mx-auto px-4 py-4">
        <div class="flex flex-col md:flex-row gap-4">
            <!-- Left Sidebar - Categories -->
            <div class="w-full md:w-1/5">
                <div class="bg-white rounded-lg shadow-md p-4 mb-4">
                    <h3 class="text-lg font-bold text-dark border-b border-gray-200 pb-2 mb-3">Danh mục đồ chơi</h3>
                    <asp:Repeater ID="rptCategories" runat="server">
                        <HeaderTemplate>
                            <ul class="space-y-2">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a href='<%# "/Client/ProductList.aspx?categoryId=" + Eval("Id") %>' class="flex items-center text-gray-700 hover:text-primary transition-colors">
                                    <i class='<%# Eval("Icon") %> w-6'></i>
                                    <span><%# Eval("Name") %></span>
                                    <span class="ml-auto text-xs text-gray-500">(<%# Eval("ProductCount") %>)</span>
                                </a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                
                <!-- Online Users -->
                <div class="bg-white rounded-lg shadow-md p-4 mt-4">
                    <h3 class="text-lg font-bold text-dark border-b border-gray-200 pb-2 mb-3">Thông tin</h3>
                    <div class="flex items-center text-gray-700 mb-2">
                        <i class="fas fa-users text-secondary mr-2"></i>
                        <span>Đang online: </span>
                        <span class="ml-auto font-bold text-primary">
                            <asp:Label ID="lblOnlineUsers" runat="server" Text="257"></asp:Label>
                        </span>
                    </div>
                    <div class="flex items-center text-gray-700 mb-2">
                        <i class="fas fa-shopping-bag text-secondary mr-2"></i>
                        <span>Sản phẩm: </span>
                        <span class="ml-auto font-bold text-primary">
                            <asp:Label ID="lblTotalProducts" runat="server" Text="2,548"></asp:Label>
                        </span>
                    </div>
                    <div class="flex items-center text-gray-700">
                        <i class="fas fa-truck text-secondary mr-2"></i>
                        <span>Đơn hàng hôm nay: </span>
                        <span class="ml-auto font-bold text-primary">
                            <asp:Label ID="lblTodayOrders" runat="server" Text="36"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            
            <!-- Center Content -->
            <div class="w-full md:w-3/5">
                <!-- Best Selling Section -->
                <div class="bg-white rounded-lg shadow-md p-4 mb-4">
                    <div class="flex justify-between items-center border-b border-gray-200 pb-2 mb-4">
                        <h2 class="text-xl font-bold text-dark">Đồ chơi bán chạy</h2>
                        <a href="/Client/ProductList.aspx?filter=bestseller" class="text-primary hover:underline flex items-center">
                            <span>Xem tất cả</span>
                            <i class="fas fa-chevron-right ml-1 text-xs"></i>
                        </a>
                    </div>
                    <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
                        <asp:Repeater ID="rptBestSellingProducts" runat="server">
                            <ItemTemplate>
                                <div class="product-card relative group">
                                    <div class="bg-gray-100 rounded-lg overflow-hidden mb-2">
                                        <asp:Image ID="imgProduct" runat="server" 
                                            ImageUrl='<%# Eval("ImageUrl") %>' 
                                            AlternateText='<%# Eval("Name") %>' 
                                            CssClass="w-full h-40 object-cover transition-transform group-hover:scale-105" />
                                        <div class="product-actions absolute inset-0 bg-black bg-opacity-20 flex items-center justify-center space-x-2 opacity-0 transition-opacity">
                                            <asp:LinkButton ID="btnAddToCart" runat="server" 
                                                CommandName="AddToCart" 
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-shopping-cart"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnAddToWishlist" runat="server" 
                                                CommandName="AddToWishlist" 
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-heart"></i>
                                            </asp:LinkButton>
                                            <asp:HyperLink ID="lnkViewDetails" runat="server" 
                                                NavigateUrl='<%# "/Client/ProductDetails.aspx?id=" + Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-eye"></i>
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                    <h3 class="font-medium text-gray-800 line-clamp-2 h-12"><%# Eval("Name") %></h3>
                                    <div class="flex items-center text-xs mb-1">
                                        <div class="flex text-yellow-400">
                                            <!-- Rating stars will be generated by code-behind -->
                                        </div>
                                        <span class="text-gray-500 ml-1">(<%# Eval("ReviewCount") %>)</span>
                                    </div>
                                    <div class="flex justify-between items-center">
                                        <div>
                                            <span class="text-primary font-bold"><%# Eval("Price", "{0:N0}") %>đ</span>
                                            <asp:Panel ID="pnlDiscount" runat="server" Visible='<%# Convert.ToDecimal(Eval("OriginalPrice")) > 0 %>'>
                                                <span class="text-gray-500 text-xs line-through ml-1"><%# Eval("OriginalPrice", "{0:N0}") %>đ</span>
                                            </asp:Panel>
                                        </div>
                                        <asp:Panel ID="pnlDiscountBadge" runat="server" Visible='<%# Convert.ToInt32(Eval("DiscountPercent")) > 0 %>'>
                                            <span class="bg-red-100 text-red-600 text-xs px-2 py-1 rounded">-<%# Eval("DiscountPercent") %>%</span>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNewBadge" runat="server" Visible='<%# Convert.ToBoolean(Eval("IsNew")) %>'>
                                            <span class="bg-green-100 text-green-600 text-xs px-2 py-1 rounded">Mới</span>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <!-- Advertisement Banner 1 -->
                <div class="rounded-lg overflow-hidden mb-4 shadow-md">
                    <asp:Image ID="imgAdBanner1" runat="server" 
                        ImageUrl="https://api.placeholder.com/800/150/FFE66D/1A535C?text=Giảm+giá+50%+tất+cả+đồ+chơi+giáo+dục" 
                        AlternateText="Khuyến mãi" 
                        CssClass="w-full" />
                </div>

                <!-- Main Products Section -->
                <div class="bg-white rounded-lg shadow-md p-4">
                    <div class="flex justify-between items-center border-b border-gray-200 pb-2 mb-4">
                        <h2 class="text-xl font-bold text-dark">Đồ chơi nổi bật</h2>
                        <div class="flex space-x-2">
                            <asp:LinkButton ID="btnGridView" runat="server" 
                                CssClass="text-gray-600 hover:text-primary transition-colors"
                                OnClick="btnGridView_Click">
                                <i class="fas fa-th-large"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnListView" runat="server" 
                                CssClass="text-gray-600 hover:text-primary transition-colors"
                                OnClick="btnListView_Click">
                                <i class="fas fa-list"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="grid grid-cols-2 md:grid-cols-3 gap-4">
                        <asp:Repeater ID="rptFeaturedProducts" runat="server">
                            <ItemTemplate>
                                <div class="product-card relative group">
                                    <div class="bg-gray-100 rounded-lg overflow-hidden mb-2">
                                        <asp:Image ID="imgFeaturedProduct" runat="server" 
                                            ImageUrl='<%# Eval("ImageUrl") %>' 
                                            AlternateText='<%# Eval("Name") %>' 
                                            CssClass="w-full h-40 object-cover transition-transform group-hover:scale-105" />
                                        <div class="product-actions absolute inset-0 bg-black bg-opacity-20 flex items-center justify-center space-x-2 opacity-0 transition-opacity">
                                            <asp:LinkButton ID="btnAddToCartFeatured" runat="server" 
                                                CommandName="AddToCart" 
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-shopping-cart"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnAddToWishlistFeatured" runat="server" 
                                                CommandName="AddToWishlist" 
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-heart"></i>
                                            </asp:LinkButton>
                                            <asp:HyperLink ID="lnkViewDetailsFeatured" runat="server" 
                                                NavigateUrl='<%# "/Client/ProductDetails.aspx?id=" + Eval("Id") %>'
                                                CssClass="bg-white text-primary p-2 rounded-full hover:bg-primary hover:text-white transition-colors">
                                                <i class="fas fa-eye"></i>
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                    <h3 class="font-medium text-gray-800 line-clamp-2 h-12"><%# Eval("Name") %></h3>
                                    <div class="flex items-center text-xs mb-1">
                                        <div class="flex text-yellow-400">
                                        </div>
                                        <span class="text-gray-500 ml-1">(<%# Eval("ReviewCount") %>)</span>
                                    </div>
                                    <div class="flex justify-between items-center">
                                        <span class="text-primary font-bold"><%# Eval("Price", "{0:N0}") %>đ</span>
                                        <asp:Panel ID="pnlFeaturedBadge" runat="server" Visible='<%# Convert.ToBoolean(Eval("IsHot")) %>'>
                                            <span class="bg-blue-100 text-blue-600 text-xs px-2 py-1 rounded">Hot</span>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Pagination -->
                    <div class="flex justify-center mt-6">
                        <div class="flex space-x-1">
                            <asp:LinkButton ID="btnPrevPage" runat="server" 
                                CssClass="px-3 py-1 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors"
                                OnClick="btnPrevPage_Click">
                                <i class="fas fa-chevron-left"></i>
                            </asp:LinkButton>
                            
                            <asp:Repeater ID="rptPagination" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnPage" runat="server" 
                                        Text='<%# Eval("PageNumber") %>'
                                        CommandArgument='<%# Eval("PageNumber") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("IsCurrentPage")) ? "px-3 py-1 rounded bg-primary text-white" : "px-3 py-1 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors" %>'
                                        OnClick="btnPage_Click" />
                                </ItemTemplate>
                            </asp:Repeater>
                            
                            <asp:LinkButton ID="btnNextPage" runat="server" 
                                CssClass="px-3 py-1 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors"
                                OnClick="btnNextPage_Click">
                                <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Right Sidebar -->
            <div class="w-full md:w-1/5">
                <!-- Shopping Cart Info -->
                <div class="bg-white rounded-lg shadow-md p-4 mb-4">
                    <h3 class="text-lg font-bold text-dark border-b border-gray-200 pb-2 mb-3">Giỏ hàng của bạn</h3>
                    <asp:Panel ID="pnlCartEmpty" runat="server" Visible="false">
                        <p class="text-gray-500 text-center py-4">Giỏ hàng trống</p>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlCartItems" runat="server">
                        <div class="space-y-3">
                            <asp:Repeater ID="rptCartItems" runat="server">
                                <ItemTemplate>
                                    <div class="flex items-start">
                                        <div class="bg-gray-100 rounded w-16 h-16 overflow-hidden mr-2 flex-shrink-0">
                                            <asp:Image ID="imgCartProduct" runat="server" 
                                                ImageUrl='<%# Eval("ImageUrl") %>' 
                                                AlternateText='<%# Eval("Name") %>' 
                                                CssClass="w-full h-full object-cover" />
                                        </div>
                                        <div class="flex-grow">
                                            <h4 class="text-sm font-medium line-clamp-1"><%# Eval("Name") %></h4>
                                            <div class="flex justify-between text-xs text-gray-500">
                                                <span><%# Eval("Quantity") %> x <%# Eval("Price", "{0:N0}") %>đ</span>
                                                <asp:LinkButton ID="btnRemoveFromCart" runat="server" 
                                                    CommandName="RemoveFromCart" 
                                                    CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="text-red-500 hover:text-red-600"
                                                    OnClick="btnRemoveFromCart_Click">
                                                    <i class="fas fa-trash-alt"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="border-t border-gray-200 mt-3 pt-3">
                            <div class="flex justify-between font-medium">
                                <span>Tổng cộng:</span>
                                <span class="text-primary">
                                    <asp:Label ID="lblCartTotal" runat="server" Text="0"></asp:Label>đ
                                </span>
                            </div>
                            <asp:HyperLink ID="lnkCheckout" runat="server" 
                                NavigateUrl="/Client/Cart.aspx"
                                CssClass="mt-3 block bg-primary text-white text-center py-2 rounded hover:bg-primary-dark transition-colors">
                                Thanh toán
                            </asp:HyperLink>
                        </div>
                    </asp:Panel>
                </div>
                
                <!-- Advertisement Banner 2 -->
                <div class="mb-4">
                    <div class="bg-white rounded-lg shadow-md overflow-hidden">
                        <asp:Image ID="imgAdBanner2" runat="server" 
                            ImageUrl="https://api.placeholder.com/400/600/4ECDC4/FFFFFF?text=Đồ+chơi+giáo+dục+giảm+30%" 
                            AlternateText="Quảng cáo" 
                            CssClass="w-full" />
                    </div>
                </div>
                
                <!-- New Products -->
                <div class="bg-white rounded-lg shadow-md p-4">
                    <h3 class="text-lg font-bold text-dark border-b border-gray-200 pb-2 mb-3">Đồ chơi mới</h3>
                    <div class="space-y-3">
                        <asp:Repeater ID="rptNewProducts" runat="server">
                            <ItemTemplate>
                                <div class="flex items-start">
                                    <div class="bg-gray-100 rounded w-16 h-16 overflow-hidden mr-2 flex-shrink-0">
                                        <asp:Image ID="imgNewProduct" runat="server" 
                                            ImageUrl='<%# Eval("ImageUrl") %>' 
                                            AlternateText='<%# Eval("Name") %>' 
                                            CssClass="w-full h-full object-cover" />
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="lnkNewProduct" runat="server" 
                                            NavigateUrl='<%# "/Client/ProductDetails.aspx?id=" + Eval("Id") %>'
                                            CssClass="text-sm font-medium hover:text-primary transition-colors">
                                            <%# Eval("Name") %>
                                        </asp:HyperLink>
                                        <div class="flex text-yellow-400 text-xs">
                                        </div>
                                        <span class="text-primary text-sm font-medium"><%# Eval("Price", "{0:N0}") %>đ</span>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>