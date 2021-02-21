<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="DeliveryMenP.aspx.cs" Inherits="WebFastfood.DeliveryMenP" %>
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
                    <asp:LinkButton ID="AuthorizedSection" runat="server" OnClick="AuthorizedSection_Click" class="nav-link nav-link1 active" role="tab" aria-controls="v-pills-Beverages" aria-selected="false">Authorized</asp:LinkButton>
                    <asp:LinkButton ID="NotAuthorizedSection" runat="server" OnClick="NotAuthorizedSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Burgers" aria-selected="true">Not authorized</asp:LinkButton>
                </div>
                
                    <asp:TextBox ID="AuthorizationTextBox" runat="server" Visible="false"></asp:TextBox>
                    
                    <br />
                    <br />

                    <a class="nav-link btn btn-warning" href="~/AddDeliveryManP.aspx" runat="server">Add a delivery man</a>
            </div>

            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">

                    <div id="menu" style="overflow: auto;">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="d-flex flex-wrap justify-content-center" style="overflow-x: auto; padding-top: 20px;">
                                        <asp:ListView ID="DeliveryMenListView" runat="server">
                                            <ItemTemplate>
                                                <div class='card card1 available_<%#Eval("authorized") %>' style="">
                                                    <div class="product-img-div1">
                                                        <asp:Image ID="DeliveryMenImage" runat="server" ImageUrl='<%# "~/pictures/delivery_men/" + Eval("photo") %>' alt='<%#Eval("firstname")+" "+Eval("lastname")%>' class="product-img1 ml-auto" />
                                                    </div>

                                                    <div class="card-body product-info1" style="">
                                                        <h5 class="card-title"><%#Eval("firstname")+" "+Eval("lastname")%></h5>
                                                        <span class="card-text"> <%#Eval("phone") %></span><br />
                                                        <span class="card-text text-secondary"><%#Eval("email") %></span><br />
                                                        <div class="ml-auto" style="width: 50px;">
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/DeliveryManP.aspx?id="+Eval("id_delivery_man") %>' class="btn btn-primary">Show</asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="AuthorizedSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="NotAuthorizedSection" EventName="Click" />
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
        $("#DeliveryMenAspx").addClass("active");

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
