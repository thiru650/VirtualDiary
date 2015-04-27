<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="dbVirtualDiary.Main" MasterPageFile="~/VirtualDiary.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCv8xNxc1zNrBZ1mqcospMuOT80rsZvZ3Q&sensor=false">
    </script>
    <script type="text/javascript">
        function initialize() {

            var mapsStringData = document.getElementById('txtmapdata').value;
            if (mapsStringData.length > 0)
            {
                var mapsDataArr = mapsStringData.split(';');
                var maptitle = [];
                var maplat = [];
                var maplong = [];
                for (j = 0; j < mapsDataArr.length; j += 3) {
                    maptitle.push(mapsDataArr[j]);
                    maplat.push(parseFloat(mapsDataArr[j + 1]));
                    maplong.push(parseFloat(mapsDataArr[j + 2]));
                }

                var bounds = new google.maps.LatLngBounds();

                var mapOptions = {
                    mapTypeId: 'roadmap'
                };
                var div = document.getElementById('map-canvas');
                if (maptitle.length > 0)
                { div.style.display = ''; }
                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

                map.setTilt(45);


                // Loop through our array of markers & place each one on the map  
                for (i = 0; i < maptitle.length; i++) {
                    var position = new google.maps.LatLng(maplat[i], maplong[i]);
                    bounds.extend(position);
                    marker = new google.maps.Marker({
                        position: position,
                        map: map,
                        title: maptitle[i]
                    });
                }

                // Automatically center the map fitting all markers on the screen
                map.fitBounds(bounds);
            }
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td colspan="2">
                <asp:Label Text="Enter a date to check you diary for that day:" runat="server" /><br />
            </td>
        </tr>

        <tr>
            <td style="width: 100px; padding-top: 8px">
                <asp:TextBox ID="startdate" runat="server" Style="width: 80px; height: 30px" ClientIDMode="Static"></asp:TextBox>
                <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="startdate"></ajaxToolkit:CalendarExtender>
            </td>
            <td style="padding-left: 20px; padding-top: 8px">
                <asp:Button runat="server" Text="Go" Style="padding: 3px 30px" OnClick="GoButton_Click" />
            </td>

            <%--<asp:Table runat="server">--%>
            <%--<asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lbldate" runat="server">Date entered:</asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                            </asp:TableCell>
                        </asp:TableRow>--%>
            <%--<asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" ID ="lblwebref" Text="Calling from webservice::   "></asp:Label>
                       
                    </asp:TableCell>
                    <asp:TableCell>
                         <asp:TextBox ID="txtwberef" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>--%>
            <%--<asp:TableRow Height="20px">
                            <asp:TableCell>
                            </asp:TableCell>
                        </asp:TableRow>--%>
            <%--</asp:Table>--%>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Table ID="tblDisplay" runat="server">
                </asp:Table>
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="pnlDisplay" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="tblDisplay" runat="server">
                            </asp:Table>
                        </ContentTemplate>
                    </asp:UpdatePanel> --%>
            </td>
            <td>
                <%--API_KEY = AIzaSyCv8xNxc1zNrBZ1mqcospMuOT80rsZvZ3Q                           For Google Maps--%>
                <div id="map-canvas" style="width: 350px; height: 350px; display:none; padding-right:25px;" />
                <asp:TextBox ID="txtmapdata" runat="server" ClientIDMode="Static" Style="display: none; "></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
