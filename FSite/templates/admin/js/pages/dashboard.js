//[Dashboard Javascript]

//Project:	Alfa Admin - Responsive Admin Template
//Last change:  25/11/2017
//Primary use:   Used only for the main dashboard (index.html)


$(function () {

  'use strict';

  // Make the dashboard widgets sortable Using jquery UI
  $('.connectedSortable').sortable({
    placeholder         : 'sort-highlight',
    connectWith         : '.connectedSortable',
    handle              : '.box-header, .nav-tabs',
    forcePlaceholderSize: true,
    zIndex              : 999999
  });
  $('.connectedSortable .box-header, .connectedSortable .nav-tabs-custom').css('cursor', 'move');

  // jQuery UI sortable for the todo list
  $('.todo-list').sortable({
    placeholder         : 'sort-highlight',
    handle              : '.handle',
    forcePlaceholderSize: true,
    zIndex              : 999999
  });

  $('.daterange').daterangepicker({
    ranges   : {
      'Today'       : [moment(), moment()],
      'Yesterday'   : [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
      'Last 7 Days' : [moment().subtract(6, 'days'), moment()],
      'Last 30 Days': [moment().subtract(29, 'days'), moment()],
      'This Month'  : [moment().startOf('month'), moment().endOf('month')],
      'Last Month'  : [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    },
    startDate: moment().subtract(29, 'days'),
    endDate  : moment()
  }, function (start, end) {
    window.alert('You chose: ' + start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
  });

	
//ANALYSIS

	$("#baranl").sparkline([1,4,3,7,6,4,8,9,6,8,12,6,7,9,4,8,5,7,9,13,10,9,9,8], {
			type: 'bar',
			height: '70',
			barColor: '#ef5350',
			barWidth: 5,
    		barSpacing: 5,
		});

  // SLIMSCROLL FOR CHAT WIDGET
  $('#direct-chat').slimScroll({
    height: '300px'
  });
	
	
	
	$('#invoice-list').DataTable({
	  'paging'      : true,
	  'lengthChange': false,
	  'searching'   : false,
	  'ordering'    : true,
	  'info'        : true,
	  'autoWidth'   : true,
	});	
	
//-------------
		//- PIE CHART -
		//-------------
		// Get context with jQuery - using jQuery's .get() method.
		var pieChartCanvas = $('#pieChart').get(0).getContext('2d');
		var pieChart       = new Chart(pieChartCanvas);
		var PieData        = [
		  {
			value    : 750,
			color    : '#ab8ce4',
			highlight: '#ab8ce4',
			label    : 'Dress'
		  },
		  {
			value    : 550,
			color    : '#00c292',
			highlight: '#00c292',
			label    : 'Accessories'
		  },
		  {
			value    : 450,
			color    : '#fec107',
			highlight: '#fec107',
			label    : 'Saree'
		  },
		  {
			value    : 350,
			color    : '#03a9f3',
			highlight: '#03a9f3',
			label    : 'Kurta'
		  },
		  {
			value    : 200,
			color    : '#fb9678',
			highlight: '#fb9678',
			label    : 'T-Shirt'
		  }
		];
		var pieOptions     = {
		  //Boolean - Whether we should show a stroke on each segment
		  segmentShowStroke    : true,
		  //String - The colour of each segment stroke
		  segmentStrokeColor   : '#fff',
		  //Number - The width of each segment stroke
		  segmentStrokeWidth   : 2,
		  //Number - The percentage of the chart that we cut out of the middle
		  percentageInnerCutout: 10, // This is 0 for Pie charts
		  //Number - Amount of animation steps
		  animationSteps       : 100,
		  //String - Animation easing effect
		  animationEasing      : 'easeOutBounce',
		  //Boolean - Whether we animate the rotation of the Doughnut
		  animateRotate        : true,
		  //Boolean - Whether we animate scaling the Doughnut from the centre
		  animateScale         : true,
		  //Boolean - whether to make the chart responsive to window resizing
		  responsive           : true,
		  // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
		  maintainAspectRatio  : true,
		  //String - A legend template
		  legendTemplate       : '<ul class="<%=name.toLowerCase()%>-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'
		};
		//Create pie or douhnut chart
		// You can switch between pie and douhnut using the method below.
		pieChart.Doughnut(PieData, pieOptions);

// chart
	$("#linechart").sparkline([1,4,3,7,6,4,8,9,6,8,12], {
			type: 'line',
			width: '100',
			height: '38',
			lineColor: '#06d79c',
			fillColor: '#ffffff',
			lineWidth: 2,
			minSpotColor: '#0fc491',
			maxSpotColor: '#0fc491',
		});
		
		$("#barchart").sparkline([1,4,3,7,6,4,8,9,6,8,12], {
			type: 'bar',
			height: '38',
			barWidth: 6,
			barSpacing: 4,
			barColor: '#e9ab2e',
		});
		$("#discretechart").sparkline([1,4,3,7,6,4,8,9,6,8,12], {
			type: 'discrete',
			width: '50',
			height: '38',
			lineColor: '#745af2',
		});
	
	
//sparkline charts
$(document).ready(function() {
   var sparklineLogin = function() { 
       
  
        $("#sparkline8").sparkline([2,4,4,6,8,5,6,4,8,6,6,2 ], {
            type: 'line',
            width: '100%',
            height: '70',
            lineColor: '#fb9678',
            fillColor: '#fb9678',
            maxSpotColor: '#fb9678',
            highlightLineColor: 'rgba(0, 0, 0, 0.2)',
            highlightSpotColor: '#fb9678'
        });
        
   }
    var sparkResize;
 
        $(window).resize(function(e) {
            clearTimeout(sparkResize);
            sparkResize = setTimeout(sparklineLogin, 500);
        });
        sparklineLogin();

});
	
	
// AREA CHART

	
	
 var area = new Morris.Area({
        element: 'revenue-chart',
        data: [{
            period: '2010',
            iMac: 0,
            iPhone: 0,
            
        }, {
            period: '2011',
            iMac: 130,
            iPhone: 100,
            
        }, {
            period: '2012',
            iMac: 30,
            iPhone: 60,
            
        }, {
            period: '2013',
            iMac: 30,
            iPhone: 200,
            
        }, {
            period: '2014',
            iMac: 200,
            iPhone: 150,
            
        }, {
            period: '2015',
            iMac: 105,
            iPhone: 90,
            
        },
         {
            period: '2016',
            iMac: 250,
            iPhone: 150,
           
        }],
        xkey: 'period',
        ykeys: ['iMac', 'iPhone'],
        labels: ['iMac', 'iPhone'],
        pointSize: 0,
        fillOpacity: 0.4,
        pointStrokeColors:['#b4becb', '#01c0c8'],
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        lineWidth: 0,
        smooth: true,
        hideHover: 'auto',
        lineColors: ['#b4becb', '#01c0c8'],
        resize: true
        
    });


}); // End of use strict
