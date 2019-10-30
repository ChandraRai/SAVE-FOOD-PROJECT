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
    <style>
        .star-rating {
            font-size: 0;
            white-space: nowrap;
            display: inline-block;
            width: 250px;
            height: 50px;
            overflow: hidden;
            position: relative;
            background: url('data:image/svg+xml;base64,PHN2ZyB2ZXJzaW9uPSIxLjEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iMjBweCIgaGVpZ2h0PSIyMHB4IiB2aWV3Qm94PSIwIDAgMjAgMjAiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDIwIDIwIiB4bWw6c3BhY2U9InByZXNlcnZlIj48cG9seWdvbiBmaWxsPSIjREREREREIiBwb2ludHM9IjEwLDAgMTMuMDksNi41ODMgMjAsNy42MzkgMTUsMTIuNzY0IDE2LjE4LDIwIDEwLDE2LjU4MyAzLjgyLDIwIDUsMTIuNzY0IDAsNy42MzkgNi45MSw2LjU4MyAiLz48L3N2Zz4=');
            background-size: contain;
        }

            .star-rating i {
                opacity: 0;
                position: absolute;
                left: 0;
                top: 0;
                height: 100%;
                width: 20%;
                z-index: 1;
                background: url('data:image/svg+xml;base64,PHN2ZyB2ZXJzaW9uPSIxLjEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iMjBweCIgaGVpZ2h0PSIyMHB4IiB2aWV3Qm94PSIwIDAgMjAgMjAiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDIwIDIwIiB4bWw6c3BhY2U9InByZXNlcnZlIj48cG9seWdvbiBmaWxsPSIjRkZERjg4IiBwb2ludHM9IjEwLDAgMTMuMDksNi41ODMgMjAsNy42MzkgMTUsMTIuNzY0IDE2LjE4LDIwIDEwLDE2LjU4MyAzLjgyLDIwIDUsMTIuNzY0IDAsNy42MzkgNi45MSw2LjU4MyAiLz48L3N2Zz4=');
                background-size: contain;
            }

            .star-rating input {
                opacity: 0;
                display: inline-block;
                width: 20%;
                height: 100%;
                margin: 0;
                padding: 0;
                z-index: 2;
                position: relative;
            }

                .star-rating input:hover + i,
                .star-rating input:checked + i {
                    opacity: 1;
                }

            .star-rating i ~ i {
                width: 40%;
            }

                .star-rating i ~ i ~ i {
                    width: 60%;
                }

                    .star-rating i ~ i ~ i ~ i {
                        width: 80%;
                    }

                        .star-rating i ~ i ~ i ~ i ~ i {
                            width: 100%;
                        }

        ::after,
        ::before {
            height: 100%;
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            text-align: center;
            vertical-align: middle;
        }
    </style>


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
                                        CommandArgument='<%#Eval("donor.UserName")  + ";" + Eval("FoodName") +";"+Eval("FoodDesc") +";"+Eval("Expiry") +";"+Eval("FId")+";"+Eval("Status")+";"+Eval("PostingDate") +";donor"%>'
                                        runat="server"
                                        OnClick="GetModelData">
                              <%--<div class="foodItem-hover">
                                    <div class="foodItem-hover-content">
                                      <i class="fas fa-plus fa-3x"></i>
                                    </div>
                              </div>--%>
                                        <div class="foodItem-caption">
                                            <h4 <%# ChangeColor(Eval("Status").ToString(), Eval("Expiry").ToString()) %>>
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
                                CommandArgument='<%#Eval("foodOrder.donor.UserName")  + ";" + Eval("foodOrder.FoodName") +";"+Eval("foodOrder.FoodDesc") +";"+Eval("FoodOrder.Expiry") +";"+Eval("foodOrder.FId")+";"+Eval("foodOrder.Status")+";"+Eval("postingDate")+";order"%>'
                                runat="server"
                                OnClick="GetModelData">
                              <%--<div class="foodItem-hover">
                                    <div class="foodItem-hover-content">
                                      <i class="fas fa-plus fa-3x"></i>
                                    </div>
                              </div>--%>
                       <div class="foodItem-caption">
                        <h4 style="color:black;"><%#Eval("foodOrder.foodName") %></h4>
                       </div>
                           <img class="img-fluid" src="images/01-thumbnail.jpg" alt="">
                            </asp:LinkButton>
                            <div class="foodItem-caption">
                                <p class="text-muted">
                                    Order#: <%#Eval("OId") %>
                                    <br>
                                    Ordered: <%#Eval("postingDate") %>
                                    <br>
                                    Expiry: <%#Eval("foodOrder.Expiry") %>
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
                                <p class="item-intro text-muted">
                                    Name of Donor: <span runat="server"
                                        id="txtDonor"></span>
                                </p>
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
                                <asp:Panel runat="server" ID="panelRate">
                                    <h2>Rate donor</h2>
                                    <div class="container" align="center">

                                        <span class="star-rating">
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starOne" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starTwo" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starThree" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starFour" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starFive" Checked="true" /><i></i>
                                            </br>								
                                        </span>
                                        <br />
                                        <asp:Button runat="server" class="profile-mb-30" ID="btnSubmitRating" Text="Submit Rating"
                                            Visible="true" Display="Dynamic" OnClick="btnSubmitRating_Click"></asp:Button>



                                    </div>
                                </asp:Panel>
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
