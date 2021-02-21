<%@ Page Title="Fast food - Login" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="LoginP.aspx.cs" Inherits="WebFastfood.LoginP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />

    <form id="LoginForm" runat="server">
        <div style="width: 40%; min-width: 300px !important; margin-left: auto; margin-right: auto;">
            <div class="form-group">
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox" Text="Email address"></asp:Label>
                <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email" CssClass="form-control" MaxLength="50" aria-describedby="emailHelp" placeholder="Enter email"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox" Text="Password"></asp:Label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" MaxLength="100" placeholder="Password"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Button ID="SubmitButton" Text="Login" OnClick="SubmitButton_Click" runat="server" CssClass="btn btn-warning" ValidationGroup="LoginFormValidate" Style="padding-left: 30px; padding-right: 30px;" />
                </div>
                <div class="form-group col-md-8">
                    <asp:Label ID="MessageLabel" runat="server" Style=""></asp:Label>
                </div>
            </div>

            <div>
                <br />
                <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server"
                    ControlToValidate="EmailTextBox"
                    ErrorMessage=""
                    ForeColor="Red"
                    ValidationExpression="^[\s\S]{1,50}$"
                    ValidationGroup="LoginFormValidate">- Email is required (Maximum 50 characters).<br />
                </asp:RequiredFieldValidator>

                <asp:RequiredFieldValidator ID="PasswordTextBoxValidator" runat="server"
                    ControlToValidate="PasswordTextBox"
                    ErrorMessage=""
                    ForeColor="Red"
                    ValidationExpression="^[\s\S]{1,100}$"
                    ValidationGroup="LoginFormValidate">- Password is required (Maximum 100 characters).<br />
                </asp:RequiredFieldValidator>

            </div>

        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        $("#navbar-nav").hide();
    </script>
</asp:Content>

