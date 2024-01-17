var INS = {
	init: function() {
		this.Main.init();
		if(window.shop.template == 'index' ){
			this.Home.init();
		}
		if(window.shop.template == 'product' ){
			this.Product.init();
		}
		if(window.shop.template == 'collection' ){
			this.Collection.init();
		}
		this.ComparePD.init();
	},
	resize: function() {

	},
	load: function() {
		this.loadPage.init();
	}
}
$(document).ready(function() {
	INS.init();
})
$(window).on('load resize', function(){
	INS.resize();
})
$(window).load(function(){
	INS.load();
})
/* jQuery for all*/
INS.Main = {
	init: function(){
		this.fastClick();
		this.searchAutoComplete();
		this.scrollToTop();
		this.productItemAddCart();
		this.mobileActions();
		this.replaceImageLoop();
		this.scrollReplaceRegExpImg();
	},
	fastClick: function(){
		$(function() {
			FastClick.attach(document.body);
		});
	},
	searchAutoComplete: function(){
		var $input = $('#searchFRM input[type="text"]');
		$input.bind('keyup change paste propertychange', function() {
			var key = $(this).val(),
					$parent = $(this).parents('.frmSearch'),
					$results = $(this).parents('.frmSearch').find('.ajaxSearchAuto');
			if(key.length > 0 && key !== $(this).attr('data-history')){
				//$(this).attr('data-history', key);
				//var str = '/search?type=product&q=' + key + '&view=auto-complete';
				//$.ajax({
				//	url: str,
				//	type: 'GET',
				//	async: true,
				//	success: function(data){
				//		$results.find('.resultsContent').html(data);
				//	}
				//})
			    var str = '/ajax-san-pham-quick-search?q=' + key + '&view=auto-complete';
				$.ajax({
				    url: str,
				    type: 'GET',
				    async: true,
				    success: function (data) {
				        $results.find('.resultsContent').html(data);
				    }
				})
				$results.fadeIn();
			}
		})
		$('body').click(function(evt) {
			var target = evt.target;
			if (target.id !== 'ajaxSearchResults' && target.id !== 'inputSearchAuto') {
				$("#ajaxSearchResults").hide();
			}
		});
		$('body').on('click', '#searchFRM input[type="text"]', function() {
			if ($(this).is(":focus")) {
				if ($(this).val() != '') {
					$("#ajaxSearchResults").show();
				}
			} else {

			}
		})
	},
	scrollToTop: function() {
		jQuery(window).scroll(function() {
			if ($(this).scrollTop() > 100) {
				$('.tempFixed .itemFixed.backTop').addClass('trans');
			} else {
				$('.tempFixed .itemFixed.backTop').removeClass('trans');
			}
		});

		//Click event to scroll to top
		jQuery('.backTop a').click(function() {
			$('html, body').animate({
				scrollTop: 0
			}, 600);
			return false;
		});
		jQuery('.tempFixed .itemFixed.hotLine label i').click(function() {
			$(this).parents('.hotLine').fadeOut(500)
		});
	},
	productItemAddCart: function(){
		$(document).on('click','.Addcart', function(){
			var qty = 1,
					variantID = $(this).attr('data-variantid'),
					cart = $('.tempFixed .cartFixed'),
					image = $(this).parents('.pdLoopItem').find('.pdLoopImg img').eq(0);
			cart.addClass('loading');
			INS.Main.flyToElement(image,cart);
			INS.Main.ajaxAddCart(qty,variantID);
		})
		$(document).on('click','#quick-view-modal .btn-addcart', function(e){
			e.preventDefault();
			var qty = parseInt($('#quick-view-modal .form-input input[type=number]').val()),
					variantID = $('#quick-view-modal select#p-select').val(),
					cart = $('.tempFixed .cartFixed'),
					image = $(this).parents('#quick-view-modal').find('.p-product-image-feature');
			cart.addClass('loading');
			INS.Main.flyToElement(image,cart);
			INS.Main.ajaxAddCart(qty,variantID);
		})
	},
	flyToElement(flyer, flyingTo) {
		var $func = $(this);
		var divider = 10;
		var flyerClone = $(flyer).clone().css('width','100px');
		$(flyerClone).css({position: 'absolute', top: $(flyer).offset().top + "px", left: $(flyer).offset().left + "px", opacity: 1, 'z-index': 100001});
		$('body').append($(flyerClone));
		var gotoX = $(flyingTo).offset().left + ($(flyingTo).width() / 2) - ($(flyer).width()/divider)/2;
		var gotoY = $(flyingTo).offset().top + ($(flyingTo).height() / 2) - ($(flyer).height()/divider)/2;

		$(flyerClone).animate({
			opacity: 0.5,
			left: gotoX,
			top: gotoY,
			width: 10,
			height: 10
		}, 1000,
													function () {
														$(flyingTo).fadeOut('fast', function () {
															$(flyingTo).fadeIn('fast', function () {
																$(flyerClone).fadeOut('fast', function () {
																	$(flyerClone).remove();
																	flyingTo.removeClass('loading');
																});
															});
														});
													});
	},
	ajaxAddCart: function(qty,id){
		var cartItem = parseInt($('#cartCount').text());
		var params = {
			type: 'POST',
			url: '/cart/add.js',
			data: 'quantity=' + qty + '&id=' + id,
			dataType: 'json',
			success: function(line_item) { 
				$('.cartItemCount').text(cartItem + qty).removeClass('hide');
			},
			error: function(XMLHttpRequest, textStatus) {
				Haravan.onError(XMLHttpRequest, textStatus);
			}
		};
		jQuery.ajax(params);
	},
	replaceImageLoop: function(){
		var imgArr = ['_thumb','_compact','_medium','_large'],
				time = 1500,
				index = 0,
				key = '_icon';
		var timeReplace = setInterval(function(){
			$('.imgLoopItem').each(function(){
				var imgSrc = $(this).attr('src'),
						imgReg = imgSrc.replace(key,imgArr[index]);
				$(this).attr('src',imgReg);
			})
			key = imgArr[index];
			index++;
			if( index >= imgArr.length ){
				$('.imgLoopItem').attr('data-reg',true).css('width','auto');
				clearInterval(timeReplace);
				return;
			}
		},time);
	},
	scrollReplaceRegExpImg: function(){
		var $imgEl = $('.imgLoopItem');
		$(window).scroll(function(){
			var sizeReplace = $('.imgLoopItem[data-reg="false"]').length;
			if(sizeReplace == 0){
				$imgEl.css('width','auto');
				return;
			}
			$imgEl.each(function(index, el){
				var src = $(el).attr('src');
				var regExp = src.replace(/icon|thumb|compact|medium|large/gi, "large");
				$(el).attr({'data-reg': true, 'src': regExp});
			});
		})
	},
	mobileActions: function(){
		$(document).on('click','.btnMBToggleNav, .closeMenuMB, .overlayMenu', function(){
			$('body').toggleClass('openNav');
		})
	}
};
/* jQuery for index*/
INS.Home = {
	init: function(){
		this.owlSliderHome();
		this.renderRelatedViewedHome();
		this.renderTopProduct();
		this.slideBlockHome();
		this.actionMediaScreen();
	},
	owlSliderHome: function(){
		var sliderOWl = jQuery('.sliderWrap').owlCarousel({
			items: 1,
			lazyLoad:true,
			loop: false,
			autoplay: false,
			margin: 0,
			responsiveClass: true,
			paginationSpeed : 800,
			nav : false,
			navText: ['‹' , '›'],
			afterAction: function(){

			}
		});
		var size = $('.subWrap ul.listSub li').length,
				sizeWidth = 100 / size;
		$('.subWrap ul.listSub li').css('width',sizeWidth +'%');
		$('.subWrap ul.listSub li a').on('click', function(){
			$('.subWrap ul.listSub li').removeClass('active');
			$(this).parent().addClass('active');
			var index = $(this).parent().index();
			sliderOWl.trigger('to.owl.carousel', index)
		})
		sliderOWl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
			if (!event.namespace) return;
			var carousel = event.relatedTarget,
					element = event.target,
					current = carousel.current();
			$('.subWrap ul.listSub li').removeClass('active');
			$('.subWrap ul.listSub li').eq(current).addClass('active');
		})
	},
	renderRelatedViewedHome: function(){
		var arrViewed = JSON.parse(localStorage.getItem('relatedViewedArr')),
				$parent = $('#relatedViewed'),
				sel = this,
				size = 0;
		if(arrViewed != null){
			$.each(arrViewed.reverse(), function(i, item) {
				size += 1;
				var handle = item.handleProduct,
						url = '/products/' + handle;
				sel.ajaxGetProductViewed(url);
				if( size == 2 ){
					return false;
				}
			})
			var owl = $parent.find('#pdViewedLisstting');
			setTimeout(function(){
				if(owl.children().size() > 0 ){
					owl.on('initialize.owl.carousel initialized.owl.carousel ' +
								 'initialize.owl.carousel initialize.owl.carousel ' +
								 'resize.owl.carousel resized.owl.carousel ' +
								 'refresh.owl.carousel refreshed.owl.carousel ' +
								 'update.owl.carousel updated.owl.carousel ' +
								 'drag.owl.carousel dragged.owl.carousel ' +
								 'translate.owl.carousel translated.owl.carousel ' +
								 'to.owl.carousel changed.owl.carousel', function(e) {
									 $parent.fadeIn(500);
								 })
					owl.owlCarousel({
						items: 5,
						slideBy: 5,
						loop: false,
						autoplay: false,
						margin: 15,
						responsiveClass: true,
						dots: false,
						nav : true,
						navText: ['‹' , '›'],
						responsive: {
							0: {
								items: 1
							},
							320: {
								items: 2
							},
							480: {
								items: 2
							},
							767: {
								items: 3
							},
							992: {
								items: 5
							},
							1200: {
								items: 5
							}
						}
					});
					owl.find('.owl-prev').addClass('disabled')
					owl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
						if (!event.namespace) return;
						var carousel = event.relatedTarget,
								element = event.target,
								current = carousel.current();
						$('.owl-next', element).toggleClass('disabled', current === carousel.maximum());
						$('.owl-prev', element).toggleClass('disabled', current === carousel.minimum());
					})
					$('#compareProduct .listCpPd').html('');
					INS.ComparePD.getDefaulItem();
					INS.Main.replaceImageLoop();
					INS.Main.scrollReplaceRegExpImg();
				}
			},1500)
		}else{
			$parent.hide();
		}
	},
	ajaxGetProductViewed: function(url){
		var str = url + '?view=related-viewed';
		$.ajax({
			url: str,
			beforeSend: function() {},
			success: function(data) {
				$('#pdViewedLisstting').append(data);
			}
		})
	},
	renderTopProduct: function(){
		if($('#topProducts').size() > 0 ){
			var checkAuto = parseInt($('#topProducts').attr('data-auto'));
			if( checkAuto == 1 ){
				var str = '/collections/all?sort_by=best-selling&view=top';
				$.ajax({
					url: str,
					async:false,
					beforeSend: function() {},
					success: function(data) {
						var parsed = $.parseHTML(data);
						var html = $(parsed).filter('.itemTop[data-show="true"]').clone();
						$('#pdTopLisstting').append(html);
					}
				})
				var owl = $('#pdTopLisstting');
				setTimeout(function(){
					owl.on('initialize.owl.carousel initialized.owl.carousel ' +
								 'initialize.owl.carousel initialize.owl.carousel ' +
								 'resize.owl.carousel resized.owl.carousel ' +
								 'refresh.owl.carousel refreshed.owl.carousel ' +
								 'update.owl.carousel updated.owl.carousel ' +
								 'drag.owl.carousel dragged.owl.carousel ' +
								 'translate.owl.carousel translated.owl.carousel ' +
								 'to.owl.carousel changed.owl.carousel', function(e) {
									 $('#topProducts').fadeIn(500);
								 })
					owl.owlCarousel({
						items: 6,
						loop: false,
						autoplay: false,
						margin: 15,
						responsiveClass: true,
						dots: false,
						nav : true,
						navText: ['‹' , '›'],
						responsive: {
							0: {
								items: 1
							},
							320: {
								items: 2
							},
							480: {
								items: 2
							},
							767: {
								items: 3
							},
							992: {
								items: 5
							},
							1200: {
								items: 6
							}
						}
					});
					owl.find('.owl-prev').addClass('disabled')
					owl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
						if (!event.namespace) return;
						var carousel = event.relatedTarget,
								element = event.target,
								current = carousel.current();
						$('.owl-next', element).toggleClass('disabled', current === carousel.maximum());
						$('.owl-prev', element).toggleClass('disabled', current === carousel.minimum());
					})
					$('#compareProduct .listCpPd').html('');
					INS.ComparePD.getDefaulItem();
				},1000)
			}else{
				var owl = $('#pdTopLisstting');
				owl.on('initialize.owl.carousel initialized.owl.carousel ' +
							 'initialize.owl.carousel initialize.owl.carousel ' +
							 'resize.owl.carousel resized.owl.carousel ' +
							 'refresh.owl.carousel refreshed.owl.carousel ' +
							 'update.owl.carousel updated.owl.carousel ' +
							 'drag.owl.carousel dragged.owl.carousel ' +
							 'translate.owl.carousel translated.owl.carousel ' +
							 'to.owl.carousel changed.owl.carousel', function(e) {
								 $('#topProducts').fadeIn(500);
							 })
				owl.owlCarousel({
					items: 6,
					loop: false,
					autoplay: false,
					margin: 15,
					responsiveClass: true,
					dots: false,
					nav : true,
					navText: ['‹' , '›'],
					responsive: {
						0: {
							items: 1
						},
						320: {
							items: 2
						},
						480: {
							items: 2
						},
						767: {
							items: 3
						},
						992: {
							items: 5
						},
						1200: {
							items: 6
						}
					}
				});
				owl.find('.owl-prev').addClass('disabled')
				owl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
					if (!event.namespace) return;
					var carousel = event.relatedTarget,
							element = event.target,
							current = carousel.current();
					$('.owl-next', element).toggleClass('disabled', current === carousel.maximum());
					$('.owl-prev', element).toggleClass('disabled', current === carousel.minimum());
				})
				$('#compareProduct .listCpPd').html('');
				//INS.ComparePD.getDefaulItem();
			}
		}
	},
	slideBlockHome: function(){
		$('.slidePDHome').each(function(){
			var sizeChild = $(this).children('.productItem').size();
			if(sizeChild > 0 ){
				var owl = $(this);
				owl.parent().hide();
				owl.on('initialize.owl.carousel initialized.owl.carousel ' +
							 'initialize.owl.carousel initialize.owl.carousel ' +
							 'resize.owl.carousel resized.owl.carousel ' +
							 'refresh.owl.carousel refreshed.owl.carousel ' +
							 'update.owl.carousel updated.owl.carousel ' +
							 'drag.owl.carousel dragged.owl.carousel ' +
							 'translate.owl.carousel translated.owl.carousel ' +
							 'to.owl.carousel changed.owl.carousel', function(e) {
								 owl.parent().fadeIn(500);
							 })
				owl.owlCarousel({
					items: 4,
					slideBy: 4,
					loop: false,
					autoplay: false,
					margin: 15,
					responsiveClass: true,
					dots: false,
					nav : true,
					navText: ['‹' , '›'],
					responsive: {
						0: {
							items: 1
						},
						320: {
							items: 2
						},
						480: {
							items: 2
						},
						767: {
							items: 3
						},
						992: {
							items: 4
						},
						1200: {
							items: 4
						}
					}
				});
				owl.find('.owl-prev').addClass('disabled')
				owl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
					if (!event.namespace) return;
					var carousel = event.relatedTarget,
							element = event.target,
							current = carousel.current();
					$('.owl-next', element).toggleClass('disabled', current === carousel.maximum());
					$('.owl-prev', element).toggleClass('disabled', current === carousel.minimum());
				})
			}
		})
		if($('.slideBlogHome').children().size() > 0 ){
			var owl = $('.slideBlogHome');
			owl.owlCarousel({
				items: 3,
				loop: false,
				autoplay: false,
				margin: 15,
				responsiveClass: true,
				dots: false,
				nav : true,
				navText: ['‹' , '›'],
				responsive: {
					0: {
						items: 1
					},
					320: {
						items: 1
					},
					480: {
						items: 1
					},
					767: {
						items: 2
					},
					992: {
						items: 3
					},
					1200: {
						items: 3
					}
				}
			});
			owl.find('.owl-prev').addClass('disabled')
			owl.on('initialized.owl.carousel changed.owl.carousel refreshed.owl.carousel', function (event) {
				if (!event.namespace) return;
				var carousel = event.relatedTarget,
						element = event.target,
						current = carousel.current();
				$('.owl-next', element).toggleClass('disabled', current === carousel.maximum());
				$('.owl-prev', element).toggleClass('disabled', current === carousel.minimum());
			})
		}
	},
	actionMediaScreen: function(){
		$(document).on('click','.openMenuTabs a', function(){
			$(this).parents('.blockTitle').toggleClass('open');
		})
	}
};
/* js load page */
INS.loadPage = {
	init : function(){
		this.pageLoad();
	},
	pageLoad: function(){
		$('.preloader').delay(1000).fadeOut(500);
		//setTimeout(function(){$(window).trigger('resize');},4000)
	}
}
/* js Product page */
INS.Product = {
	init: function(){
		this.addItemRelatedViewedPD();
		this.removeProductCookie();
		this.addFirstProductCompare();
		this.ajaxAddCart();
	},
	addItemRelatedViewedPD: function(){
		var flag = true,
				currentID = $('#currentPd'),
				oldItems = JSON.parse(localStorage.getItem('relatedViewedArr')) || [];
		$.each(oldItems, function(i, item) {
			if (item.idProduct == currentID.val()){
				flag= false;
				return false;
			}else{
				$('.inRelatedItem').each(function(i,v){
					var idTam = $(v).val();
					if (item.idProduct == idTam ){
						//console.log('ID bị trùng: '+idTam);
						flag= false;
						return false;
					}
				})
			}
		});
		if(flag){
			var newItem = {
				'idProduct': currentID.val(),
				'handleProduct' : currentID.attr('data-handle'),
			};
			if (oldItems.length > 5){
				oldItems.shift()
			}
			oldItems.push(newItem);
			localStorage.setItem('relatedViewedArr', JSON.stringify(oldItems));
		}
	},
	removeProductCookie: function(){
		$.each($.cookie(), function(i, item) {
			if(item.indexOf('product') > 0 ){
				INS.ComparePD.removeItemCompare(i);
			}
		})
	},
	addFirstProductCompare: function(){
		setTimeout(function(){
			var $pdInput = $('#pdCompareTemp'),
					id = $pdInput.val(),
					img = $pdInput.data('image'),
					url = $pdInput.data('url'),
					title = $pdInput.data('title');
			INS.ComparePD.addItemCompare(id,url,img,title);
		},1000)
	},
	ajaxAddCart: function(){
		$(document).on('click','.btn-addCart', function(){
			var qty = parseInt($('.wrapBlockInfo .groupQty input').val()),
					variantID = $('#product-select').val(),
					cart = $('.tempFixed .cartFixed'),
					image = $('.wrapperPdImage .featureImg img');
			cart.addClass('loading');
			INS.Main.flyToElement(image,cart);
			INS.Main.ajaxAddCart(qty,variantID);
		})
	}
}
/* Compare Products js*/
INS.ComparePD = {
	init: function(){
		$(document).on('click','.btnCompare', this.toggleItemCompare);
		$(document).on('click','.mainCpPd .toggleButton a', this.toggleSlideCompare );
		$(document).on('click','.removeCPItem', function(){
			INS.ComparePD.removeItemCompare($(this).data('item'))
		});
		this.getDefaulItem();
	},
	toggleItemCompare: function(){
		$(this).toggleClass('checked');
		var id = $(this).data('id'),
				url = $(this).data('compare'),
				image = $(this).parents('.pdLoopImg').find('img').attr('src'),
				title = $(this).parents('.itemLoop').find('.productName').html();
		$('#compareProduct').fadeIn(500);
		INS.ComparePD.checkAddCompareList(id,url,image,title);
	},
	toggleSlideCompare: function(){
		$('#compareProduct .mainCpPd').toggleClass('toggleSlide');
	},
	checkAddCompareList: function(id,url,image,title){
		var sizeCheck = $('#compareProduct .listCpPd .compareItem[data-id="'+id+'"]').length;
		if( sizeCheck > 0 ){
			INS.ComparePD.removeItemCompare(id);
		}else{
			INS.ComparePD.addItemCompare(id,url,image,title);
		}
	},
	removeItemCompare: function(id){
		$('#compareProduct .listCpPd .compareItem[data-id="'+id+'"]').remove();
		$('.btnCompare[data-id="'+id+'"]').removeClass('checked');
		$.removeCookie(id, { path: '/' });
		var $item = $('#compareProduct .listCpPd .compareItem');
		INS.ComparePD.checkShowCompareBox($item);
	},
	checkShowCompareBox: function($item){
		var number = $item.length;
		switch(number) {
			case 0:
				$('#compareProduct').hide();
				$('#compareProduct .mainCpPd .linkToCompare').hide();
				$('#compareProduct .mainCpPd').removeClass('toggleSlide');
				break;
			case 1:
				$('#compareProduct').fadeIn(500);
				$('#compareProduct .mainCpPd .linkToCompare').fadeOut(500);;
				break;
			case 2:
				$('#compareProduct').fadeIn(500);
				$('#compareProduct .mainCpPd .linkToCompare').fadeIn(500);
				break;
			default:

		}
	},
	addItemCompare: function(id,url,image,title){
		var $compare = $('#compareProduct'),
				$wrapcompare = $compare.find('.listCpPd'),
				itemCompare = '';
		itemCompare += '<div class="compareItem" data-id="'+id+'">';
		itemCompare += '<div class="siteItem clearfix">';

		itemCompare += '<div class="imageItem">';
		itemCompare += '<a href="'+url+'"><img src="'+image+'" /></a>';
		itemCompare += '</div>';
		itemCompare += '<div class="detailItem">';
		itemCompare += '<a href="'+url+'">'+title+'</a>';
		itemCompare += '<a class="removeCPItem" href="javascript:void(0)" data-item="'+id+'">Xóa sản phẩm</a>';
		itemCompare += '</div>';

		itemCompare += '</div>';
		itemCompare += '</div>';
		if($wrapcompare.children().size() < 2 ){
			$wrapcompare.append(itemCompare);
		}else{
			var idRemove = $wrapcompare.find('.compareItem:eq(-1)').data('id');
			INS.ComparePD.removeItemCompare(idRemove);
			$wrapcompare.append(itemCompare);
		}
		$.cookie(id, url , { expires: 1, path: '/' });
		var $item = $('#compareProduct .listCpPd .compareItem');
		INS.ComparePD.checkShowCompareBox($item);
	},
	getDefaulItem: function(){
		$.each($.cookie(), function(i, v) {
			var itemURL = v,
					itemID = '';
			if(itemURL.indexOf('product') > 0 ){
				itemID = i;
				$('.btnCompare[data-id="'+itemID+'"]').addClass('checked')
				$.ajax({
					url: itemURL + '.js',
					async: false,
					success: function (product) {
						var id = product.id,
								url = product.url,
								image = product.featured_image,
								title = product.title;
						INS.ComparePD.addItemCompare(id,url,image,title);
					}
				})
			}
		});
	}
};
/* js Collection page */
INS.Collection = {
	init: function(){
		this.toggleFilter();
		this.ajaxClickInputFilter();
		this.toggleNavMobile();
	},
	toggleFilter: function(){
		$(document).on('click', '.toggleFilter a, .overlayFilter', function(){
			$('body').toggleClass('openFilter');
		})
	},
	ajaxClickInputFilter: function(){
		$(document).on('click','.check-box-list li > input',function(){
			jQuery(this).parent().toggleClass('active');
			var _url = INS.Collection.ajaxGetFilterQuery() + '&view=filter&page=1';
			INS.Collection.ajaxRenderHTML(_url);
		})
		$(document).on('click','.content_sortPagiBar.pagiFilter li a', function(e){
			e.preventDefault();
			INS.Collection.ajaxRenderHTML($(this).attr('href'))
		});
	},
	ajaxGetFilterQuery: function(){
		var _query = '', _price = '', _vendor = '', _color = '', _size = '', _id = '';
		_id = $('#coll-handle').val();
		var _str = '/search?q=filter=';
		_query = "("+_id+")";

		jQuery('.filter-price ul.check-box-list li.active').each(function(){
			_price = _price + jQuery(this).find('input').data('price') + '||';
		})
		_price=_price.substring(0,_price.length -2);
		if(_price != ""){
			_price='('+_price+')';
			_query+='&&'+_price;
		}

		jQuery('.filter-brand ul.check-box-list li.active').each(function(){
			_vendor = _vendor + jQuery(this).find('input').data('vendor') + '||';
		})
		_vendor=_vendor.substring(0,_vendor.length -2);
		if(_vendor != ""){
			_vendor='('+_vendor+')';
			_query+='&&'+_vendor;
		}

		jQuery('.filter-color ul.check-box-list li.active').each(function(){
			_color = _color + jQuery(this).find('input').data('color') + '||';
		})
		_color=_color.substring(0,_color.length -2);
		if(_color != ""){
			_color='('+_color+')';
			_query+='&&'+_color;
		}

		jQuery('.filter-size ul.check-box-list li.active').each(function(){
			_size = _size + jQuery(this).find('input').data('size') + '||';
		})
		_size=_size.substring(0,_size.length -2);
		if(_size != ""){
			_size='('+_size+')';
			_query+='&&'+_size;
		}
		_str += encodeURIComponent(_query);
		return _str;
	},
	ajaxRenderHTML: function(str){
		jQuery.ajax({
			url : str,
			async: false,
			success: function(data){
				jQuery("#pd_collection").html(data);
				var parsed = $.parseHTML(data);
				$('.page_head span.countPd').html('(' + $(parsed).filter('#totalPdFilter').val() + ' sản phẩm)');
			}
		});
		if(sessionStorage.page_view == 'view_grid' ){
			$('.filter').removeClass('view_list').addClass('view_grid');
		}else{
			$('.filter').removeClass('view_grid').addClass('view_list');
		}
		INS.Main.replaceImageLoop();
		INS.Main.scrollReplaceRegExpImg();
	},
	toggleNavMobile: function(){
		$(document).on('click','#childNavCatelog .menu-item.head a', function(){
			$(this).parents('#childNavCatelog').fadeOut(500);
		})
	}
}