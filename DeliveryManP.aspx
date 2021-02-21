<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="DeliveryManP.aspx.cs" Inherits="WebFastfood.DeliveryManP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <br />
    <br />

    <form id="EditDeliveryManForm" runat="server" method="POST">
        <div style="width: 80%; margin-left: auto; margin-right: auto;">

            <div class="form-row">
                <div class="form-group d-flex flex-wrap  col-md-12 justify-content-center">
                    <asp:Image ID="DeliveryManImage" runat="server" Height="120"/>
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
                    <asp:TextBox ID="PasswordTextBox1" runat="server" CssClass="form-control" TextMode="Password" AutoCompleteType="Disabled" placeholder="Password" MaxLength="100"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-3">
                    <asp:Label ID="TransportLabel" runat="server" AssociatedControlID="TransportImage" Text="Delivering using"></asp:Label>
                    <br />
                    <asp:Image ID="TransportImage" runat="server" Height="38" />
                </div>
                <div class="form-group col-md-3">
                    <asp:Label ID="MatriculeLabel" runat="server" AssociatedControlID="MatriculeTextBox" Text="Matricule"></asp:Label>
                    <asp:TextBox ID="MatriculeTextBox" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group col-md-3 ml-auto align-middle">
                    <asp:CheckBox ID="AuthorizedCheckBox" runat="server" />
                    <asp:Label ID="AuthorizedLabel1" runat="server" AssociatedControlID="AuthorizedCheckBox" Text="Authorized" class="form-check-label"></asp:Label>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group d-flex flex-wrap  col-md-12 justify-content-center">
                    <asp:Button ID="EditButton" Text="Edit" OnClick="EditButton_Click" runat="server" CssClass="btn btn-success col-md-2" ValidationGroup="" Style="padding-left: 30px; padding-right: 30px;" />
                </div>
            </div>
        </div>
    </form>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
