var ua = navigator.userAgent;
var match = ua.match('MSIE (.)');
var versions = match && match.length > 1 ? match[1] : 'unknown';
var isTouchDevice =  "ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch  || (navigator.msMaxTouchPoints>0) || (navigator.maxTouchPoints > 0);
var isDesktop = $(window).width() != 0 && !isTouchDevice ? true : false;
var IEMobile = ua.match(/IEMobile/i);
var isIE9 = /MSIE 9/i.test(ua); 
var isIE10 = /MSIE 10/i.test(ua);
var isIE11 = /rv:11.0/i.test(ua) && !IEMobile  ? true : false;
var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || ua.indexOf(' OPR/') >= 0;
var isFirefox = typeof InstallTrigger !== 'undefined';
var isIE = false || !!document.documentMode;
var isEdge = !isIE && !!window.StyleMedia && !isIE11;
var isChrome = !!window.chrome && !!window.chrome.webstore ;
var isBlink = (isChrome || isOpera) && !!window.CSS;
var isSafari = /constructor/i.test(window.HTMLElement) && !ua.match(' Version/5.');
var isSafari5 = ua.match('Safari/') && !ua.match('Chrome') && ua.match(' Version/5.');
// Check Android version 
var AndroidVersion = parseFloat(ua.slice(ua.indexOf("Android")+8)); 
var Version = ua.match(/Android\s([0-9\.]*)/i);
// Check iOS8 version 
var isIOS8 = function() {
  var deviceAgent = navigator.userAgent.toLowerCase();
  return /iphone os 8_/.test(deviceAgent);
}
// Check iOS version 
function iOSversion() {
    if (/iP(hone|od|ad)/.test(navigator.platform)) {
        var v = (navigator.appVersion).match(/OS (\d+)_(\d+)_?(\d+)?/);
        return [parseInt(v[1], 10), parseInt(v[2], 10), parseInt(v[3] || 0, 10)];
    }
}
var iOS = iOSversion();

var ios, android, blackBerry, UCBrowser, Operamini, firefox, windows, smartphone, tablet,touchscreen, all;
var isMobile = {
  ios: (function(){
    return ua.match(/iPhone|iPad|iPod/i);
  }()),
  android: (function(){
    return ua.match(/Android/i);
  }()),
  blackBerry: (function(){
    return ua.match(/BB10|Tablet|Mobile/i);
  }()),
  UCBrowser: (function(){
    return ua.match(/UCBrowser/i);
  }()),
  Operamini: (function(){
    return ua.match(/Opera Mini/i);
  }()),
  
  windows: (function(){
    return ua.match(/IEMobile/i);
  }()),
  smartphone: (function(){
	return (ua.match(/Android|BlackBerry|Tablet|Mobile|iPhone|iPad|iPod|Opera Mini|IEMobile/i) && window.innerWidth <= 440 && window.innerHeight <= 740);
  }()),
  tablet: (function(){
    return (ua.match(/Android|BlackBerry|Tablet|Mobile|iPhone|iPad|iPod|Opera Mini|IEMobile/i) && window.innerWidth <= 1366 && window.innerHeight <= 800);
  }()),

  all: (function(){
    return ua.match(/Android|BlackBerry|Tablet|Mobile|iPhone|iPad|iPod|Opera Mini|IEMobile/i);
  }())
};



if(isTouchDevice  && isMobile.all !== null){
	var TouchLenght = true;
}else if(isMobile.tablet && isFirefox || isMobile.smartphone && isFirefox ){
	var TouchLenght = true;
}else{
	var TouchLenght = false;
}
if(isMobile.Operamini){
	alert('Please disable Data Savings Mode');
}



/*if(TouchLenght == true){
alert('Me')
}
*/

var AnimEnd = "webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend";


var Loadx = 0;



function changeUrl(url, title, description, keyword, dataName, titleog, descriptionog) {
    if (window.history.pushState !== undefined) {
        var c_href = document.URL;
        if (c_href != url && url!='')
            window.history.pushState({path: url, dataName: dataName, title: title, keyword: keyword, description: description, titleog: titleog, descriptionog: descriptionog}, "", url);
    }
    if (title != '') {
        $('#hdtitle').html(title);
        $('meta[property="og:description"]').remove();
        $('#hdtitle').after('<meta property="og:description" content="' + descriptionog + '">');
        $('meta[property="og:title"]').remove();
        $('#hdtitle').after('<meta property="og:title" content="' + titleog + '">');
        $('meta[property="og:url"]').remove();
        $('#hdtitle').after('<meta property="og:url" content="' + url + '">');
        $('meta[name=keywords]').remove();
        $('#hdtitle').after('<meta name="keywords" content="' + keyword + '">');
        $('meta[name=description]').remove();
        $('#hdtitle').after('<meta name="description" content="' + description + '">');
    }
    $('#changlanguage_redirect').val(url);
}




function ResizeWindows() {
var Portrait = $(window).height() > $(window).width();
var Landscape = $(window).height() <= $(window).width();
var img = $('.bg-home img, .mobile-bg img, .bg-picture img');
var Xwidth = $(window).width();
var Yheight = $(window).height();
var RatioScreeen = Yheight / Xwidth;
var RatioIMG = 787 / 1440;
var RatioPopup = 900 / 1100;
var RatioB = 700 / 1100;
var RatioV = 1080 / 1920;

var newXwidth;
var newYheight;
var newW;
var newH;
if(Xwidth > 1100){
if(RatioScreeen > RatioIMG){
	  newYheight = Yheight;
	  newXwidth	= Yheight / RatioIMG;
 }else{
	  newYheight = Xwidth * RatioIMG;
	  newXwidth	= Xwidth;
	  
}
}



$('.go-top').css({'display':'none', 'opacity':0});
$('.album-pic-center').css({'height':Yheight});
$('.content-page').css({'min-height':Yheight/2}); 
/*$('.container').css({'width':Xwidth}); */


var Flex = document.documentElement.style
if (('flexWrap' in Flex)){
   $('.item-wrapper').css({'display':'flex'});
   $('.item-container').css({ 'display':'flex'});
   $('.colum-box').css({ 'display':'flex'});
   
}else if (('WebkitFlexWrap' in Flex)){ 
   $('.item-wrapper').css({'display':'-webkit-flex'});
   $('.item-container').css({ 'display':'-webkit-flex'});
   $('.colum-box').css({ 'display':'-webkit-flex'});
   
}else if (('msFlexWrap' in Flex)){ 
   $('.item-wrapper').css({'display':'-ms-flexbox'});
   $('.item-container').css({ 'display':'-ms-flexbox'});  
   $('.colum-box').css({ 'display':'-ms-flexbox'});
  
} 



 


				
				 if(Xwidth <= 1100){
					 
					$('.nav-click').css({'display':'block'});
					$('.scroll-down').css({'top':Yheight-70});
					$('.video-wrap').css({'width': '100%' , 'height': '100%', 'margin': 0});
					
					   
					
					if(Xwidth <= 440){
					
					   $('.bg-home').css({'width': Xwidth, 'height':(Xwidth+200) * RatioIMG});
					   $('.slide-bg, .slider-home').css({'width': '100%', 'height':(Xwidth+200) * RatioIMG});
					   $('.bg-page').css({'width':Xwidth, 'height':(Xwidth+200) * RatioIMG});
					
					}else if(Xwidth > 440 && Xwidth <= 640){
				
					   $('.bg-home').css({'width': Xwidth, 'height':(Xwidth+100) * RatioIMG});
					   $('.slide-bg, .slider-home').css({'width': '100%', 'height':(Xwidth+100) * RatioIMG});
					   $('.bg-page').css({'width':Xwidth, 'height':(Xwidth+100) * RatioIMG});
					  
					}else{
					
					   $('.bg-home').css({'width': Xwidth, 'height':Xwidth * RatioIMG});
					   $('.slide-bg, .slider-home').css({'width': '100%', 'height':Xwidth * RatioIMG});
					   $('.bg-page').css({'width':Xwidth, 'height':Xwidth * RatioIMG});
					 
					}
					
					 $('.bg-cover').css({'width': Xwidth, 'height':'auto'});
					 // $('.popup-pics-mobile').css({'height':Xwidth * RatioPopup, 'max-height':Xwidth * RatioPopup});
					
					
					 $(img).css({'width': '100%','height': 'auto','left':'auto'});
					 
					 
					   
				       $('.scrollB, .scrollA, .scrollC, .scrollD, .scrollE, .scrollF, .scrollF,.scrollG, .scrollJ').getNiceScroll().remove();
					   $('.scrollA, .scrollC, .scrollD, .scrollE, .scrollF, .scrollH, .scrollJ').css({'height':'auto'});
					  
					   $('.colum-details, .bg-content, .slider-about').css({'width':'100%', 'height':'auto'});
				      
					   $('.content-page, .colum-box').css({'width':Xwidth, 'height':'auto'});
					   $('.box-content').css({'height':'auto'});
					   
					     if($('#about-page, #services-page, #recruitment-page').length){   
					       $('.load-page').css({'height':'auto'});
						   $('.scrollA, .scrollD').css({'height':'auto'});
						    $('.recruitment-content').css({'height':'auto'});
						 }
						 
						
						 if($('#projects-page').length){ 
					        $('.scrollF').css({'height':'auto'});
							$('.col-top .pic').css({'height': 'auto'});
					     }
					
					    if($('#contact-page').length){ 
					      $('.item-container').css({'height':'auto'});
						  $('.scrollE').css({'height':'auto'});
					     }
						 
					   if( $('#news-page').length){
				         $('.colum-box-news').css({'width': Xwidth,'min-height':Yheight,'margin-left':0});
						 $('.news-list').css({'right':'auto'});
						  var Height = $('.colum-box.active').innerHeight() ;
						 $('.box-content').css({'height':Height}); 
					   }
					   
					    if($('#project-details-page').length){
					     $('.colum-project').css({'width':'100%'});
						 $('.box-content-project').css({'width':'100%'}); 
						 $('.scrollH').css({'height':'auto'});
						 $('.colum-box-news-project').css({'width':'100%', 'margin-left':0});
						 $('.news-list-project').css({'right':'auto'});
						 $('.scrollC').css({'height':'auto'});  
						 $('.house-pic').css({'height':'auto', 'margin-top':0});
						
						}
					   
					  if(Xwidth <= 740){
				       $('.news-text img, .item-box img, .album-pic-center img, .content-text img').addClass('zoom-pic');
			         }else{
				       $('.news-text img,  .item-box img, .album-pic-center img, .content-text img').removeClass('zoom-pic');
			          }
					  
					  
				
				 }else if( Xwidth > 1100){
					
					var videoH = Yheight - 160;
					var videoW = videoH / RatioV;
					$('.video-wrap').css({'width':videoW , 'height':videoH, 'margin-left': -videoW/2, 'margin-top': -videoH/2+50});
					
						
                     $('.nav-click').css({'display':'none'});
					 $('.scroll-down').css({'display':'none', 'opacity':0});
                     $('.bg-home').css({'width':Xwidth, 'height':Yheight});
					 $('.slide-bg, .slider-home, .bg-page, .bg-content, .slider-about, .bg-cover').css({'width':Xwidth, 'height':Yheight});
					

					  $(img).css({'width': newXwidth,'height': newYheight,'left':(Xwidth - newXwidth) / 2});
					 $('.content-page').css({'width':Xwidth,'height':Yheight});
                     $('.colum-box').css({'width':Xwidth,'height':Yheight});
					 $('.box-content').css({'height':Yheight});
					 $('.colum-project').css({'width':Xwidth});
					 
					  if( Yheight > 800){
					     var W = (Yheight - 150) / RatioPopup;
					     var H = Yheight - 150;
					  }else{
						var W = (Yheight - 100) / RatioPopup;
					     var H = Yheight - 100;
					  }
					 
					 $('.popup-pics').css({'width':W,  'margin-top':-(H/2), 'margin-left':-(W/2)});
					
					  
				  
				    if($('#about-page, #services-page, #recruitment-page').length){
					  $('.content-text').each(function(index, elm) {
						  var textH = $(elm).find('.scrollA').innerHeight();
						  if(textH >= Yheight-250){
							  $(elm).find('.scrollA').css({'height':Yheight-250});
						  }else{
							  $(elm).find('.scrollA').css({'height':'auto'});
						  }
					  });
					  
						if(Xwidth <= 1420){
					        $('.load-page').css({'height':Yheight-160});
						    $('.recruitment-content').css({'height':Yheight-200});
						    $('.scrollD').css({'height': $('.recruitment-content').height()-40});
					    }else{
						   $('.load-page').css({'height':Yheight-180});
						   $('.recruitment-content').css({'height':Yheight-220});
						   $('.scrollD').css({'height': $('.recruitment-content').height()-40});
						}
					}
					
					 if($('#contact-page').length){ 
					  $('.item-container').css({'height':Yheight});
					      var textH = $('.scrollE').innerHeight();
						  if(textH >= Yheight-190){
							  $('.scrollE').css({'height':Yheight-190});
						  }else{
							  $('.scrollE').css({'height':'auto'});
						  }
					 }
					 
					 if($('#projects-page').length){
						 
						if(Yheight / Xwidth < 0.55){ 
						   if(Xwidth > 1100 && Xwidth <= 1250 ){
								 $('.scrollF').css({'height': Yheight-140});
								 $('.col-top .pic').css({'height': $('.scrollF').height()/1.8}); 
						   }else if(Xwidth > 1250 && Xwidth <= 1420){
								$('.scrollF').css({'height': Yheight-155}); 
								$('.col-top .pic').css({'height': $('.scrollF').height()/1.8});
						   }else{
								$('.scrollF').css({'height': Yheight-160}); 
								$('.col-top .pic').css({'height': $('.scrollF').height()/2.5});
						   }
						  
						}else{
							$('.scrollF').css({'height': Yheight-155}); 
						    $('.col-top .pic').css({'height': $('.scrollF').height()/2.5});
						}
						
						 
					 }
					 
					  if($('#project-details-page').length){
						 var allItem = $('.colum-project').length;
					     var widthItem = $('.colum-project').width(); 
					     $('.box-content-project').width(allItem * widthItem);  
						 
						 $('.content-pro-text').each(function(index, elm) {
						
						  var textH = $(elm).find('.content').innerHeight();
						
						  if(textH >= Yheight-420){
							 if(Xwidth > 1100 && Xwidth <= 1250 ){
							       $(elm).find('.scrollH').css({'height':Yheight-410});
							   }else if(Xwidth > 1250 && Xwidth <= 1420){
								   $(elm).find('.scrollH').css({'height':Yheight-420});
							   }else{
								   $(elm).find('.scrollH').css({'height':Yheight-430});
							   }
						  }else{
							  $(elm).find('.scrollH').css({'height':'auto'});
						  }
					    });
						
						
						   if(Xwidth > 1100 && Xwidth <= 1250 ){
								 $('.scrollJ').css({'height': Yheight-140});
						   }else if(Xwidth > 1250 && Xwidth <= 1420){
								$('.scrollJ').css({'height': Yheight-155}); 
						   }else{
								$('.scrollJ').css({'height': Yheight-160}); 
						   }
						
						 
						$('.colum-box-news-project').css({'width':Xwidth-450});
						$('.colum-box-news-project').css({'margin-left': - $('.colum-box-news-project').width()/2+20 });
						$('.news-list-project').css({'right':$('.colum-box-news-project').width()+(Xwidth/2 - $('.colum-box-news-project').width()/2-18)});
						$('.scrollC').css({'height':$('.colum-box-news-project').height()-20});  
						
						if(Yheight / Xwidth < 0.55){ 
						    $('.house-pic').css({'height':Yheight-220, 'margin-top':Yheight/2 - (Yheight-220)/2 - 45});  
						}else{
							 $('.house-pic').css({'height':Yheight-300, 'margin-top':Yheight/2 - (Yheight-300)/2 - 45}); 
						}
						 
					  }
					  
					  
					  
					if($('#news-page').length){
						var Top = $('.colum-box-news').offset().top
						$('.colum-box-news').css({'width':Xwidth-500, 'min-height':'inherit'});
						$('.colum-box-news').css({'margin-left': - $('.colum-box-news').width()/2 });
						$('.news-list').css({'right':$('.colum-box-news').width()+(Xwidth/2 - $('.colum-box-news').width()/2+2)});
						$('.scrollC').css({'height':Yheight-120}); 
					}
					
					 
					    var H1 = $('.item-box').innerHeight();
				        $('.album-box .slide-buttons').css({'top': - (H1/2 + 78)});
						var H2 = $('.vid-box').innerHeight();
				        $('.video-box .slide-buttons').css({'top': - (H2/2 + 100)});
					 
					  $('.news-text img,  .item-box img, .album-pic-center img, .content-text img').removeClass('zoom-pic');
					  $('.sub-nav-block ul, .sub-nav-typical ul').css({'width':'100%'});
					    

		       }
			            
						
				         var allItem = $('.colum-box').length;
					     var widthItem = $('.colum-box').width(); 
					     $('.box-content').width(allItem * widthItem);  		
						
            	      	
			
}



function initialize() {

var httpserver = $('.httpserver').text();
var httptemplate = $('.httptemplate').text();
var infoboxaddress = $('.infobox-address').text();
var infoboxlocationlat = $('.infobox-location-lat').text();
var infoboxlocationlng = $('.infobox-location-lng').text();
var infoboximage = $('.infobox-image').text();
var infoboxgooglemap = $('.infobox-googlemap').text();
var infoboxtitle = $('.infobox-name').text();

var infoboxphone = $('.infobox-phone').text();
var infoboxphonetel = $('.infobox-phone-tel').text();
var infoboxemail = $('.infobox-email').text();
var infoboxtextemail = $('.infobox-text-email').text();
var infoboxtexttel = $('.infobox-text-tel').text();

var Center = new google.maps.LatLng(infoboxlocationlat,infoboxlocationlng);

var marker = null;
	
	  var styles = [
		  {
			stylers: [
			  { hue: "#628dd0" },
			  { saturation: -20 }
			]
		  },{
			featureType: "road",
			elementType: "geometry",
			stylers: [
			  { lightness: 50 },
			  { visibility: "simplified" }
			]
		  },{
			featureType: "road",
			elementType: "labels",
			stylers: [
			  { visibility: "on" }
			]
		  }
		];

	  var styledMap = new google.maps.StyledMapType(styles,
      {name: "Styled Map"});
	    
	  
	  /*if($(window).width() > 1100){
	       var isDraggable =  true ;
	  }else{
		   var isDraggable =  false ;
	  }
	
	  $('.touch-m').click(function ()  {
		  if(!$('.touch-m').hasClass('active')){
				 var  mapOptions = {
					 draggable:true
				 };
				  map.setOptions(mapOptions);
				  $('.touch-m').addClass('active');
				  return false;
			}else{
				  var  mapOptions = {
					draggable:false
				 };
				  map.setOptions(mapOptions);
				 $('.touch-m').removeClass('active');
				 return false;
			}
		 });*/
	
	  
	  var mapOptions = {
	  center: Center,
	  zoom: 16,
	  scrollwheel: true,
	  //draggable:isDraggable,
	  draggable:true,
	  draggingCursor: 'move',
	  noclear: true,
	  disableDefaultUI: true,
	  disableDoubleClickZoom : true,
	  mapTypeControlOptions: {
      mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style'],
      position: google.maps.ControlPosition.TOP_RIGHT
	  }
	  
	  };
	  
	  
	  google.maps.event.addDomListener(window, "resize", function() {
        google.maps.event.trigger(map, "resize");
        map.setCenter(Center);
		map.setZoom(16);
     });

	  
	  var map = new google.maps.Map(document.getElementById("map-canvas"),mapOptions);
	  var styledMapOptions = { name: "ANPHALAND" };
	 
	  map.mapTypes.set('map_style', styledMap);
      map.setMapTypeId('map_style');
	  
	  
	  var logo = httptemplate + 'default/images/logo-map.png';
	  marker = new google.maps.Marker({
		  map: map,
		  draggable:false,
		  animation: google.maps.Animation.DROP,
		  position: new google.maps.LatLng(infoboxlocationlat,infoboxlocationlng),
		  icon: logo
	  });
	  

    function bounceAnimationMarker() {marker.setAnimation(google.maps.Animation.BOUNCE);}
	function clearAnimationMarker() { marker.setAnimation(null);}
	
	 
	
	
	  google.maps.event.addListener(marker, 'click', openBox);
	
	
     
	 
	 function openBox() {
	  clearAnimationMarker();
	  var boxText = document.createElement("div");
	  boxText.innerHTML = 
	  "<div class='infobox'>"
	  +"<img src='"+infoboximage+"'  alt='" + infoboxtitle + "' >"
	  +"<h3>" + infoboxtitle + "</h3>"
	  +"<p>" + infoboxaddress + "<br>"
	  + infoboxtexttel + infoboxphone + "<br>"
	  + infoboxtextemail + infoboxemail + "<br>"
	  +"</p></div>"; 
	
	  
	  var myOptions = {
	    content: boxText,
		disableAutoPan: true,
		maxWidth: 280,
		pixelOffset: new google.maps.Size(-110, -140),
		boxStyle: {background: "transparent",opacity: 1 ,width: "300px"},
		closeBoxMargin: "0",
		closeBoxzIndex: "99999",
		closeBoxPosition: "absolute",
		closeBoxURL: httptemplate + "default/images/close3.png",
		infoBoxClearance: new google.maps.Size(1, 1),
		isHidden: false,
		pane: "floatPane",
		enableEventPropagation: true
	  };
	  
	  var showinfo = new InfoBox(myOptions);
	  showinfo.open(map, marker);	
	    
	  }
	 
	 ZoomControl(map);
		 
}

       

function ZoomControl(map) {
  $('.zoom-control a').click(function ()  {
   var zoom = map.getZoom();
	switch ($(this).attr("id")) {
	case "zoom-in":
		map.setZoom(++zoom);
		break;
	case "zoom-out":
		map.setZoom(--zoom);
		break;
	default:
		break
	}
	return false
  
 });
 
 
}


function LoadProjects() {
  var $grid = $('.grid');
 $grid.imagesLoaded( function() {
	
 
 // SORT PROJECT 	
 $('.sub-menu li:first-child').addClass('current');
 $('.sub-menu li a').click(function(e){
	  e.preventDefault();
	    if ($(window).width() > 1100) {
	       $('.scrollF').scrollTop(0);
		}
		
	  $('.sub-menu li').removeClass('current'); 
	  $(this).parent().addClass('current');
	   var filterValue = $(this).attr('data-filter');
	   
	   $grid.isotope({ filter: filterValue});
	   $grid.isotope('shuffle');
	   $grid.on( 'arrangeComplete', onArrange);
			  function onArrange() {
				   if ($(window).width() > 1100) {
				     $('.scrollF').getNiceScroll().resize();
				   }else{
					   detectBut();
				   }
			   }
		  
	  return false;		 
			   
	});
  
   $grid.isotope({
	   itemSelector: '.item-content-box',
	   stagger: 50,
	   percentPosition: true,
	   transitionDuration: '0.6s',
		 hiddenStyle: {
		  opacity: 0,
		},
		visibleStyle: {
		 opacity: 1,
		}
	 });
	 
     	$('.item-content-box').children().each(function(i){
		var box = $(this);
		   setTimeout(function(){$(box).addClass('show')}, (i+1) * 100);
	   });
	   
	    $('.loadicon').fadeOut(500, function () { 
				 $('.loadicon').remove();
			 });
	   
	});		   
			   	 	  
 }


function Calculation() {

var Xwidth = $(window).width();
var Yheight = $(window).height();
 if(Xwidth <= 1100){
       var totalWidth = 0;
		 $('.sub-news li, .sub-menu li, .sub-nav-typical li').each(function(index, element) {
		 var widthThumb = $(this).outerWidth()+4;
		 totalWidth += parseInt(widthThumb);
		 $('.sub-news ul, .sub-menu ul, .sub-nav-typical ul').width(totalWidth);
		}); 
		
			
		 $('.scrollB').each(function(index, element) {
             var Thumb = $(this).find('.link-page').length;
			 var Width = $(this).find('.link-page').outerWidth()+5;
             $(this).width(Thumb * Width);
		  });		 
		
		 var IMG = $('.popup-pics-mobile img').attr('src');
		 $('.popup-img').css({'background-image':'url(  '+ IMG +'  )'});	 
 }else{
	   $('.sub-news ul, .sub-menu ul, .sub-nav-typical ul').css({'width':'100%'});
	   $('.scrollB').css({'width':'100%'});
 }

}


function GradientText(){
  $('.nav li a, .hotline a, .news-slide h2').pxgradient({step: 4, colors: ["#ffffff","#d9d5b1","#d9d5b1","#ccc79c"],   dir: "y"});	
}


function Done() {
	 $('html, body').scrollTop(0);
       ResizeWindows();
	   Calculation(); 
	   //GradientText();
	    $(".title-page > h1").lettering('words').children('span').lettering().children('span').lettering();

		 $('.container').stop().animate({'opacity':1},500 ,'linear', function () { 
		  ContentLoad();
		  
		   if($('.grid').length){
	           LoadProjects();
          }else{
			 $('.loadicon').fadeOut(500, function () { 
				 $('.loadicon').remove();
			 });
		  }
		 });
}








(function($) {
	
    if (!Array.prototype.indexOf)
	   {
	   Array.prototype.indexOf = function(elt /*, from*/)
             {
             var len  = this.length >>> 0;
             var from = Number(arguments[1]) || 0;
                 from = (from < 0)
                      ? Math.ceil(from)
                      : Math.floor(from);
             if (from < 0)
                 from += len;
 
                 for (; from < len; from++)
                     {
                     if (from in this &&
                     this[from] === elt)
                     return from;
                     }
             return -1;
             };
       }

    var Yheight = $(window).height();
    var Xwidth = $(window).width();	
    var qLimages = new Array;
    var qLdone = 0;
    var qLdestroyed = false;
    var qLimageContainer = "";
    var qLoverlay = "";
    var qLbar = "";
    var qLpercentage = "";
    var qLimageCounter = 0;
    var qLstart = 0;

    var qLoptions = {
        onComplete: function () {
			      $('#qLoverlay').remove();
			      $('body .item-load').remove();
			   },
        backgroundColor: "#fff",
        barColor: "#fff",
        barHeight: 1,
        percentage: true,
        deepSearch: true,
        completeAnimation: "fade",
        minimumTime: 500,
        onLoadComplete: function () {
            if (qLoptions.completeAnimation == "grow") {
                var animationTime = 100;
                var currentTime = new Date();
                if ((currentTime.getTime() - qLstart) < qLoptions.minimumTime) {
                    animationTime = (qLoptions.minimumTime - (currentTime.getTime() - qLstart));
                }

                 $('#qLbar').stop().animate({"width": "100%"}, animationTime, function () {
					    
						  $('#qLoverlay').fadeOut(200, function () {      
						         qLoptions.onComplete();
								  if( Loadx == 0){
									  Loadx = 1;
                                      Done();
                                   }
								 ResizeWindows();
								
                          });
                });
			}
		}
            
    };
	
	      
    var afterEach = function () {
        //start timer
        var currentTime = new Date();
        qLstart = currentTime.getTime();

        createPreloadContainer();
        createOverlayLoader();
    };

    var createPreloadContainer = function() {
         qLimageContainer = $('<div class="item-load"></div>').appendTo("body").css({
            display: "none",
            width: 0,
            height: 0,
            overflow: "hidden"
        });
        for (var i = 0; qLimages.length > i; i++) {
            $.ajax({
                url: qLimages[i],
                type: 'HEAD',
                success: function(data) {
                    if(!qLdestroyed){
                        qLimageCounter++;
                        addImageForPreload(this['url']);
                    }
                }
            });
        }
    };

    var addImageForPreload = function(url) {
        var image = $("<img/>").attr("src", url).bind("load", function () {
            completeImageLoading();
        }).appendTo(qLimageContainer);
    };

    var completeImageLoading = function () {
        qLdone++;

        var percentage = (qLdone / qLimageCounter) * 100;
        $(qLbar).stop().animate({
            width: percentage + "%",
            minWidth: percentage + "%"
        }, 200);

        if (qLoptions.percentage == true) {
            $(qLpercentage).text(Math.ceil(percentage) + "%");
        }

        if (qLdone == qLimageCounter) {
            destroyQueryLoader();
        }
    };

    var destroyQueryLoader = function () {
        $(qLimageContainer).remove();
        qLoptions.onLoadComplete();
        qLdestroyed = true;
    };

    var createOverlayLoader = function () {
            qLoverlay = $('<div id="qLoverlay"></div>').css({
            width: "100%",
            height: "10px",
            position: "absolute",
            zIndex:1000,
            top: 0,
            left: 0,
        }).appendTo("body");
        qLbar = $('<div id="qLbar"></div>').css({
            height: qLoptions.barHeight + "px",
            backgroundColor: qLoptions.barColor,
            width: "0%",
            position: "absolute",
            top: "0px"
        }).appendTo(qLoverlay);
        if (qLoptions.percentage == true) {
            qLpercentage = $('<div id="qLpercentage"></div>').text("0%").css({
               height: "120px",
			   width: "120px",
			   position: "absolute",
			   fontSize: "0px",
			   top: "50%",
			   left: "50%",
			   marginTop: "60px" ,
			   textAlign: "center",
			   marginLeft: "-60px",
			   color: "#fff"
            }).appendTo(qLoverlay);
        }
    };

    var findImageInElement = function (element) {
        var url = "";

        if ($(element).css("background-image") != "none") {
            var url = $(element).css("background-image");
        } else if (typeof($(element).attr("src")) != "undefined" && element.nodeName.toLowerCase() == "img") {
            var url = $(element).attr("src");
        }

        if (url.indexOf("gradient") == -1) {
            url = url.replace(/url\(\"/g, "");
            url = url.replace(/url\(/g, "");
            url = url.replace(/\"\)/g, "");
            url = url.replace(/\)/g, "");

            var urls = url.split(", ");

            for (var i = 0; i < urls.length; i++) {
                if (urls[i].length > 0 && qLimages.indexOf(urls[i]) == -1) {
                    var extra = "";
                    qLimages.push(urls[i] + extra);
                }
            }
        }
    }

    $.fn.queryLoader = function(options) {
        if(options) {
            $.extend(qLoptions, options );
        }

        this.each(function() {
            findImageInElement(this);
            if (qLoptions.deepSearch == true) {
                $(this).find("*:not(script)").each(function() {
                    findImageInElement(this);
                });
            }
        });

        afterEach();

        return this;
    };

})(jQuery);





$(document).ready(function () {
$('html, body').scrollTop(0);
ResizeWindows();
//if(!isFirefox){
//	$('head').append('<link rel="stylesheet" href="' + window.location.origin + '/assets/styles/css/scrollbar.css" type="text/css" >')
//}


if(!$('.loadicon').length){
    $('body').append('<div class="loadicon" style="display:block"></div>');	
}
$('body').queryLoader({ barColor: "#fff", percentage: false, barHeight:1, completeAnimation: "grow",  minimumTime:100});	
setTimeout(function(){if( Loadx == 0){ Loadx = 1;  Done();}}, 500);


});



//------//IMAGE LOAD//--------//

/*!
 * imagesLoaded PACKAGED v3.2.0
 * JavaScript is all like "You images are done yet or what?"
 * MIT License
 */


(function () {
	'use strict';

	function EventEmitter() {}

	var proto = EventEmitter.prototype;
	var exports = this;
	var originalGlobalValue = exports.EventEmitter;
	function indexOfListener(listeners, listener) {
		var i = listeners.length;
		while (i--) {
			if (listeners[i].listener === listener) {
				return i;
			}
		}

		return -1;
	}

	function alias(name) {
		return function aliasClosure() {
			return this[name].apply(this, arguments);
		};
	}


	proto.getListeners = function getListeners(evt) {
		var events = this._getEvents();
		var response;
		var key;
	
		if (typeof evt === 'object') {
			response = {};
			for (key in events) {
				if (events.hasOwnProperty(key) && evt.test(key)) {
					response[key] = events[key];
				}
			}
		}
		else {
			response = events[evt] || (events[evt] = []);
		}

		return response;
	};
	proto.flattenListeners = function flattenListeners(listeners) {
		var flatListeners = [];
		var i;

		for (i = 0; i < listeners.length; i += 1) {
			flatListeners.push(listeners[i].listener);
		}

		return flatListeners;
	};
	proto.getListenersAsObject = function getListenersAsObject(evt) {
		var listeners = this.getListeners(evt);
		var response;

		if (listeners instanceof Array) {
			response = {};
			response[evt] = listeners;
		}

		return response || listeners;
	};
	proto.addListener = function addListener(evt, listener) {
		var listeners = this.getListenersAsObject(evt);
		var listenerIsWrapped = typeof listener === 'object';
		var key;

		for (key in listeners) {
			if (listeners.hasOwnProperty(key) && indexOfListener(listeners[key], listener) === -1) {
				listeners[key].push(listenerIsWrapped ? listener : {
					listener: listener,
					once: false
				});
			}
		}

		return this;
	};


	proto.on = alias('addListener');
	proto.addOnceListener = function addOnceListener(evt, listener) {
		return this.addListener(evt, {
			listener: listener,
			once: true
		});
	};
	proto.once = alias('addOnceListener');
	proto.defineEvent = function defineEvent(evt) {
		this.getListeners(evt);
		return this;
	};

	proto.defineEvents = function defineEvents(evts) {
		for (var i = 0; i < evts.length; i += 1) {
			this.defineEvent(evts[i]);
		}
		return this;
	};

	proto.removeListener = function removeListener(evt, listener) {
		var listeners = this.getListenersAsObject(evt);
		var index;
		var key;

		for (key in listeners) {
			if (listeners.hasOwnProperty(key)) {
				index = indexOfListener(listeners[key], listener);

				if (index !== -1) {
					listeners[key].splice(index, 1);
				}
			}
		}

		return this;
	};

	proto.off = alias('removeListener');

	proto.addListeners = function addListeners(evt, listeners) {
		// Pass through to manipulateListeners
		return this.manipulateListeners(false, evt, listeners);
	};

	proto.removeListeners = function removeListeners(evt, listeners) {
		// Pass through to manipulateListeners
		return this.manipulateListeners(true, evt, listeners);
	};


	proto.manipulateListeners = function manipulateListeners(remove, evt, listeners) {
		var i;
		var value;
		var single = remove ? this.removeListener : this.addListener;
		var multiple = remove ? this.removeListeners : this.addListeners;

		// If evt is an object then pass each of it's properties to this method
		if (typeof evt === 'object' && !(evt instanceof RegExp)) {
			for (i in evt) {
				if (evt.hasOwnProperty(i) && (value = evt[i])) {
					// Pass the single listener straight through to the singular method
					if (typeof value === 'function') {
						single.call(this, i, value);
					}
					else {
						// Otherwise pass back to the multiple function
						multiple.call(this, i, value);
					}
				}
			}
		}
		else {
			
			i = listeners.length;
			while (i--) {
				single.call(this, evt, listeners[i]);
			}
		}

		return this;
	};

	
	proto.removeEvent = function removeEvent(evt) {
		var type = typeof evt;
		var events = this._getEvents();
		var key;

		// Remove different things depending on the state of evt
		if (type === 'string') {
			// Remove all listeners for the specified event
			delete events[evt];
		}
		else if (type === 'object') {
			// Remove all events matching the regex.
			for (key in events) {
				if (events.hasOwnProperty(key) && evt.test(key)) {
					delete events[key];
				}
			}
		}
		else {
			// Remove all listeners in all events
			delete this._events;
		}

		return this;
	};

	proto.removeAllListeners = alias('removeEvent');
	proto.emitEvent = function emitEvent(evt, args) {
		var listeners = this.getListenersAsObject(evt);
		var listener;
		var i;
		var key;
		var response;

		for (key in listeners) {
			if (listeners.hasOwnProperty(key)) {
				i = listeners[key].length;

				while (i--) {
					// If the listener returns true then it shall be removed from the event
					// The function is executed either with a basic call or an apply if there is an args array
					listener = listeners[key][i];

					if (listener.once === true) {
						this.removeListener(evt, listener.listener);
					}

					response = listener.listener.apply(this, args || []);

					if (response === this._getOnceReturnValue()) {
						this.removeListener(evt, listener.listener);
					}
				}
			}
		}

		return this;
	};

	/**
	 * Alias of emitEvent
	 */
	proto.trigger = alias('emitEvent');


	proto.emit = function emit(evt) {
		var args = Array.prototype.slice.call(arguments, 1);
		return this.emitEvent(evt, args);
	};

	proto.setOnceReturnValue = function setOnceReturnValue(value) {
		this._onceReturnValue = value;
		return this;
	};

	proto._getOnceReturnValue = function _getOnceReturnValue() {
		if (this.hasOwnProperty('_onceReturnValue')) {
			return this._onceReturnValue;
		}
		else {
			return true;
		}
	};

	proto._getEvents = function _getEvents() {
		return this._events || (this._events = {});
	};
	EventEmitter.noConflict = function noConflict() {
		exports.EventEmitter = originalGlobalValue;
		return EventEmitter;
	};

	// Expose the class either via AMD, CommonJS or the global object
	if (typeof define === 'function' && define.amd) {
		define('eventEmitter/EventEmitter',[],function () {
			return EventEmitter;
		});
	}
	else if (typeof module === 'object' && module.exports){
		module.exports = EventEmitter;
	}
	else {
		this.EventEmitter = EventEmitter;
	}
}.call(this));


( function( window ) {
var docElem = document.documentElement;
var bind = function() {};
function getIEEvent( obj ) {
  var event = window.event;
  // add event.target
  event.target = event.target || event.srcElement || obj;
  return event;
}

if ( docElem.addEventListener ) {
  bind = function( obj, type, fn ) {
    obj.addEventListener( type, fn, false );
  };
} else if ( docElem.attachEvent ) {
  bind = function( obj, type, fn ) {
    obj[ type + fn ] = fn.handleEvent ?
      function() {
        var event = getIEEvent( obj );
        fn.handleEvent.call( fn, event );
      } :
      function() {
        var event = getIEEvent( obj );
        fn.call( obj, event );
      };
    obj.attachEvent( "on" + type, obj[ type + fn ] );
  };
}

var unbind = function() {};

if ( docElem.removeEventListener ) {
  unbind = function( obj, type, fn ) {
    obj.removeEventListener( type, fn, false );
  };
} else if ( docElem.detachEvent ) {
  unbind = function( obj, type, fn ) {
    obj.detachEvent( "on" + type, obj[ type + fn ] );
    try {
      delete obj[ type + fn ];
    } catch ( err ) {
      // can't delete window object properties
      obj[ type + fn ] = undefined;
    }
  };
}

var eventie = {
  bind: bind,
  unbind: unbind
};

// transport
if ( typeof define === 'function' && define.amd ) {
  // AMD
  define( 'eventie/eventie',eventie );
} else {
  // browser global
  window.eventie = eventie;
}

})( this );


( function( window, factory ) { 'use strict';

  if ( typeof define == 'function' && define.amd ) {
    // AMD
    define( [
      'eventEmitter/EventEmitter',
      'eventie/eventie'
    ], function( EventEmitter, eventie ) {
      return factory( window, EventEmitter, eventie );
    });
  } else if ( typeof module == 'object' && module.exports ) {
    // CommonJS
    module.exports = factory(
      window,
      require('wolfy87-eventemitter'),
      require('eventie')
    );
  } else {
    // browser global
    window.imagesLoaded = factory(
      window,
      window.EventEmitter,
      window.eventie
    );
  }

})( window,

// --------------------------  factory -------------------------- //

function factory( window, EventEmitter, eventie ) {



var $ = window.jQuery;
var console = window.console;

// -------------------------- helpers -------------------------- //

// extend objects
function extend( a, b ) {
  for ( var prop in b ) {
    a[ prop ] = b[ prop ];
  }
  return a;
}

var objToString = Object.prototype.toString;
function isArray( obj ) {
  return objToString.call( obj ) == '[object Array]';
}

// turn element or nodeList into an array
function makeArray( obj ) {
  var ary = [];
  if ( isArray( obj ) ) {
    // use object if already an array
    ary = obj;
  } else if ( typeof obj.length == 'number' ) {
    // convert nodeList to array
    for ( var i=0; i < obj.length; i++ ) {
      ary.push( obj[i] );
    }
  } else {
    // array of single index
    ary.push( obj );
  }
  return ary;
}

  // -------------------------- imagesLoaded -------------------------- //

  /**
   * @param {Array, Element, NodeList, String} elem
   * @param {Object or Function} options - if function, use as callback
   * @param {Function} onAlways - callback function
   */
  function ImagesLoaded( elem, options, onAlways ) {
    // coerce ImagesLoaded() without new, to be new ImagesLoaded()
    if ( !( this instanceof ImagesLoaded ) ) {
      return new ImagesLoaded( elem, options, onAlways );
    }
    // use elem as selector string
    if ( typeof elem == 'string' ) {
      elem = document.querySelectorAll( elem );
    }

    this.elements = makeArray( elem );
    this.options = extend( {}, this.options );

    if ( typeof options == 'function' ) {
      onAlways = options;
    } else {
      extend( this.options, options );
    }

    if ( onAlways ) {
      this.on( 'always', onAlways );
    }

    this.getImages();

    if ( $ ) {
      // add jQuery Deferred object
      this.jqDeferred = new $.Deferred();
    }

    // HACK check async to allow time to bind listeners
    var _this = this;
    setTimeout( function() {
      _this.check();
    });
  }

  ImagesLoaded.prototype = new EventEmitter();

  ImagesLoaded.prototype.options = {};

  ImagesLoaded.prototype.getImages = function() {
    this.images = [];

    // filter & find items if we have an item selector
    for ( var i=0; i < this.elements.length; i++ ) {
      var elem = this.elements[i];
      this.addElementImages( elem );
    }
  };

  /**
   * @param {Node} element
   */
  ImagesLoaded.prototype.addElementImages = function( elem ) {
    // filter siblings
    if ( elem.nodeName == 'IMG' ) {
      this.addImage( elem );
    }
    // get background image on element
    if ( this.options.background === true ) {
      this.addElementBackgroundImages( elem );
    }

    // find children
    // no non-element nodes, #143
    var nodeType = elem.nodeType;
    if ( !nodeType || !elementNodeTypes[ nodeType ] ) {
      return;
    }
    var childImgs = elem.querySelectorAll('img');
    // concat childElems to filterFound array
    for ( var i=0; i < childImgs.length; i++ ) {
      var img = childImgs[i];
      this.addImage( img );
    }

    // get child background images
    if ( typeof this.options.background == 'string' ) {
      var children = elem.querySelectorAll( this.options.background );
      for ( i=0; i < children.length; i++ ) {
        var child = children[i];
        this.addElementBackgroundImages( child );
      }
    }
  };

  var elementNodeTypes = {
    1: true,
    9: true,
    11: true
  };

  ImagesLoaded.prototype.addElementBackgroundImages = function( elem ) {
    var style = getStyle( elem );
    // get url inside url("...")
    var reURL = /url\(['"]*([^'"\)]+)['"]*\)/gi;
    var matches = reURL.exec( style.backgroundImage );
    while ( matches !== null ) {
      var url = matches && matches[1];
      if ( url ) {
        this.addBackground( url, elem );
      }
      matches = reURL.exec( style.backgroundImage );
    }
  };

  // IE8
  var getStyle = window.getComputedStyle || function( elem ) {
    return elem.currentStyle;
  };

  /**
   * @param {Image} img
   */
  ImagesLoaded.prototype.addImage = function( img ) {
    var loadingImage = new LoadingImage( img );
    this.images.push( loadingImage );
  };

  ImagesLoaded.prototype.addBackground = function( url, elem ) {
    var background = new Background( url, elem );
    this.images.push( background );
  };

  ImagesLoaded.prototype.check = function() {

    var _this = this;
    this.progressedCount = 0;
    this.hasAnyBroken = false;
    // complete if no images
    if ( !this.images.length ) {
      this.complete();
      return;
    }

    function onProgress( image, elem, message ) {
      // HACK - Chrome triggers event before object properties have changed. #83
      setTimeout( function() {
        _this.progress( image, elem, message );
      });
    }

    for ( var i=0; i < this.images.length; i++ ) {
      var loadingImage = this.images[i];
      loadingImage.once( 'progress', onProgress );
      loadingImage.check();
    }
  };

  ImagesLoaded.prototype.progress = function( image, elem, message ) {
    this.progressedCount++;
    this.hasAnyBroken = this.hasAnyBroken || !image.isLoaded;
    // progress event
    this.emit( 'progress', this, image, elem );
    if ( this.jqDeferred && this.jqDeferred.notify ) {
      this.jqDeferred.notify( this, image );
    }
    // check if completed
    if ( this.progressedCount == this.images.length ) {
      this.complete();
    }

    if ( this.options.debug && console ) {
      console.log( 'progress: ' + message, image, elem );
    }
  };

  ImagesLoaded.prototype.complete = function() {
    var eventName = this.hasAnyBroken ? 'fail' : 'done';
    this.isComplete = true;
    this.emit( eventName, this );
    this.emit( 'always', this );
    if ( this.jqDeferred ) {
      var jqMethod = this.hasAnyBroken ? 'reject' : 'resolve';
      this.jqDeferred[ jqMethod ]( this );
    }
  };

  // --------------------------  -------------------------- //

  function LoadingImage( img ) {
    this.img = img;
  }

  LoadingImage.prototype = new EventEmitter();

  LoadingImage.prototype.check = function() {
    // If complete is true and browser supports natural sizes,
    // try to check for image status manually.
    var isComplete = this.getIsImageComplete();
    if ( isComplete ) {
      // report based on naturalWidth
      this.confirm( this.img.naturalWidth !== 0, 'naturalWidth' );
      return;
    }

    // If none of the checks above matched, simulate loading on detached element.
    this.proxyImage = new Image();
    eventie.bind( this.proxyImage, 'load', this );
    eventie.bind( this.proxyImage, 'error', this );
    // bind to image as well for Firefox. #191
    eventie.bind( this.img, 'load', this );
    eventie.bind( this.img, 'error', this );
    this.proxyImage.src = this.img.src;
  };

  LoadingImage.prototype.getIsImageComplete = function() {
    return this.img.complete && this.img.naturalWidth !== undefined;
  };

  LoadingImage.prototype.confirm = function( isLoaded, message ) {
    this.isLoaded = isLoaded;
    this.emit( 'progress', this, this.img, message );
  };

  // ----- events ----- //

  // trigger specified handler for event type

  LoadingImage.prototype.handleEvent = function( event ) {
    var method = 'on' + event.type;
    if ( this[ method ] ) {
      this[ method ]( event );
    }
  };

  LoadingImage.prototype.onload = function() {
    this.confirm( true, 'onload' );
    this.unbindEvents();
  };

  LoadingImage.prototype.onerror = function() {
    this.confirm( false, 'onerror' );
    this.unbindEvents();
  };

  LoadingImage.prototype.unbindEvents = function() {
    eventie.unbind( this.proxyImage, 'load', this );
    eventie.unbind( this.proxyImage, 'error', this );
    eventie.unbind( this.img, 'load', this );
    eventie.unbind( this.img, 'error', this );
  };

  // -------------------------- Background -------------------------- //

  function Background( url, element ) {
    this.url = url;
    this.element = element;
    this.img = new Image();
  }

  // inherit LoadingImage prototype
  Background.prototype = new LoadingImage();

  Background.prototype.check = function() {
    eventie.bind( this.img, 'load', this );
    eventie.bind( this.img, 'error', this );
    this.img.src = this.url;
    // check if image is already complete
    var isComplete = this.getIsImageComplete();
    if ( isComplete ) {
      this.confirm( this.img.naturalWidth !== 0, 'naturalWidth' );
      this.unbindEvents();
    }
  };

  Background.prototype.unbindEvents = function() {
    eventie.unbind( this.img, 'load', this );
    eventie.unbind( this.img, 'error', this );
  };

  Background.prototype.confirm = function( isLoaded, message ) {
    this.isLoaded = isLoaded;
    this.emit( 'progress', this, this.element, message );
  };

  // -------------------------- jQuery -------------------------- //

  ImagesLoaded.makeJQueryPlugin = function( jQuery ) {
    jQuery = jQuery || window.jQuery;
    if ( !jQuery ) {
      return;
    }
    // set local variable
    $ = jQuery;
    // $().imagesLoaded()
    $.fn.imagesLoaded = function( options, callback ) {
      var instance = new ImagesLoaded( this, options, callback );
      return instance.jqDeferred.promise( $(this) );
    };
  };
  // try making plugin
  ImagesLoaded.makeJQueryPlugin();

  // --------------------------  -------------------------- //

  return ImagesLoaded;

});