﻿<%@ Page Title="Đồ chơi giáo dục - ToyLand" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WebsiteDoChoi.Client.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filter-checkbox:checked + label::before {
            background-color: #FF6B6B;
            border-color: #FF6B6B;
        }
        
        .price-range-slider::-webkit-slider-thumb {
            background: #FF6B6B;
        }
        
        .price-range-slider::-moz-range-thumb {
            background: #FF6B6B;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumb -->
    <div class="container mx-auto px-4 py-3">
        <nav class="text-sm">
            <ol class="list-none p-0 inline-flex text-gray-600">
                <li class="flex items-center">
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="/Client/Default.aspx" 
                        CssClass="hover:text-primary transition-colors">Trang chủ</asp:HyperLink>
                </li>
                <li class="flex items-center">
                    <i class="fas fa-chevron-right mx-2 text-xs"></i>
                    <span class="text-primary font-medium">
                        <asp:Label ID="lblBreadcrumb" runat="server" Text="Đồ chơi giáo dục"></asp:Label>
                    </span>
                </li>
            </ol>
        </nav>
    </div>

    <!-- Main Content -->
    <div class="container mx-auto px-4 pb-8">
        <div class="flex flex-col lg:flex-row gap-6">
            <!-- Left Sidebar - Filters -->
            <div class="w-full lg:w-1/4">
                <div class="bg-white rounded-lg shadow-md p-4 mb-4">
                    <div class="flex justify-between items-center border-b border-gray-200 pb-3 mb-4">
                        <h3 class="text-lg font-bold text-dark">Bộ lọc</h3>
                        <asp:LinkButton ID="btnClearFilters" runat="server" 
                            CssClass="text-sm text-primary hover:underline"
                            OnClick="btnClearFilters_Click">Xóa tất cả</asp:LinkButton>
                    </div>

                    <!-- Category Filter -->
                    <div class="mb-6">
                        <h4 class="font-semibold text-dark mb-3">Danh mục</h4>
                        <div class="space-y-2">
                            <asp:CheckBoxList ID="cblCategories" runat="server" 
                                CssClass="space-y-2" 
                                RepeatLayout="UnorderedList"
                                OnSelectedIndexChanged="cblCategories_SelectedIndexChanged"
                                AutoPostBack="false">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <!-- Price Range Filter -->
                    <div class="mb-6">
                        <h4 class="font-semibold text-dark mb-3">Khoảng giá</h4>
                        <div class="space-y-3">
                            <div class="flex justify-between text-sm text-gray-600">
                                <span>50.000đ</span>
                                <span>1.000.000đ</span>
                            </div>
                            <asp:TextBox ID="txtPriceRange" runat="server" 
                                TextMode="Range" 
                                min="50000" 
                                max="1000000" 
                                value="500000"
                                CssClass="price-range-slider w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer" />
                            <div class="flex gap-2">
                                <asp:TextBox ID="txtPriceFrom" runat="server" 
                                    placeholder="Từ" 
                                    CssClass="w-1/2 px-2 py-1 border rounded text-sm focus:outline-none focus:border-primary" />
                                <asp:TextBox ID="txtPriceTo" runat="server" 
                                    placeholder="Đến" 
                                    CssClass="w-1/2 px-2 py-1 border rounded text-sm focus:outline-none focus:border-primary" />
                            </div>
                        </div>
                    </div>

                    <!-- Age Filter -->
                    <div class="mb-6">
                        <h4 class="font-semibold text-dark mb-3">Độ tuổi phù hợp</h4>
                        <div class="space-y-2">
                            <asp:CheckBoxList ID="cblAgeGroups" runat="server" 
                                CssClass="space-y-2" 
                                RepeatLayout="UnorderedList"
                                OnSelectedIndexChanged="cblAgeGroups_SelectedIndexChanged"
                                AutoPostBack="false">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <!-- Rating Filter -->
                    <div class="mb-6">
                        <h4 class="font-semibold text-dark mb-3">Đánh giá</h4>
                        <div class="space-y-2">
                            <asp:RadioButtonList ID="rblRating" runat="server" 
                                CssClass="space-y-2" 
                                RepeatLayout="UnorderedList"
                                OnSelectedIndexChanged="rblRating_SelectedIndexChanged"
                                AutoPostBack="false">
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <!-- Brand Filter -->
                    <div class="mb-6">
                        <h4 class="font-semibold text-dark mb-3">Thương hiệu</h4>
                        <div class="space-y-2">
                            <asp:CheckBoxList ID="cblBrands" runat="server" 
                                CssClass="space-y-2" 
                                RepeatLayout="UnorderedList"
                                OnSelectedIndexChanged="cblBrands_SelectedIndexChanged"
                                AutoPostBack="false">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <!-- Apply Filters Button -->
                    <asp:Button ID="btnApplyFilters" runat="server" 
                        Text="Áp dụng bộ lọc" 
                        CssClass="w-full bg-primary text-white py-2 rounded hover:bg-primary-dark transition-colors"
                        OnClick="btnApplyFilters_Click" />
                </div>

                <!-- Sale Banner -->
                <div class="bg-white rounded-lg shadow-md overflow-hidden">
                    <asp:Image ID="imgSaleBanner" runat="server" 
                        ImageUrl="https://api.placeholder.com/300/400/4ECDC4/FFFFFF?text=Khuyến+mãi+đặc+biệt" 
                        AlternateText="Khuyến mãi" 
                        CssClass="w-full" />
                </div>
            </div>

            <!-- Right Content - Products -->
            <div class="w-full lg:w-3/4">
                <!-- Category Header -->
                <div class="bg-white rounded-lg shadow-md p-4 mb-6">
                    <div class="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
                        <div>
                            <h1 class="text-2xl font-bold text-dark mb-2">
                                <asp:Label ID="lblCategoryTitle" runat="server" Text="Đồ chơi giáo dục"></asp:Label>
                            </h1>
                            <p class="text-gray-600">
                                <asp:Label ID="lblCategoryDescription" runat="server" Text="Phát triển trí tuệ và kỹ năng học tập cho trẻ em"></asp:Label>
                            </p>
                        </div>
                        <div class="flex items-center space-x-2 text-sm text-gray-600">
                            <span>Hiển thị</span>
                            <span class="font-medium text-primary">
                                <asp:Label ID="lblCurrentRange" runat="server" Text="1-24"></asp:Label>
                            </span>
                            <span>trong tổng số</span>
                            <span class="font-medium text-primary">
                                <asp:Label ID="lblTotalProducts" runat="server" Text="45"></asp:Label>
                            </span>
                            <span>sản phẩm</span>
                        </div>
                    </div>
                </div>

                <!-- Sorting and View Options -->
                <div class="bg-white rounded-lg shadow-md p-4 mb-6">
                    <div class="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
                        <div class="flex flex-wrap items-center gap-4">
                            <div class="flex items-center">
                                <span class="text-gray-700 mr-2">Sắp xếp:</span>
                                <asp:DropDownList ID="ddlSortBy" runat="server" 
                                    CssClass="border border-gray-300 rounded px-3 py-1 focus:outline-none focus:border-primary"
                                    OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="popular" Text="Phổ biến nhất" Selected="True" />
                                    <asp:ListItem Value="newest" Text="Mới nhất" />
                                    <asp:ListItem Value="price-low" Text="Giá thấp đến cao" />
                                    <asp:ListItem Value="price-high" Text="Giá cao đến thấp" />
                                    <asp:ListItem Value="rating" Text="Đánh giá cao nhất" />
                                </asp:DropDownList>
                            </div>
                            <div class="flex items-center">
                                <span class="text-gray-700 mr-2">Hiển thị:</span>
                                <asp:DropDownList ID="ddlPageSize" runat="server" 
                                    CssClass="border border-gray-300 rounded px-3 py-1 focus:outline-none focus:border-primary"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="24" Text="24 sản phẩm" Selected="True" />
                                    <asp:ListItem Value="36" Text="36 sản phẩm" />
                                    <asp:ListItem Value="48" Text="48 sản phẩm" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="flex items-center space-x-2">
                            <span class="text-gray-700 text-sm">Xem:</span>
                            <asp:LinkButton ID="btnGridView" runat="server" 
                                CssClass="p-2 border rounded hover:bg-gray-50 transition-colors bg-primary text-white"
                                OnClick="btnGridView_Click">
                                <i class="fas fa-th"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnListView" runat="server" 
                                CssClass="p-2 border rounded hover:bg-gray-50 transition-colors"
                                OnClick="btnListView_Click">
                                <i class="fas fa-list"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <!-- Products Grid -->
                <div id="productsContainer" class="grid grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-4 mb-6">
                    <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
                        <ItemTemplate>
                            <div class="product-card relative group bg-white rounded-lg shadow-md overflow-hidden">
                                <div class="relative">
                                    <asp:Image ID="imgProduct" runat="server" 
                                        ImageUrl='<%# Eval("ImageUrl") %>' 
                                        AlternateText='<%# Eval("Name") %>' 
                                        CssClass="w-full h-48 object-cover transition-transform group-hover:scale-105" />
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
                                    <!-- Product Badges -->
                                    <asp:Panel ID="pnlNewBadge" runat="server" Visible='<%# Convert.ToBoolean(Eval("IsNew")) %>'>
                                        <span class="absolute top-2 left-2 bg-green-500 text-white text-xs px-2 py-1 rounded">Mới</span>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlHotBadge" runat="server" Visible='<%# Convert.ToBoolean(Eval("IsHot")) %>'>
                                        <span class="absolute top-2 left-2 bg-blue-500 text-white text-xs px-2 py-1 rounded">Hot</span>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDiscountBadge" runat="server" Visible='<%# Convert.ToInt32(Eval("DiscountPercent")) > 0 %>'>
                                        <span class="absolute top-2 left-2 bg-red-500 text-white text-xs px-2 py-1 rounded">-<%# Eval("DiscountPercent") %>%</span>
                                    </asp:Panel>
                                </div>
                                <div class="p-3">
                                    <h3 class="font-medium text-gray-800 mb-2 line-clamp-2 h-12"><%# Eval("Name") %></h3>
                                    <div class="flex items-center text-xs mb-2">
                                        <div class="flex text-yellow-400">
                                            <asp:Literal ID="litRating" runat="server" Text='<%# GenerateStars(Convert.ToDouble(Eval("Rating"))) %>'></asp:Literal>
                                        </div>
                                        <span class="text-gray-500 ml-1">(<%# Eval("ReviewCount") %>)</span>
                                    </div>
                                    <div class="flex justify-between items-center">
                                        <div>
                                            <span class="text-primary font-bold"><%# Eval("Price", "{0:N0}") %>đ</span>
                                            <asp:Panel ID="pnlOriginalPrice" runat="server" 
                                                Visible='<%# Convert.ToDecimal(Eval("OriginalPrice")) > 0 %>' 
                                                CssClass="inline">
                                                <span class="text-gray-500 text-xs line-through ml-1"><%# Eval("OriginalPrice", "{0:N0}") %>đ</span>
                                            </asp:Panel>
                                        </div>
                                        <span class="text-xs text-gray-500"><%# Eval("AgeGroup") %></span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- No Products Found Message -->
                    <asp:Panel ID="pnlNoProducts" runat="server" Visible="false" CssClass="col-span-full">
                        <div class="text-center py-12">
                            <i class="fas fa-search text-6xl text-gray-300 mb-4"></i>
                            <h3 class="text-xl font-semibold text-gray-600 mb-2">Không tìm thấy sản phẩm</h3>
                            <p class="text-gray-500">Hãy thử thay đổi bộ lọc hoặc từ khóa tìm kiếm</p>
                        </div>
                    </asp:Panel>
                </div>

                <!-- Pagination -->
                <div class="bg-white rounded-lg shadow-md p-4">
                    <div class="flex justify-center">
                        <div class="flex space-x-1">
                            <asp:LinkButton ID="btnPrevPage" runat="server" 
                                CssClass="px-3 py-2 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors"
                                OnClick="btnPrevPage_Click">
                                <i class="fas fa-chevron-left"></i>
                            </asp:LinkButton>
                            
                            <asp:Repeater ID="rptPagination" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnPage" runat="server" 
                                        Text='<%# Eval("PageNumber") %>'
                                        CommandArgument='<%# Eval("PageNumber") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("IsCurrentPage")) ? "px-3 py-2 rounded bg-primary text-white" : "px-3 py-2 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors" %>'
                                        OnClick="btnPage_Click" />
                                </ItemTemplate>
                            </asp:Repeater>
                            
                            <asp:LinkButton ID="btnNextPage" runat="server" 
                                CssClass="px-3 py-2 rounded bg-gray-200 text-gray-600 hover:bg-primary hover:text-white transition-colors"
                                OnClick="btnNextPage_Click">
                                <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Filter Checkboxes styling
        document.addEventListener('DOMContentLoaded', function() {
            const filterCheckboxes = document.querySelectorAll('input[type="checkbox"]');
            filterCheckboxes.forEach(checkbox => {
                // Add custom styling class
                checkbox.classList.add('filter-checkbox', 'hidden');
                
                // Create custom checkbox appearance
                const label = checkbox.parentElement;
                if (label.tagName === 'LABEL') {
                    const span = document.createElement('span');
                    span.className = 'w-4 h-4 border-2 border-gray-300 rounded mr-2 flex-shrink-0 relative';
                    const icon = document.createElement('i');
                    icon.className = 'fas fa-check absolute inset-0 text-white text-xs opacity-0';
                    span.appendChild(icon);
                    label.insertBefore(span, label.firstChild);
                }
                
                checkbox.addEventListener('change', function() {
                    const customCheckbox = this.parentElement.querySelector('span');
                    const checkmark = customCheckbox.querySelector('i');
                    if (this.checked) {
                        customCheckbox.style.backgroundColor = '#FF6B6B';
                        customCheckbox.style.borderColor = '#FF6B6B';
                        checkmark.style.opacity = '1';
                    } else {
                        customCheckbox.style.backgroundColor = 'transparent';
                        customCheckbox.style.borderColor = '#d1d5db';
                        checkmark.style.opacity = '0';
                    }
                });
            });

            // View Toggle
            const gridViewBtn = document.getElementById('<%= btnGridView.ClientID %>');
            const listViewBtn = document.getElementById('<%= btnListView.ClientID %>');
            const productsContainer = document.getElementById('productsContainer');

            if (gridViewBtn && listViewBtn) {
                gridViewBtn.addEventListener('click', function(e) {
                    e.preventDefault();
                    gridViewBtn.classList.add('bg-primary', 'text-white');
                    listViewBtn.classList.remove('bg-primary', 'text-white');
                    productsContainer.className = 'grid grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-4 mb-6';
                });

                listViewBtn.addEventListener('click', function(e) {
                    e.preventDefault();
                    listViewBtn.classList.add('bg-primary', 'text-white');
                    gridViewBtn.classList.remove('bg-primary', 'text-white');
                    productsContainer.className = 'grid grid-cols-1 gap-4 mb-6';
                });
            }
        });
    </script>
</asp:Content>