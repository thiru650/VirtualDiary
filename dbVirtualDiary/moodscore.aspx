<%@ Page Title="" Language="C#" MasterPageFile="~/VirtualDiary.Master" AutoEventWireup="true" CodeBehind="moodscore.aspx.cs" Inherits="dbVirtualDiary.moodscore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {            
            var startDateString = document.getElementById('startdate').value;
            var endDateString = document.getElementById('enddate').value;
            if (startDateString != "" && endDateString != "") {

                var pieChartStringData = document.getElementById('pieChartData').value;
                var pieChartDataArr = pieChartStringData.split(',');
                var dataArrayFinal = Array();
                for (j = 0, index = 0; j < pieChartDataArr.length; j += 2, index++)
                {
                    var temp = new Array(pieChartDataArr[j], parseFloat(pieChartDataArr[j + 1]));
                    dataArrayFinal[index] = temp;
                }

                var moodPieChartStringData = document.getElementById('moodPieChartData').value;
                var moodPieChartDataArr = moodPieChartStringData.split(',');
                var moodDataArrayFinal = Array();
                for (j = 0, index = 0; j < moodPieChartDataArr.length; j += 2, index++) {
                    var temp = new Array(moodPieChartDataArr[j], parseFloat(moodPieChartDataArr[j + 1]));
                    moodDataArrayFinal[index] = temp;
                }
                var startDateParts = startDateString.split('/');
                var endDateParts = endDateString.split('/');

                var startDate = new Date(startDateParts[2], (startDateParts[0] - 1), startDateParts[1]);
                var endDate = new Date(endDateParts[2], (endDateParts[0] - 1), endDateParts[1]);

                var timeDiff = Math.abs(endDate.getTime() - startDate.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

                var data = document.getElementById('txtdata').value;
                var dataArray = data.split(',');
                var data2 = [];
                for (var i = 0; i < dataArray.length; i++) {
                    data2[i] = parseFloat(dataArray[i]);
                }
                if (data2.length > 0) {
                    $(function () {
                        // Mood score chart
                        $('#container').highcharts({
                            chart: {
                                zoomType: 'x'
                            },
                            title: {
                                text: 'Mood from ' + startDateString + ' through ' + endDateString
                            },
                            subtitle: {
                                text: document.ontouchstart === undefined ?
                                    'Click and drag in the plot area to zoom in' :
                                    'Pinch the chart to zoom in'
                            },
                            xAxis: {
                                type: 'datetime',
                                minRange: diffDays * 24 * 3600000
                            },
                            yAxis: {
                                title: {
                                    text: 'Mood score'
                                }
                            },
                            legend: {
                                enabled: false
                            },
                            plotOptions: {
                                area: {
                                    fillColor: {
                                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                        stops: [
                                            [0, Highcharts.getOptions().colors[0]],
                                            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                                        ]
                                    },
                                    marker: {
                                        radius: 2
                                    },
                                    lineWidth: 1,
                                    states: {
                                        hover: {
                                            lineWidth: 1
                                        }
                                    },
                                    threshold: null
                                }
                            },

                            series: [{
                                type: 'area',
                                name: 'Mood score',
                                pointInterval: 24 * 3600 * 1000,
                                pointStart: Date.UTC(startDateParts[2], startDateParts[0] - 1, startDateParts[1]),
                                data: data2
                            }]
                        });
                    });
                }

                $(function () {
                    // Social Media usage chart
                    $('#container2').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false
                        },
                        title: {
                            text: 'Social media usage'
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                    }
                                }
                            }
                        },
                        series: [{
                            type: 'pie',
                            name: 'Social media share',
                            data: dataArrayFinal
                        }]
                    });
                });

                $(function () {
                    // Mood share chart
                    $('#container3').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false
                        },
                        title: {
                            text: 'Mood share'
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                    }
                                }
                            }
                        },
                        series: [{
                            type: 'pie',
                            name: 'Mood share',
                            data: moodDataArrayFinal
                        }]
                    });
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>    
                    <table style="padding-top: -50px">
                        <tr>
                            <th>
                                From
                            </th>
                            <th>

                            </th>
                            <th>
                                To
                            </th>
                            <th></th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="startdate" runat="server" Style="width:80px;height:30px" ClientIDMode="Static"></asp:TextBox>
                                <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="startdate"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="width:50px; font-size:x-large; font-weight:bold; text-align: center; font-family:Arial">
                                -
                            </td>
                            <td>
                                <asp:TextBox ID="enddate" runat="server" Style="width:80px;height:30px" ClientIDMode="Static"></asp:TextBox>                                
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="enddate"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="padding-left: 20px">
                                <asp:Button ID="btndateClick" Text="Go" runat="server" OnClick="btndateClick_Click" style="padding:3px 30px" />
                            </td>
                        </tr>
                       
                    </table>
            </td>
        </tr>
        <tr style="width: 70%;">
            <td>
                <asp:TextBox runat="server" ID="txtdata" ClientIDMode="Static" Style="display: none;" />
                <asp:TextBox runat="server" ID="pieChartData" ClientIDMode="Static" Style="display: none;" />
                <asp:TextBox runat="server" ID="moodPieChartData" ClientIDMode="Static" Style="display: none;" />
                <table style="width:100%">
                    <tr>
                        <td>
                            <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                        </td>
                        <td>
                            <div id="container2" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="container3" style="min-width: 310px; height: 400px; margin: 0 auto; align-content:center"></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

