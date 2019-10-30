<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="foodItemList.aspx.cs" Inherits="foodItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function populateRating() {
            var currentRating = parseInt(document.querySelector('.lblRating').innerText);
            var stars = document.querySelectorAll('.star-rating svg');

            for (var i = 0; i < currentRating; i++) {
                stars[i].setAttribute('class', 'filled-star')
            }
        }

        function openModal() {
            $('#foodItem1').modal('show');
            populateRating();
        }
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }
    </script>
    <style>
        .filled-star {
            fill: yellow!important;
        }

        .star-rating svg {
            fill: #DCDCDC;
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

	h1[alt="Simple"] {color: black;}
a[href], a[href]:hover {color: grey; font-size: 1em; text-decoration: none}

.starrating > input {display: none;}  /* Remove radio buttons */

.starrating > label:before { 
  content: "\f005"; /* Star */
  margin: 1px;
  font-size: 5em;
  font-family: FontAwesome;
  display: inline-block; 
}

.starrating > label
{
  color: #222222; /* Start color when not clicked */
}

.starrating > input:checked ~ label
{ color: #ffca08 ; } /* Set yellow color when star checked */

.starrating > input:hover ~ label
{ color: #ffca08 ;  } /* Set yellow color when star hover */



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
                                            <h4 <%# ChangeColor(Eval("Status").ToString(),Eval("Expiry").ToString()) %>>
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
                            <div id="items" class="collapse" style="margin-left: 50px">
                                <p>User-Request Id: <%#Eval("URId")%></p>
                                <p>Item Details: <%#Eval("ItemDetails")%></p>
                                <p>Date: <%#Eval("Date")%></p>
                                <p><i>Posted by: <%#Eval("user.username")%></i></p>

								<a href="#" data-toggle="modal" data-target="#myModal">Accept Request</a>

<!-- Modal -->
<div class="modal fade" id="myModal" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">

        <h4 class="modal-title">Accept Request</h4>
      </div>
      <div class="form-group">
      <label class="control-label col-sm-4" for="id">User:</label>
      <div class="col-sm-10">
        <input runat="server" type="text" class="form-control" id="txtReqUser"  name="id">
      </div>
    </div>
    <div class="form-group">
      <label class="control-label col-sm-4" for="type">Item Type:</label><br />
      <div class="col-sm-10">          
        <input runat="server" type="text" class="form-control" id="txtDetails" placeholder="Item Details" name="details">
      </div>
    </div>
		 <div class="form-group">
      <label class="control-label col-sm-4" for="">Amount:</label>
      <div class="col-sm-10">
        <input type="text" class="form-control" id="amount" placeholder="Amount" name="amount">
      </div>
    </div>
    <div class="form-group">
      <label class="control-label col-sm-4" for="date">Date:</label>
      <div class="col-sm-10">          
        <input type="date" class="form-control" id="date" placeholder="Date" name="date">
      </div>
    </div>
		 <div class="form-group">
      <label class="control-label col-sm-4" for="posted">Posted By:</label>
      <div class="col-sm-10">          
        <input type="text" class="form-control" id="posted" placeholder="Posted By" name="date">
      </div>
    </div>
		  <input type="submit" class="btn btn-info" value="Submit Button">
		
    </div>

  </div>
</div>




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
                                    <p class="item-intro text-muted">
                                        Name of Donor : <span runat="server"
                                            id="txtDonor"></span>
                                    </p>
                                </a>
                                <img class="img-fluid d-block mx-auto" src="images/01-full.jpg" alt="">
                                <p runat="server" id="txtfoodDesc"></p>
                                <ul class="list-inline">
                                    <li>Date Posted: <span runat="server" id="txtPosted"></span></li>
                                    <li>Donor's rating: <span runat="server" id="txtRating" class="lblRating"></span></li>
                                    <div class="star-rating">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24">
                                            <path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z" />
                                        </svg>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24">
                                            <path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z" />
                                        </svg>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24">
                                            <path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z" />
                                        </svg>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24">
                                            <path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z" />
                                        </svg>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24">
                                            <path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z" />
                                        </svg>
                                    </div>

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
