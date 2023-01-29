<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AI501000.aspx.cs" Inherits="Page_AI501000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="BetaAI.AIEmailProcess"
        PrimaryView="EmailsToProcess"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid FilesIndicator="False" NoteIndicator="False" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Primary" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="EmailsToProcess">
			    <Columns>
				<px:PXGridColumn Type="CheckBox" DataField="Selected" Width="60" ></px:PXGridColumn>
				<px:PXGridColumn DataField="MailFrom" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn DataField="MailTo" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn Type="CheckBox" DataField="IsIncome" Width="60" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Subject" Width="280" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>