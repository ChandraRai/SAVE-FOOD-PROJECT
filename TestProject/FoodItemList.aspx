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
  -moz-appearance: none;
  -webkit-appearance: none;
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
                    <asp:Button runat="server" ID="btnRequest" Text="Request Food"
                        class="btn btn-primary" type="submit" OnClick="btnRequest_Click" />
                </div>
            </div>

            <!--This is for Food Item list -->
            <div class="row">
                <div class="col-9" id="myItemListBorder">                    
                        <asp:Repeater ID="repeaterFoodItems" runat="server">
                            <ItemTemplate>
                              <div class="col col-md-4" id="myItems">
                                        <asp:LinkButton CssClass="foodItem-link"
                                            CommandArgument='<%#Eval("donor.username")  + ";" + Eval("FoodName") +";"+Eval("FoodDesc") +";"+Eval("Expiry") +";"+Eval("FId") +";"+Eval("PostingDate")%>' runat="server" OnClick="GetModelData">                            
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
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>


                <!--This is for Request Items -->
                <div class="col-3">
                    <asp:Repeater ID="rptrRequests" runat="server">
                        <ItemTemplate>
                            <h5 style="margin-left:50px; margin-top: 20px">
                                <a data-toggle="collapse" data-target="#items" href="#items">+ <%#Eval("ItemType")%></a>
                            </h5>
                            <div id="items" class="collapse" style="margin-left:50px" >
                                <p>User-Request Id: <%#Eval("URId")%></p>
                                <p>Item Details: <%#Eval("ItemDetails")%></p>
                                <p>Amount: <%#Eval("Amount")%></p>
                                <p>Posted Date: <%#Eval("Date")%></p>
                                <p><i>Posted by: <%#Eval("user.username")%></i></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
      
    </section>

    <!-- foodItem model -->
    <!-- model 1 -->
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
                                    <li>Donor's rating: <span runat="server" id="txtRating"></span></li>
						<span class="star-rating">
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starFive" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starFour" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starThree" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starTwo" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="starOne" Checked="true" />
					   </br>
					</span>
					
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
