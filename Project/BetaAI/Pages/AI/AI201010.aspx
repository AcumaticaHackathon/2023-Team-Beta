<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AI201010.aspx.cs" Inherits="Page_AI201010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="BetaAI.AITransMaint"
        PrimaryView="MasterView"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="MasterView" Width="100%" Height="150px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit18" DataField="TransNbr" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="RefType" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit3" DataField="RefNbr" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule27" StartColumn="True" ></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit17" DataField="SubRefType" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit16" DataField="SubRefNbr" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule28" StartColumn="True" ></px:PXLayoutRule>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit14" DataField="SentimentProcDate" ></px:PXDateTimeEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit10" DataField="KeyPhraseProcDate" ></px:PXDateTimeEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit8" DataField="EntityLinkProcDate" ></px:PXDateTimeEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit9" DataField="EntityRecogProcDate" ></px:PXDateTimeEdit></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
		<Items>
			<px:PXTabItem Text="Analysis" >
				<Template>
					<px:PXFormView runat="server" ID="CstFormView20" DataMember="MasterView">
						<Template>
							<px:PXTextEdit runat="server" ID="CstPXTextEdit21" DataField="AnalysisText" ></px:PXTextEdit></Template></px:PXFormView></Template></px:PXTabItem>
			<px:PXTabItem Text="Sentiment">
				<Template>
					<px:PXFormView runat="server" ID="CstFormView22" DataMember="MasterView" >
						<Template>
							<px:PXTextEdit runat="server" ID="CstPXTextEdit26" DataField="SentimentResult" ></px:PXTextEdit>
							<px:PXNumberEdit runat="server" ID="CstPXNumberEdit25" DataField="SentimentPositive" ></px:PXNumberEdit>
							<px:PXNumberEdit runat="server" ID="CstPXNumberEdit24" DataField="SentimentNeutral" ></px:PXNumberEdit>
							<px:PXNumberEdit runat="server" ID="CstPXNumberEdit23" DataField="SentimentNegative" ></px:PXNumberEdit></Template></px:PXFormView>
					<px:PXGrid DataSourceID="ds" runat="server" ID="CstPXGrid4">
						<Levels>
							<px:PXGridLevel DataMember="Sentiment" >
								<Columns>
									<px:PXGridColumn DataField="SentenceText" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn DataField="SentimentPositive" Width="100" ></px:PXGridColumn>
									<px:PXGridColumn DataField="SentimentNeutral" Width="100" ></px:PXGridColumn>
									<px:PXGridColumn DataField="SentimentNegative" Width="100" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Entity Link">
				<Template>
					<px:PXGrid runat="server" ID="CstPXGrid1" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="EntityLink" >
								<Columns>
									<px:PXGridColumn DataField="Confidence" Width="100" ></px:PXGridColumn>
									<px:PXGridColumn DataField="EntityText" Width="70" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Entity Recognition" >
				<Template>
					<px:PXGrid DataSourceID="ds" runat="server" ID="CstPXGrid5">
						<Levels>
							<px:PXGridLevel DataMember="EntityRecog" >
								<Columns>
									<px:PXGridColumn DataField="EntityText" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Category" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Confidence" Width="100" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></Template></px:PXTabItem>
			<px:PXTabItem Text="Key Phrase" >
				<Template>
					<px:PXGrid DataSourceID="ds" runat="server" ID="CstPXGrid6">
						<Levels>
							<px:PXGridLevel DataMember="KeyPhrase" >
								<Columns>
									<px:PXGridColumn DataField="EntityText" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn DataField="KeyPhrase" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Confidence" Width="100" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></Template></px:PXTabItem></Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
	</px:PXTab>
</asp:Content>