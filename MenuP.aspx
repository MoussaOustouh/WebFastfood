<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="MenuP.aspx.cs" Inherits="WebFastfood.MenuP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            overflow: hidden !important;
        }

        .side1 .active {
            background-color: var(--colorPrimaryDark) !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row" style="margin-top: 55px;">
            <div class="col-3" style="padding-left: 2%; padding-top: 30px; border-right: 1px solid rgba(0,0,0,.125);">

                <div class="input-group">
                    <asp:TextBox ID="SearchTextBox" runat="server" class="form-control" placeholder="Search"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click" CssClass="btn btn-success">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </span>
                </div>


                <br />
                <br />

                <div class="nav flex-column nav-pills side1" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <asp:LinkButton ID="BeveragesSection" runat="server" OnClick="BeveragesSection_Click" class="nav-link nav-link1 active" role="tab" aria-controls="v-pills-Beverages" aria-selected="false">Beverages</asp:LinkButton>
                    <asp:LinkButton ID="BurgersSection" runat="server" OnClick="BurgersSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Burgers" aria-selected="true">Burgers</asp:LinkButton>
                    <asp:LinkButton ID="FullMealSection" runat="server" OnClick="FullMealSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-FullMeal" aria-selected="true">Full Meal</asp:LinkButton>
                    <asp:LinkButton ID="PizzaSection" runat="server" OnClick="PizzaSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Pizza" aria-selected="false">Pizza</asp:LinkButton>
                    <asp:LinkButton ID="SaladsSection" runat="server" OnClick="SaladsSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Salads" aria-selected="false">Salads</asp:LinkButton>
                    <asp:LinkButton ID="UnavailableSection" runat="server" OnClick="UnavailableSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Salads" aria-selected="false">Unavailable</asp:LinkButton>
                </div>
                
                    <asp:TextBox ID="CategoryTextBox" runat="server" Visible="false"></asp:TextBox>
                    
                    <br />
                    <br />

                    <a class="nav-link btn btn-warning" href="~/AddProductP.aspx" runat="server">Add product </a>
            </div>

            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">

                    <div id="menu" style="overflow: auto;">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="d-flex flex-wrap justify-content-center" style="overflow-x: auto; padding-top: 20px;">
                                        <asp:ListView ID="ProductsListView" runat="server">
                                            <ItemTemplate>
                                                <div class='card card1' style="">
                                                    <div class='product-img-div1 available_<%#Eval("available") %>'>
                                                        <asp:Image ID="ProductImage" runat="server" ImageUrl='<%# "~/pictures/products/" + Eval("picture") %>' alt='<%#Eval("title")%>' class="product-img1" />
                                                    </div>

                                                    <div class="card-body product-info1" style="">
                                                        <h5 class="card-title"><%#Eval("title") %></h5>
                                                        <span class="card-text"><span class="badge badge-pill badge-warning" style="font-size: 16px;">Dhs</span> <%#Eval("price") %></span><br />
                                                        <span class="card-text text-secondary"><%#Eval("description") %></span><br />
                                                        <div class="ml-auto" style="width: 50px;">
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/productP.aspx?id="+Eval("id_product") %>' class="btn btn-primary">Show</asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BeveragesSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BurgersSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="FullMealSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="PizzaSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="SaladsSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="UnavailableSection" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        $("#MenuAspx").addClass("active");

        $("#menu").css("height", (window.innerHeight - 55));
        $(window).resize(function () {
            $("#menu").css("height", (window.innerHeight - 55));
        });

        $(".nav-link1").click(function () {
            $(".nav-link1").removeClass("active");
            $(this).addClass("active");
        });
    </script>
</asp:Content>
