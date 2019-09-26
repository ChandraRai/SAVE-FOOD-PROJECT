<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="tips">
        <div class="container">
            <div class="col-md-8">
                <div class="form-area">
                    <br style="clear: both">
                    <h3 style="margin-bottom: 25px; text-align: center;">Food Request Form</h3>
                    <div class="form-group">
                        <span>Request User : </span>
                          <span runat="server" id="requestUser"/> 
                   
                    </div>
                    <div class="form-group">
                      <span>Request Food Item : </span>
                     <asp:dropdownlist runat="server" id="ddlFood" >
                         <asp:listitem text="Can" value="1"></asp:listitem>
                         <asp:listitem text="Vage." value="2"></asp:listitem>
                         <asp:listitem text="Fruit" value="3"></asp:listitem>
                         <asp:listitem text="Instant Food" value="4"></asp:listitem>
                     </asp:dropdownlist>
                    </div>
                    <div class="form-group">
                        <span>Accept Expire Data : </span>
                     <asp:TextBox type="" class="form-control" aria-label="Expiry Date"
          aria-describedby="basic-addon1" runat="server" TextMode="Date"/>
                    </div>
                    <div class="form-group">
                        <span>
                            Prefer Pick up Area : 
                        </span>
                        <asp:TextBox ID="txtPickupArea" Text="" runat="server" placeHolder="Postal Code"/>
                    </div>
                      <div class="form-group">
                        <span>Prefer Pick up Date</span>
                     <asp:TextBox type="" class="form-control" aria-label="Pick Up Date"
          aria-describedby="basic-addon1" runat="server" TextMode="Date"/>
                    </div>
                    <asp:Button class="btn btn-primary" id="btnRequestSubmit" runat="server" Text="Submit"
                      />
                    <asp:Button class="btn btn-primary" id="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                       />
                </div>
            </div>
        </div>
    </div>

</span>

</asp:Content>

