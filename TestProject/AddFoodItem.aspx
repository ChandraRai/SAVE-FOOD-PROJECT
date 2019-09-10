<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddFoodItem.aspx.cs" Inherits="AddFoodItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container editForm">
        <h5>Add Food Item</h5>
        <p><span>*</span>indicates required fields</p>
          <div class="form-group" runat="server">
                <div class="input-group mb-3">
                    <asp:TextBox id="txtUserName" type="text" class="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1" runat="server" Enabled="False"/>
                </div>
                <div class="input-group mb-3">
                    <asp:TextBox id="txtFoodName" type="text" class="form-control" placeholder="Food Name*" aria-label="Food Name" aria-describedby="basic-addon1" runat="server"/>
                </div>
                    <asp:RequiredFieldValidator 
                        ID="rfvFoodName" 
                        runat="server" 
                        ErrorMessage="Food Name is Required" 
                        ControlToValidate="txtFoodName" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              <div class="input-group mb-3">
                    <asp:TextBox id="txtExpiry" type="" class="form-control" placeholder="Expiry Date*" aria-label="Expiry Date" aria-describedby="basic-addon1" runat="server" TextMode="Date"/>
                </div>
              <asp:RequiredFieldValidator 
                        ID="rfvExpiry" 
                        runat="server" 
                        ErrorMessage="Expiry Date is Required" 
                        ControlToValidate="txtExpiry" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              <div class="input-group mb-3">
                    <asp:TextBox id="txtAddress" type="text" class="form-control" placeholder="Address*" aria-label="Address" aria-describedby="basic-addon1" runat="server"/>
                </div>
                  <asp:RequiredFieldValidator 
                        ID="rfvAddress" 
                        runat="server" 
                        ErrorMessage="Address is Required" 
                        ControlToValidate="txtAddress" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              <div class="input-group mb-3">
                    <asp:TextBox id="txtCity" type="text" class="form-control" placeholder="City*" aria-label="City" aria-describedby="basic-addon1" runat="server"/>
                </div>
              <asp:RequiredFieldValidator 
                        ID="rfvCity" 
                        runat="server" 
                        ErrorMessage="City is Required" 
                        ControlToValidate="txtCity" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              <div class="input-group mb-3">
                    <asp:TextBox id="txtPostal" type="text" class="form-control" placeholder="Postal Code*" aria-label="Postal Code" aria-describedby="basic-addon1" runat="server"/>
                </div>
              <asp:RequiredFieldValidator 
                        ID="rfvPostal" 
                        runat="server" 
                        ErrorMessage="Postal Code is Required" 
                        ControlToValidate="txtPostal" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              <div class="input-group mb-3">
                <asp:Label runat="server" ID="lblCondition" Text="Condition"></asp:Label>
               <asp:DropDownList runat="server" ID="ddlCondition">
                      <asp:ListItem>Fresh</asp:ListItem>
                      <asp:ListItem>Stale</asp:ListItem>
                      </asp:DropDownList>
                  </div>
              <div>
                <label for="description"></label>
                <asp:TextBox id="txtFoodDesc" runat="server" placeholder="Description*" class="form-control" rows="3" Wrap="False" TextMode="MultiLine"></asp:TextBox>
            </div>
              <asp:RequiredFieldValidator 
                        ID="rfvDesc" 
                        runat="server" 
                        ErrorMessage="Food Description is Required" 
                        ControlToValidate="txtFoodDesc" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
              	<br />
                      <div class="btn-container">
					    <button class="btn btn-danger" id="btnBack">
						    <a href="FoodItemList.aspx" class="a-link-back">
						    Back 
						    </a>
					    </button>
                        <asp:Button class="btn btn-primary" id="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Food"/>
<%--						    <a href="MyItems.aspx" class="a-link-save">
						    Add Food 
						    </a>--%>
                     </div>
              <asp:Label runat="server" ID="lblError"></asp:Label>
                
            </div>
        </div>
</asp:Content>