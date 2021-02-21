<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="ProductP.aspx.cs" Inherits="WebFastfood.ProductP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />

    <form id="EditProductForm" runat="server" method="POST">
        <div style="width: 80%; margin-left: auto; margin-right: auto;">
            <div class="form-row">
                <div class="form-group col-md-7">
                    <asp:Label ID="TitleLabel" runat="server" AssociatedControlID="TitleTextBox" Text="Title"></asp:Label>
                    <asp:TextBox ID="TitleTextBox" runat="server" CssClass="form-control" placeholder="Pizza 4 fromages" MaxLength="100" required="required"></asp:TextBox>
                </div>
                <div class="form-group col-md-5">
                    <asp:Label ID="CategoryLabel" runat="server" AssociatedControlID="CategoryDropDownList" Text="Category"></asp:Label>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Beverages" Selected="True">Beverages</asp:ListItem>
                        <asp:ListItem Value="Burgers">Burgers</asp:ListItem>
                        <asp:ListItem Value="Full Meal">Full Meal</asp:ListItem>
                        <asp:ListItem Value="Pizza">Pizza</asp:ListItem>
                        <asp:ListItem Value="Salads">Salads</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-7">
                    <div class="form-group">
                        <asp:Label ID="PriceLabel" runat="server" AssociatedControlID="PriceTextBox" Text="Price"></asp:Label>
                        <asp:RangeValidator ID="PriceValidator"
                            ControlToValidate="PriceTextBox"
                            MinimumValue="1"
                            MaximumValue="111111"
                            Type="Double"
                            ForeColor="red"
                            ErrorMessage=" - Min : 1 & Max : 111111"
                            runat="server"
                            ValidationGroup="EditProductFormValidate">
                        </asp:RangeValidator>
                        <div class="input-group mb-2">
                            <div class="input-group-prepend">
                                <div class="input-group-text">Dhs</div>
                            </div>
                            <asp:TextBox ID="PriceTextBox" runat="server" CssClass="form-control" TextMode="Number" step="0.5" min="1" max="111111" placeholder="120.50" required="required"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="DescriptionLabel" runat="server" AssociatedControlID="CategoryDropDownList" Text="Description"></asp:Label>
                        <asp:TextBox ID="DescriptionTextArea" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="7" placeholder="Description" />
                    </div>
                </div>
                <div class="form-group col-md-5">
                    <div class="form-group">
                        <asp:Label ID="PictureLabel" runat="server" AssociatedControlID="PictureUpload" Text="Image"></asp:Label>
                        <asp:RegularExpressionValidator ID="ImageValidator" runat="server"
                            ControlToValidate="PictureUpload"
                            ForeColor="Red"
                            Font-Size="Small"
                            ErrorMessage="  - Only '.jpg' or '.png' or '.jpeg'" Font-Bold="True"
                            ValidationExpression="(.*?)\.(jpg|jpeg|png|JPG|JPEG|PNG)$"
                            ValidationGroup="EditProductFormValidate">
                        </asp:RegularExpressionValidator>
                        <asp:FileUpload ID="PictureUpload" runat="server" AllowMultiple="false" CssClass="form-control upload_img" accept="image/png, image/jpeg" BorderStyle="None" />
                    </div>
                    <div class="form-group">
                        <br />
                        <asp:Image runat="server" ID="ProductImage" class="rounded mx-auto d-block img_shadow product_img img_d_n1" />
                    </div>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-3">
                    <asp:CheckBox ID="AvailableCheckBox" runat="server" />
                    <asp:Label ID="AvailableLabel1" runat="server" AssociatedControlID="AvailableCheckBox" Text="Avalable" class="form-check-label"></asp:Label>
                </div>
                <div class="form-group d-flex flex-wrap  col-md-12 justify-content-center">
                    <asp:Button ID="EditButton" Text="Edit" OnClick="EditButton_Click" runat="server" CssClass="btn btn-success col-md-2" ValidationGroup="EditProductFormValidate" Style="" />
                </div>
            </div>

            <div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        $("#AddProductAspx").addClass("active");


        function readURL(input) {
            if (input.files) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $(".product_img").eq(0).attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $(".upload_img").eq(0).change(function () {
            readURL(this);
        });
    </script>
</asp:Content>
