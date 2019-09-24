<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="foodItemList.aspx.cs" Inherits="foodItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function openModal() {
            $('#foodItem1').modal('show');
        }
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="bg-light">
        <div class="container">
            <div class="row">
                <%-- Content Head --%>
                <div class="col-lg-12 text-center head-div">
                    <h2 class="section-heading text-uppercase" id="h2Title" runat="server">Donated Food List</h2>
                    <h3 class="FoodForYou" id="h3Title" runat="server">Food for you.</h3>
                </div>
                <%-- Search Bar --%>
                <div class="search-container1">
                    <asp:TextBox type="text" placeholder="Search.." name="search" runat="server" ID="txtSearch"
                        OnTextChanged="SearchItem" AutoPostBack="True" />
                    <%--<asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"/>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-10">
                    <asp:Button runat="server" ID="btnAddItem" Text="+Add Item" OnClick="btnAddItem_Click"
                        class="btn btn-primary" type="submit" />
                    <asp:Button runat="server" ID="btnPost" OnClick="btnHealthTipsPost_Click" Text="Post Health Tips"
                        class="btn btn-primary" type="submit" />
                </div>
                <div class="col-2">
                      <asp:Button runat="server" ID="btnRequest" Text="Food Request"
                        class="btn btn-primary" type="submit" OnClick="btnRequest_Click" />
                </div>
              </div>
            <div class="row">
                <asp:Repeater ID="repeaterFoodItems" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <div class="col-md-4 col-sm-6 foodItem-item">
                                    <asp:LinkButton CssClass="foodItem-link"
                                        CommandArgument='<%#Eval("donor.username")  + ";" + Eval("FoodName") +";"+Eval("FoodDesc") +";"+Eval("Expiry") +";"+Eval("FId") +";"+Eval("PostingDate")%>'
                                        runat="server"
                                        OnClick="GetModelData">
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
                                        <asp:Label runat="server" ID="lblStatus" Visible="false"></asp:Label>
                                        <p class="text-muted" style="color: black;">
                                            Donor: <%#Eval("donor.username") %>
                                            <br>
                                            Posted: <%#Eval("PostingDate") %>
                                            <br>
                                            Expiry Date: <%#Eval("Expiry") %>
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
                                <a href="DonorInfo.aspx">
                                    <p class="item-intro text-muted">Name of Donor : <span runat="server"
                                            id="txtDonor"></span></p>
                                </a>
                                <img class="img-fluid d-block mx-auto" src="images/01-full.jpg" alt="">
                                <p runat="server" id="txtfoodDesc"></p>
                                <ul class="list-inline">
                                    <li>Date Posted: <span runat="server" id="txtPosted"></span></li>
                                    <li>Expiry Date: <span runat="server" id="txtExpiry"></span></li>
                                </ul>

                                <asp:Button ID="btnPickup" runat="server" Text="+Pickup" CssClass="btn btn-primary"
                                    OnClick="btnPickup_Click" Visible="false" Display="Dynamic" />
                                <asp:Button ID="btnSendEmail" runat="server" Text="Contact Donor"
                                    CssClass="btn btn-danger" OnClick="btnSendEmail_Click" Visible="false"
                                    Display="Dynamic" />
                                <asp:Button ID="btnEdit" runat="server" Text="+Edit" CssClass="btn btn-primary"
                                    OnClick="EditItemsDirect_Click" Visible="false" Display="Dynamic" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger"
                                    Visible="false" Display="Dynamic" OnClick="btnDelete_Click" />
                                <asp:HiddenField ID="hiddenFoodId" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card text-center">
        <div class="card-header">
            <h3>Featured</h3>
        </div>
        <div class="card-body">
            <h5 class="card-title">Food & Health Videos</h5>
            <p class="EatHealthy">Eat Healthy, Live Healthy</p>
            <div class="search-container1">
                <asp:TextBox type="text" placeholder="Youtube link here..." Width="450px" name="search" runat="server"
                    ID="txtVideo" AutoPostBack="False" />
                <asp:Button runat="server" ID="btnShare" Text="Share" OnClick="btnShare_Click" class="btn btn-primary"
                    type="submit" />

            </div>

            <!-- Footer -->
            <footer class="page-footer font-small mdb-color lighten-3 pt-4">
                <!-- Footer Elements -->
                <div class="container">
                    <!--Grid row-->
                    <div class="row">
                        <asp:Repeater ID="rptrVideos" runat="server">
                            <ItemTemplate>
                                <!--Grid column-->
                                <div class="col-lg-2 col-md-12 mb-4">
                                    <!--Image-->
                                    <div class="view overlay z-depth-1-half">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe class="embed-responsive-item"
                                                src='https://www.youtube.com/embed/<%#Eval("Post") %>'></iframe>
                                        </div>
                                        <div class="foodItem-caption">
                                        <asp:Label runat="server" ID="lblStatus" Visible="false"></asp:Label>
                                        <p class="text-muted" style="color: black;">
                                            Posted By: <%#Eval("user.username") %>
                                            <br>
                                            Date Posted: <%#Eval("postingDate") %>
                                        </p>
                                    </div>
                                        <a href="">
                                            <div class="mask rgba-white-light"></div>
                                        </a>
                                    </div>
                                </div>
                                <!--Grid column-->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!--Users post section-->
                    <hr />
                    <div class="row">
                        <div class="card-header">
                            <h5>Food and Health Tips </h5>
                            <b>Find the latest updated content here!</b>
                        </div>
                    </div>
                </div>
            </footer>

            <!-- Health tips display-->
            <div class="row">
                <asp:Repeater ID="repeaterPost" runat="server">
                    <ItemTemplate>
                        <div class="card-body">
                            <h6><%#Eval("title")%></h6>
                            <p><%#Eval("post") %></p>
                            <p><small><i>Posted on: <%#Eval("postingDate") %></i></small></p>
                            <hr />
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
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
                    <asp:Button ID="btnConfirmPopup" runat="server" Text="Confirm" CssClass="btn btn-warning" />
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hiddenFoodSelection" />
        </div>
    </div>
</asp:Content>
