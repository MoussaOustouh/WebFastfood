<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="AddDeliveryManP.aspx.cs" Inherits="WebFastfood.AddDeliveryManP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <br />
    <br />
    <br />

    <form id="AddDeliveryManForm" runat="server" method="POST">
        <div style="width: 80%; margin-left: auto; margin-right: auto;">

            <div class="form-row">
                <div class="form-group d-flex flex-wrap  col-md-12 justify-content-center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/pictures/delivery_men/delivery_man.png" Width="120" Height="120"/>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstNameTextBox" Text="First name"></asp:Label>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="form-control" placeholder="First name" MaxLength="30" required="required"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastNameTextBox" Text="Last name"></asp:Label>
                    <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="form-control" placeholder="Last name" MaxLength="30" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="GenderLabel" runat="server" AssociatedControlID="GenderRadioButtonList" Text="Gender"></asp:Label>
                    <div>
                        <div class="form-check form-check-inline">
                            <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Male" Value="Male" Selected="True" style="margin-right:50px;"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="PhoneTextBox" Text="Phone"></asp:Label>
                    <asp:TextBox ID="PhoneTextBox" runat="server" CssClass="form-control" TextMode="Phone" placeholder="+212612345678" MaxLength="16" required="required"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox1" Text="Email"></asp:Label>
                    <asp:TextBox ID="EmailTextBox1" runat="server" CssClass="form-control" TextMode="Email" AutoCompleteType="Disabled" placeholder="exemple@domaine.com" MaxLength="50" required="required"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox1" Text="Password"></asp:Label>
                    <asp:TextBox ID="PasswordTextBox1" runat="server" CssClass="form-control" TextMode="Password" AutoCompleteType="Disabled" placeholder="Password" MaxLength="100" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group d-flex flex-wrap  col-md-12 justify-content-center">
                    <asp:Button ID="AddButton" Text="Add" OnClick="AddButton_Click" runat="server" CssClass="btn btn-success col-md-2" ValidationGroup="" Style="padding-left: 30px; padding-right: 30px;" />
                </div>
            </div>
        </div>
    </form>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
