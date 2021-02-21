<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="OrdersP.aspx.cs" Inherits="WebFastfood.OrdersP" %>
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
                <div class="input-group">
                    <asp:CheckBox ID="DateCheckBox" runat="server" CssClass="date-checkbox" OnCheckedChanged="DateCheckBox_CheckedChanged"/>
                    <asp:TextBox ID="DateTextBox" runat="server" TextMode="Date" style="margin-left:10px;" CssClass="form-control date-date" OnTextChanged="DateTextBox_TextChanged"></asp:TextBox>
                </div>


                <br />
                <br />

                <div class="nav flex-column nav-pills side1" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <asp:LinkButton ID="OrdersToDeliverSection" runat="server" OnClick="OrdersToDeliverSection_Click" class="nav-link nav-link1 active" role="tab" aria-controls="v-pills-Beverages" aria-selected="false">To deliver</asp:LinkButton>
                    <asp:LinkButton ID="OrdersOnTheWaySection" runat="server" OnClick="OrdersOnTheWaySection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-Burgers" aria-selected="true">On the way</asp:LinkButton>
                    <asp:LinkButton ID="OrdersHaveBeenDeliveredSection" runat="server" OnClick="OrdersHaveBeenDeliveredSection_Click" class="nav-link nav-link1" role="tab" aria-controls="v-pills-FullMeal" aria-selected="true">Delivered</asp:LinkButton>
                </div>
                
                    <asp:TextBox ID="OrdersStateTextBox" runat="server" Visible="false"></asp:TextBox>
            </div>

            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">

                    <div id="menu" style="overflow: auto;">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="d-flex flex-wrap justify-content-center" style="overflow-x: auto; padding-top: 20px;">
                                        <asp:ListView ID="OrdersListView" runat="server" OnItemDataBound="OrdersListView_ItemDataBound">
                                            <ItemTemplate>
                                                <div class='card card2' style="" id='order<%# Eval("id_order") %>'>
                                                    <div class="client-full-info-div1 d-flex">
                                                        <asp:Image ID="ProductImage" runat="server" ImageUrl='<%# "~/pictures/clients/" + Eval("CLIENT.photo") %>' alt='TITLE' class='img_client1 img-c' />
                                                        <div class="client-info-div1 d-flex align-items-center">
                                                            <div>
                                                                <span style="font-size: 22px; font-weight: bold;" class='name-c'><%# Eval("CLIENT.firstname")+" "+Eval("CLIENT.lastname") %></span><br />
                                                                <span><i class="fa fa-phone" aria-hidden="true"></i>  <%# Eval("CLIENT.phone") %></span><br />
                                                                <span><i class="fa fa-clock-o" aria-hidden="true"></i>  <%#Eval("order_datetime") %></span><br />
                                                                <asp:TextBox ID="IdOrderTextBox" runat="server" ReadOnly="true" Visible="false" Text='<%#Eval("id_order") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="card-body bill-info1" style="">
                                                        <asp:GridView ID="OrderContentGridView" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="bill1">
                                                            <Columns>
                                                                <asp:BoundField DataField="PRODUCT.title" DataFormatString="• {0}" ItemStyle-CssClass="col-td-1" />
                                                                <asp:BoundField DataField="price" ItemStyle-CssClass="col-td-2"/>
                                                                <asp:BoundField DataField="quantity" DataFormatString="x {0}" ItemStyle-CssClass="col-td-2"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div style="margin-top:5px;">
                                                            <span>
                                                                <span style="font-weight:bold;">Total price : </span> 
                                                                <span class="badge badge-pill badge-warning" style="font-size: 14px;">Dhs</span>
                                                                <asp:Label ID="TotalPriceLabel" runat="server" Text=""></asp:Label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="DeliveryManPanel" runat="server" Visible="false">
                                                        <div class="dm-full-info-div1">
                                                            <div class="d-flex">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/pictures/delivery_men/" + Eval("DELIVERY_MAN.photo") %>' alt='TITLE' class='img_dm1 img-d' />
                                                                <div class="client-info-div1 d-flex align-items-center">
                                                                    <div>
                                                                        <span style="font-size: 22px; font-weight: bold;" class='name-d'><%# Eval("DELIVERY_MAN.firstname")+" "+Eval("DELIVERY_MAN.lastname") %></span><br />
                                                                        <span><i class="fa fa-phone" aria-hidden="true"></i>  <%# Eval("DELIVERY_MAN.phone") %></span><br />
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="TransportPanel" runat="server" Visible="true" CssClass="ml-auto" style="width: 90px; text-align: center;">
                                                                    <div>
                                                                        <asp:Image ID="TransportImage" runat="server" ImageUrl='<%# "~/pictures/app/" + Eval("DELIVERY_MAN.transport") %>' style="width: 40px;" />
                                                                        <br />
                                                                        <span><%# Eval("DELIVERY_MAN.matricule") %></span>
                                                                        <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#maps-modal" id="open_maps" data-id="<%# Eval("id_order") %>" onclick='f(<%# Eval("id_order") %>)'>Maps</button>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <asp:Panel ID="ReceivedTimePanel" runat="server" Visible="false" style="width:100%;">
                                                                <span><span style="font-weight:bold;">Received at : </span> <%#Eval("received_datetime") %></span><br />
                                                            </asp:Panel>
                                                        </div>
                                                        
                                                    </asp:Panel>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="OrdersToDeliverSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="OrdersOnTheWaySection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="OrdersHaveBeenDeliveredSection" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DateCheckBox" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="DateTextBox" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>

    
    <div class="modal fade" id="maps-modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header" style="padding-top: 4px !important; padding-bottom: 4px !important;">
                    <diV class="d-flex flex-wrap" style="width: 70%;">
                        <div>
                            <img id="modal_img_c" src="" style="width:50px;"/>
                            <span id="modal_name_c"></span>
                        </div>
                        <div class="ml-auto">
                            <img id="modal_img_d" src="" style="width:50px;"/>
                            <span id="modal_name_d"></span>
                        </div>
                    </diV>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="map">

                    </div>
                    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBlcbZQIb3Wa2sC3gUlvuwjYr_mo7n__8Y&callback=initMap&libraries=&v=weekly" async ></script>

                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        $("#OrdersAspx").addClass("active");

        $("#menu").css("height", (window.innerHeight - 55));
        $(window).resize(function () {
            $("#menu").css("height", (window.innerHeight - 55));
        });

        $(".nav-link1").click(function () {
            $(".nav-link1").removeClass("active");
            $(this).addClass("active");
        });

        if ($(".date-checkbox").eq(0).find("input").is(":checked")) {
            $(".date-date").eq(0).prop("disabled", false);
        }
        else {
            $(".date-date").eq(0).prop("disabled", true);
        }
        
        $(".date-checkbox").eq(0).change(function () {
            if ($(".date-checkbox").eq(0).find("input").is(":checked")) {
                $(".date-date").eq(0).prop("disabled", false);
            }
            else {
                $(".date-date").eq(0).prop("disabled", true);
            }
        });

        var map;

        $("#maps-modal").on("show.bs.modal", function () {
            console.log("open maps modal");
            //bach n3ti l hight l div d map
            $("#map").css("height", window.innerHeight - 160);

            map = new google.maps.Map(document.getElementById("map"), {
                center: { lat: -34.397, lng: 150.644 },
                zoom: 8,
            });


        });

        $("#maps-modal").on("hide.bs.modal", function () {
            console.log("close maps modal");
        });

        function f(n) {
            let imgc = $("#order" + n).find(".img-c").eq(0).attr("src");
            let namec = $("#order" + n).find(".name-c").eq(0).text();

            let imgd = $("#order" + n).find(".img-d").eq(0).attr("src");
            let named = $("#order" + n).find(".name-d").eq(0).text();

            $("#modal_img_c").attr("src", imgc);
            $("#modal_name_c").text(namec);

            $("#modal_img_d").attr("src", imgd);
            $("#modal_name_d").text(named);
        }

        

    </script>
</asp:Content>
