<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyItems.aspx.cs" Inherits="MyItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalAvaliable() {
            $('#foodItem1').modal('show');
        }
        function openModalOrder() {
            $('#foodItem2').modal('show');
        }
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }

    </script>
    <link rel="stylesheet" type="text/css" href="css/master.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="bg-light" id="foodItem">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h4 class="section-heading text-uppercase">My Donated Food List</h4>
                </div>
            </div>
            <hr />
            <div class="row">
                <asp:Repeater ID="repeaterUserFoodItems" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <div class="col-md-4 col-sm-6 foodItem-item">
                                    <asp:LinkButton CssClass="foodItem-link"
                                        CommandArgument='<%#Eval("UserName")  + ";" + Eval("FoodName") +";"+Eval("FoodDesc") +";"+Eval("Expiry") +";"+Eval("FId")+";"+Eval("Status")+";"+Eval("PostingDate") +";donor"%>'
                                        runat="server" OnClick="GetModelData">
                                        <%--<div class="foodItem-hover">
                                    <div class="foodItem-hover-content">
                                      <i class="fas fa-plus fa-3x"></i>
                                    </div>
                              </div>--%>
                                        <div class="foodItem-caption">
                                            <h4 <%# ChangeColor(Eval("Status").ToString(), (DateTime)Eval("Expiry")) %>>
                                                <%#Eval("foodName") %></h4>
                                        </div>
                                        <img class="img-fluid" src="images/01-thumbnail.jpg" alt="">
                                    </asp:LinkButton>
                                    <div class="foodItem-caption">
                                        <p class="text-muted">
                                            Status: <%#ShowFoodStatus((Eval("Status")).ToString())%>
                                            <br>
                                            Posting Date: <%#Eval("PostingDate") %>
                                            <br>
                                            Expiry: <%#Eval("Expiry") %>
                                        </p>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <section class="bg-light">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h4 class="section-heading text-uppercase">My Ordered Food List</h4>
                </div>
            </div>
            <hr />
            <div class="row">
                <asp:Repeater ID="repeaterOrders" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-6 foodItem-item">
                            <asp:LinkButton CssClass="foodItem-link"
                                CommandArgument='<%#Eval("UserName")  + ";" + Eval("FoodName") +";"+Eval("FoodDesc") +";"+Eval("Expiry") +";"+Eval("FId")+";"+Eval("Status")+";"+Eval("PickedUp")+";order"%>'
                                runat="server" OnClick="GetModelData">
                                <%--<div class="foodItem-hover">
                                    <div class="foodItem-hover-content">
                                      <i class="fas fa-plus fa-3x"></i>
                                    </div>
                              </div>--%>
                                <div class="foodItem-caption">
                                    <h4 style="color:black;"><%#Eval("foodName") %></h4>
                                </div>
                                <img class="img-fluid" src="images/01-thumbnail.jpg" alt="">
                            </asp:LinkButton>
                            <div class="foodItem-caption">
                                <p class="text-muted">
                                    Order#: <%#Eval("OId") %>
                                    <br>
                                    Ordered: <%#Eval("PickedUp") %>
                                    <br>
                                    Expiry: <%#Eval("Expiry") %>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <!-- foodItem modal -->

    <!-- modal 1 -->
    <div class="foodItem-modal modal fade" id="foodItem1" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="close-modal" data-dismiss="modal">
                    <div class="lr">
                        <div class="rl"></div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mx-auto">
                            <div class="modal-body">
                                <!-- Project modals Go Here -->
                                <h2 class="text-uppercase" runat="server" id="txtFoodName">Name of Food
                                </h2>
                                <p class="item-intro text-muted">Name of Donor: <span runat="server"
                                        id="txtDonor"></span></p>
                                <img class="img-fluid d-block mx-auto" src="images/01-full.jpg" alt="">
                                <p runat="server" id="txtfoodDesc"></p>
                                <ul class="list-inline">
                                    <li>Date Posted: <span runat="server" id="txtPostedDate"></span></li>
                                    <li>Expiry Date: <span runat="server" id="txtExpiry"></span></li>
                                </ul>
                                <asp:Button ID="Button1" runat="server" Text="Edit Item" CssClass="btn btn-primary"
                                    OnClick="EditItemsDirect_Click" />
                                <asp:Button ID="removeItem" runat="server" Text="Remove Item" CssClass="btn btn-danger"
                                    OnClick="removeItem_Click" />
                                <asp:HiddenField ID="hiddenFoodId" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal 2 -->
    <div class="foodItem-modal modal fade" id="foodItem2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="close-modal" data-dismiss="modal">
                    <div class="lr">
                        <div class="rl"></div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mx-auto">
                            <div class="modal-body">
                                <!-- Project modals Go Here -->
                                <h2 class="text-uppercase" runat="server" id="H1">Order Number:<span runat="server"
                                        id="txtFoodOrderId"></span>
                                </h2>
                                <a href="DonorInfo.aspx">
                                    <p class="item-intro text-muted">
                                        <span runat="server" id="txtFoodOrderUsername"></span>
                                        <br>
                                        Name of Food : <span runat="server" id="txtFoodOrderName"></span>
                                    </p>
                                </a>
                                <img class="img-fluid d-block mx-auto" src="images/01-full.jpg" alt="">
                                <p runat="server" id="txtFoodOrderDesc"></p>
                                <ul class="list-inline">
                                    <li>Orderd: <span runat="server" id="txtFoodOrdered"></span></li>
                                    <li>Expiry Date: <span runat="server" id="txtFoodOrderDate"></span></li>
                                </ul>
                                <div class="btn-container">
                                    <button class="btn btn-danger" id="btnBack">
                                        <a class="a-link-back">Back </a>
                                    </button>
                                </div>
                                <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order"
                                    CssClass="btn btn-danger btn-cancelOrder" OnClick="btnCancelOrder_Click" />
                                <asp:HiddenField ID="hiddenFoodOrderId" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Popup Modal -->
    <div class="modal fade" id="popUpConfirm" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><span runat="server" id="txtPopup"></span></h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <p><span runat="server" id="txtPopupText"></span></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmPopup" runat="server" Text="Confirm" CssClass="btn btn-warning"
                        OnClick="btnConfirmPopup_Click" />

                </div>
            </div>
            <asp:HiddenField runat="server" ID="hiddenFoodSelection" />
        </div>
    </div>
</asp:Content>