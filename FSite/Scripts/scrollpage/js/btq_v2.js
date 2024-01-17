(function($) {
    var methods = {
        on: $.fn.on,
        bind: $.fn.bind
    };
    $.each(methods, function(k) {
        $.fn[k] = function() {
            var args = [].slice.call(arguments),
                delay = args.pop(),
                fn = args.pop(),
                timer;
            args.push(function() {
                var self = this,
                    arg = arguments;
                clearTimeout(timer);
                timer = setTimeout(function() {
                    fn.apply(self, [].slice.call(arg));
                }, delay);
            });
            return methods[k].apply(this, isNaN(delay) ? arguments : args);
        };
    });
}(jQuery));



(function($) {
    $.fn.clipPath = function() {

        this.filter('[data-clipPath]').each(function(i) {

            //get IMG attributes
            var maskPath = $(this).attr('data-clipPath');
            var maskSrc = $(this).attr('src');
            var Width = $(this).css('width');
            var Height = $(this).css('height');
            var maskWidth = parseFloat(Width, 10);
            var maskHeight = parseFloat(Height, 10);
            var maskAlt = $(this).attr('alt');
            var maskTitle = $(this).attr('title');
            var uniqueID = i;
            \

            //build SVG from our IMG attributes & path coords.

            var svg = $('<svg version="1.1" xmlns="http://www.w3.org/2000/svg" class="svgMask" width="' +
                Width + '" height="' +
                Height + '" viewBox="0 0 ' +
                maskWidth + ' ' +
                maskHeight + '"><defs><clipPath id="maskID' +
                uniqueID + '"><path d="' +
                maskPath + '"/></clipPath>  </defs><title>' +
                maskTitle + '</title><desc>' +
                maskAlt + '</desc><image clip-path="url(#maskID' +
                uniqueID + ')" width="' +
                Width + '" height="' +
                Height + '" xlink:href="' +
                maskSrc + '" /></svg>');

            //swap original IMG with SVG
            $(this).replaceWith(svg);

            //clean up
            delete maskPath, maskSrc, maskWidth, maskHeight, maskAlt, maskTitle, uniqueID, svg;

        });

        return this;

    };

}(jQuery));

var timex;
var Click;
var News = 0;
var shownews;
var show;
var Menu = 0;
var SubMenu = 0;
var Details = 0;
var doWheel = true;
var doTouch = true;
var Has = true;
var Arrhash;
var windscroll = $(document).scrollTop();
var monthHash = null;
var date = new Date();
var curMonth = date.getMonth();
var curYear = date.getFullYear();
var timer;
var isLoad = 1;

function NavClick() {
    $('.nav-click').bind('click', function() {
        if ($(window).width() <= 1100) {
            if ($(this).hasClass('active')) {
                $('.top').scrollTop(0);
                $('.container').removeClass('wrap');
                $('.nav-click').removeClass('active');
                $('.overlay-menu, .top, .right').removeClass('show');
                $('.right').removeClass('hide');
                $('html, body').removeClass('no-scroll-nav');

            } else {
                $('.top').scrollTop(0);
                $('.container').addClass('wrap');
                $('.nav-click').addClass('active');
                $('.overlay-menu, .top, .right').addClass('show');
                $('html, body').addClass('no-scroll-nav');
            }
        }
        return false;

    });
}




function SlidePicture() {

    /*HOME PAGE*/

    if ($('#home-page').length) {

        $(".content-present h3").lettering('words').children("span").lettering();

        var Time = $('.slider-home').attr('data-time');



        if ($('.bg-home').length <= 1) {
            $('.pagination').css({
                'display': 'none'
            });
            Time = false;
        }

        if ($(window).width() <= 1100) {
            var Mouse = false;
            var Direction = 'horizontal';
            var Class = "s-mobile";
            var Effect = 'slide'
        } else {
            var Mouse = true;
            var Direction = 'vertical';
            var Class = "s-desktop";
            if (isIE9 || isIE10 || isIE11) {
                var Effect = 'slide'
            } else {
                var Effect = 'coverflow'
            }
        }

        var Full = new Swiper('.slide-bg', {
            pagination: '.pagination',
            autoplay: Time,
            speed: 1200,
            slidesPerView: 1,
            loop: true,
            keyboardControl: true,
            direction: Direction,
            paginationClickable: true,
            autoplayDisableOnInteraction: false,
            mousewheelControl: Mouse,
            effect: Effect,
            coverflow: {
                rotate: 50,
                stretch: 100,
                depth: -100,
                modifier: 1,
                slideShadows: false,
            },

            onInit: function(swiper) {
                $('.slider-home').addClass('fadein');
                $('.item-container').addClass(Class);
            },
            onTransitionStart: function(swiper) {
                $('.left-content').removeClass('show-text');
                $('.item-container').removeClass(Class);
            },
            onTransitionEnd: function(swiper) {
                $('.item-container').addClass(Class);
                $('.item-active').find('.left-content').addClass('show-text');
                StopTime();
                addPlay();
            },

        });

        setTimeout(function() {
            Full.once('onInit');
        }, 200);


    }


    if ($('.news-slide').length) {
        $('.news-slide ul').BTQSlider({
            singleItem: true,
            slideSpeed: 800,
            paginationSpeed: 800,
            navigation: true,
            autoPlay: 4000,
            autoHeight: true,
            AutoHeight: true,
        });

    }

    if ($('.video-slide').length) {
        $('.video-slide ul').BTQSlider({
            singleItem: true,
            slideSpeed: 800,
            paginationSpeed: 800,
            navigation: true,
            autoPlay: 5000,
            stopOnHover: true,
        });

    }


    /*ABOUT SERVICE PAGE*/
    if ($('#scroll-page').length) {
        if ($(window).width() > 1100) {
            var AboutSlide = new Swiper('.bg-content', {
                speed: 800,
                paginationClickable: true,
                direction: 'vertical',
                hashnav: true,
                slidesPerView: 1,
                keyboardControl: true,
                mousewheelControl: true,
                simulateTouch: false,
                effect: 'slide',

                onInit: function(swiper) {
                    $('.slider-about').addClass('fadein');
                    $('.item-container').removeClass('ani-text');
                    $('.item-container').eq(swiper.activeIndex).addClass('ani-text');
                    $('.ani-text .content-text p, .ani-text .content-text h2,.ani-text .content-album h2, .ani-text .box-img, .ani-text .box-value h3,.ani-text .box,.ani-text .album-box,.ani-text .member-box').removeClass('flipoutx').addClass('fadeinup');

                    var textH = $('.ani-text').find('.scrollA').innerHeight();
                    if (textH >= $(window).height() - 250) {
                        ScrollNiceA()
                    }

                    $('.ani-text .partner').each(function(i) {
                        var box = $(this);
                        setTimeout(function() {
                            $(box).addClass('fadeinup')
                        }, (i + 1) * 50);
                    });

                    if ($('.detail-recruitment').length) {
                        ScrollNiceA();
                    }

                },
                onTransitionStart: function(swiper) {
                    $('.content-text p, .content-text h2,.content-album h2, .box-img, .box-value h3, .box, .album-box,.member-box, .partner').removeClass('fadeinup').addClass('flipoutx');
                    $('.item-container').removeClass('ani-text');
                    $('.sub-nav li').removeClass('current');
                    ScrollNiceHide();
                },
                onTransitionEnd: function(swiper) {
                    $('.item-container').eq(swiper.activeIndex).addClass('ani-text');
                    $('.ani-text .content-text p, .ani-text  .content-text h2,.ani-text .content-album h2,.ani-text .box-img,.ani-text .box-value h3,.ani-text .box,.ani-text .album-box,.ani-text .member-box').removeClass('flipoutx').addClass('fadeinup');


                    var Index = $('.ani-text').attr('data-hash');
                    $('.sub-nav li a[data-name= "' + Index + '"]').parent().addClass('current');



                    var textH = $('.ani-text').find('.scrollA').innerHeight();
                    if (textH >= $(window).height() - 250) {
                        ScrollNiceA();
                    }

                    $('.ani-text .partner').each(function(i) {
                        var box = $(this);
                        setTimeout(function() {
                            $(box).addClass('fadeinup')
                        }, (i + 1) * 50);
                    });


                    if ($('.detail-recruitment').length) {
                        ScrollNiceD();
                    }

                    if (!$('.sub-nav li:last-child').hasClass('current')) {
                        $('.scroll-down-desktop').addClass('show');
                    } else {
                        $('.scroll-down-desktop').removeClass('show');
                    }
                },
            });

            setTimeout(function() {
                AboutSlide.once('onInit');
            }, 300);
        }


    }


    if ($('.album-box').length) {
        $('.album-box').BTQSlider({
            singleItem: true,
            slideSpeed: 1000,
            paginationSpeed: 1000,
            navigation: true,
            pagination: true,
            //rewindNav: false,
            afterInit: function(elem) {
                var H = $('.album-box .item-box').innerHeight();
                $('.album-box .slide-buttons').css({
                    'top': -(H / 2 + 78)
                });
                //  $(elem).trigger("BTQ.goTo", 1);

            },

            afterAction: function(el) {
                this.$BTQItems.removeClass('select-pic');
                this.$BTQItems.eq(this.currentItem).addClass('select-pic');

            }
        });
    }


    if ($('.video-box').length) {
        $('.video-box').BTQSlider({
            singleItem: true,
            slideSpeed: 1000,
            paginationSpeed: 1000,
            navigation: true,
            pagination: true,
            //rewindNav: false,
            afterInit: function(elem) {
                var H = $('.video-box .vid-box').innerHeight();
                $('.video-box .slide-buttons').css({
                    'top': -(H / 2 + 100)
                });
                // $(elem).trigger("BTQ.goTo", 1);
            },

            afterAction: function(el) {
                this.$BTQItems.removeClass('select-video');
                this.$BTQItems.eq(this.currentItem).addClass('select-video');

            }
        });
    }



    if ($('.member-box').length) {
        $('.member-box').BTQSlider({
            singleItem: true,
            slideSpeed: 1000,
            paginationSpeed: 1000,
            navigation: true,
            pagination: true,
            rewindNav: false,
            afterAction: function(el) {
                this.$BTQItems.removeClass('select-member');
                this.$BTQItems.eq(this.currentItem).addClass('select-member');
            }
        });

    }




}


function StopTime() {
    if (timex > 0) {
        clearTimeout(timex);
        timex = 0;
    }
}


function addPlay() {
    $('.content-present').removeClass('move');
    $('.content-present .arrow').removeClass('show')
    $('.content-present h3').children().removeClass('move');
    $('.text-intro').removeClass('show');
    $('.show-text .content-present').addClass('move');

    var Lengh = $('.show-text .content-present h3').children().length;
    var Time = (200 * Lengh);
    setTimeout(function() {
        $('.show-text .content-present .arrow').addClass('show'), $('.show-text .text-intro').addClass('show')
    }, Time);


    $('.move h3').children().each(function(i) {
        var box = $(this);
        timex = setTimeout(function() {
            $(box).addClass('move')
        }, (i + 1) * 200);
    });



}

function AniText() {
    $('.title-page h1').children().children().each(function(i) {
        var box = $(this);
        setTimeout(function() {
            $(box).addClass('move')
        }, (i + 1) * 100);
    });

}

function NewsLoad(url, ShowDetails) {
    $.ajax({
        url: url,
        cache: false,
        dataType: 'json',
        success: function(data) {
            $(ShowDetails).find('.news-content').html(data.html);
            //$('.bg-picture').css('background-image', 'url(' + data.img + ')');

            $('.share-button').click(function(e){
                e.preventDefault();
                var ur1 = 'https://www.facebook.com/sharer/sharer.php?u='+url;                
                window.open(ur1);
                return false;
            });

            $('.news-text img').addClass('zoom-pic');
            //ZoomPic();

            $('.news-text a, .news-text p a').click(function(e) {
                e.preventDefault();
                var url = $(this).attr('href');
                window.open(url, '_blank');
                return false;
            });

            $('.news-text').imagesLoaded(function() {

                $(ShowDetails).find('.news-content').stop().animate({
                    'opacity': 1
                }, 100, 'linear', function() {
                    if ($(window).width() > 1100) {
                        ScrollNiceC();
                        var Top = $(ShowDetails).find('.link-page.current').offset().top;
                        var H = $(ShowDetails).find('.news-list').offset().top;
                        if (News == 0) {
                            $(ShowDetails).find('.scrollB').stop().animate({
                                scrollTop: Top - H
                            });
                            News = 1;
                        }

                    } else {
                        var Height = $('.colum-box.active').find('.colum-box-news').innerHeight();
                        $('.box-content').css({
                            'height': Height + 100
                        });
                        detectBut();
                    }

                    $('.news-text').addClass('fadein');
                    $('.loadicon').fadeOut(300, 'linear', function() {
                        $('.loadicon').remove();
                    });

                });
            });




        }
    });
}


function NewsProjectLoad(url, ShowDetails) {
    $.ajax({
        url: url,
        cache: false,
        success: function(data) {
            $(ShowDetails).find('.news-content').append(data);

            $('.scrollC').getNiceScroll().remove();
            $('.news-text img').addClass('zoom-pic');
            ZoomPic();

            $('.news-text a, .news-text p a').click(function(e) {
                e.preventDefault();
                var url = $(this).attr('href');
                window.open(url, '_blank');
                return false;
            });


            $('div .newsload').removeClass('newsload');

            $(ShowDetails).find('.colum-box-news-project').addClass('show');

            $(ShowDetails).find('.news-content').stop().animate({
                'opacity': 1
            }, 100, 'linear', function() {
                var Top = $(ShowDetails).find('.news-text').offset().top - 60;
                var Height = $(ShowDetails).find('.news-content').innerHeight();

                if ($(window).width() > 1100) {
                    ScrollNiceC();
                    var Top = $(ShowDetails).find('.link-page.current').offset().top;
                    var H = $(ShowDetails).find('.news-list-project').offset().top;
                    if (News == 0) {
                        $(ShowDetails).find('.scrollG').stop().animate({
                            scrollTop: Top - H
                        });
                        News = 1;
                    }
                } else {
                    $('html, body').animate({
                        scrollTop: Top
                    }, 'slow');
                }

                $('.news-text').addClass('fadein');
                $('.loadicon').fadeOut(300, 'linear', function() {
                    $('.loadicon').remove();
                });

            });


            $('.close').click(function(e) {
                e.preventDefault();
                $(this).parent().addClass('exit');
                var Top = $(this).parent().parent().offset().top;
                $('html, body').scrollTop(Top);
                $(this).parent().parent().find('.link-page.current').removeClass('current');

                $('.exit').find('.news-content').stop().animate({
                    'opacity': 0
                }, 500, 'linear', function() {
                    $('.exit').find('.news-content').children().remove();
                    $('.exit').removeClass('show');
                    $('.colum-box-news-project.exit').removeClass('exit');


                });

                return false;

            });



        }
    });
}

function RecruitmentLoad(url, title) {

    $.ajax({
        url: url,
        cache: false,
        success: function(data) {

            $('.recruitment-content').prepend(data);
            $('.scrollD').css({
                'height': $('.recruitment-content').height() - 40
            });


            $('.detail-recruitment h3').text(title);

            $('.load-page').fadeIn(500, 'linear', function() {
                if ($(window).width() > 1100) {
                    setTimeout(ScrollNiceD, 100);
                    $('.load-page h2').addClass('fadeinup');
                } else {
                    var Top = $('.load-page').offset().top;
                    $('html, body').animate({
                        scrollTop: Top
                    }, 'slow');
                }




                $('.loadicon').fadeOut(300, 'linear', function() {
                    $('.loadicon').remove();
                });
            });




            $('.back').click(function() {
                ScrollNiceHide();

                var tmpurl = $(this).attr('data-href');
                var tmptitle = $(this).attr('data-title');
                var tmpkeyword = $(this).attr('data-keyword');
                var tmpdescription = $(this).attr('data-description');
                var tmpdataname = $(this).attr('data-name');
                changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);

                $('.load-page').fadeOut(500, 'linear', function() {
                    $('.load-page h2').removeClass('fadeinup');
                    $('.detail-recruitment').remove();
                    $('.list-career').fadeIn(500, 'linear', function() {
                        if ($(window).width() > 1100) {
                            setTimeout(ScrollNiceA, 100);
                        } else {
                            var Top = $('.list-career').offset().top;
                            $('html, body').animate({
                                scrollTop: Top
                            }, 'slow');
                        }
                    });

                });

                return false;

            });



        }

    });

}

function VideoLoad(idx) {
    $.ajax({
        url: idx,
        cache: false,
        success: function(data) {
            $('.allvideo').append(data);
            var ThisVideo = document.getElementById("view-video");

            function playVid() {
                ThisVideo.play();
            }

            function pauseVid() {
                ThisVideo.pause();
            }


            if ($(window).width() > 1100) {
                var RatioV = 1080 / 1920;
                var videoH = $(window).height() - 160;
                var videoW = videoH / RatioV;
                $('.video-wrap').css({
                    'width': videoW,
                    'height': videoH,
                    'margin-left': -videoW / 2,
                    'margin-top': -videoH / 2 + 50
                });
            } else {

                $('.video-wrap').css({
                    'width': '100%',
                    'height': '100%',
                    'margin': 0
                });

                $("#view-video").on('click touchstart', function() {
                    if (this.paused) {
                        this.play();
                    }
                });

                setTimeout(function() {
                    $("#view-video").trigger('touchstart');
                }, 300);
            }

            $('.loadicon').fadeOut(300, 'linear', function() {
                playVid();
                $('.loadicon').remove();
            });


            var length = $('#view-video').length;
            $('.close-video').click(function() {

                if ($('#home-page').length) {
                    var Full = $('.slide-bg')[0].swiper;
                    Full.startAutoplay();
                }


                if (length != 0) {
                    pauseVid();
                }

                $('.video-list, .video-skin').fadeOut(500, 'linear', function() {
                    $('.allvideo').removeClass('show');

                    if ($('.library-load').length) {
                        if ($(window).width() > 1100) {
                            $('.library-center').trigger('BTQ.play', 5000);
                        }
                    }
                    $('.overlay-video').fadeOut(500, 'linear', function() {
                        $('.allvideo').css({
                            'display': 'none'
                        });
                        $('.close-video').fadeOut(300, 'linear', function() {
                            $('.allvideo .video-list').remove();
                        });

                    });

                    $('html, body').removeClass('no-scroll');

                    if ($('.to-scrollV').length) {
                        var top = $('.to-scrollV').offset().top;
                        $('.to-scrollV').removeClass('to-scrollV');

                        if ($(window).width() < 1100) {
                            $('html, body').scrollTop(top - 60);
                        }
                    }



                });

            });
        }


    });
}




function AlbumLoad(url, num) {
    $.ajax({
        url: url,
        cache: false,
        success: function(data) {
            $('.all-album').append(data);

            $('.album-pic-center').css({
                'height': $(window).height()
            });

            $(".pic-name > h3").lettering('words').children("span").lettering().children("span").lettering();


            $('.album-center').BTQSlider({
                singleItem: true,
                pagination: false,
                rewindNav: false,
                lazyLoad: true,
                mouseDrag: true,
                slideSpeed: 600,
                paginationSpeed: 600,
                afterAction: function(el) {
                    this.$BTQItems.removeClass('show-active');
                    this.$BTQItems.eq(this.currentItem).addClass('show-active');
                    ButCheck();
                    addText();
                }
            });



            if (TouchLenght == false || !isTouchDevice) {
                $('.album-center').on('mousewheel', '.slide-wrapper ', function(e) {
                    if (e.deltaY > 0) {
                        if (!doWheel) {
                            return;
                        }
                        doWheel = false;
                        $('.album-center').trigger('BTQ.prev');
                        setTimeout(turnWheelTouch, 500);
                    } else {
                        if (!doWheel) {
                            return;
                        }
                        doWheel = false;
                        $('.album-center').trigger('BTQ.next');
                        setTimeout(turnWheelTouch, 500);

                    }
                    e.preventDefault();
                });
            }

            function ButCheck() {

                if ($('.show-active:first-child').hasClass('show-active')) {
                    $('.prev-pic').addClass('disable');
                } else {
                    $('.prev-pic').removeClass('disable');
                }
                if ($('.show-active:last-child').hasClass('show-active')) {
                    $('.next-pic').addClass('disable');
                } else {
                    $('.next-pic').removeClass('disable');
                }
            }

            function addText() {
                clearTimeout(timex);
                $('.pic-name').each(function(index, element) {
                    var Text = $(this).find('h3').text();

                    if (Text.length > 1) {
                        $(this).find('h3').css({
                            'display': 'inline-block'
                        });

                    } else {
                        $(this).find('h3').css({
                            'display': 'none'
                        });
                    }

                });



                $('.pic-name').removeClass('move');
                $('.pic-name h3').children().children().removeClass('move');
                $('.slide-item.show-active').find('.pic-name').addClass('move');
                $('.move h3').children().children().each(function(i) {
                    var box = $(this);
                    var timex = setTimeout(function() {
                        $(box).addClass('move')
                    }, (i + 1) * 50);
                });
            }

            $('.next-pic').click(function() {
                $('.album-center').trigger('BTQ.next');
            });

            $('.prev-pic').click(function() {
                $('.album-center').trigger('BTQ.prev');
            });

            $('.all-album').stop().animate({
                'opacity': 1
            }, 100, 'linear', function() {
                if ($('.album-pic-center').length > 1) {
                    $('.slide-pic-nav').css({
                        'display': 'block'
                    });
                }
            });

            $('.album-load').fadeIn(800, 'linear', function() {
                $('.loadicon').fadeOut(300, 'linear', function() {
                    $('.loadicon').remove();
                });
            });


            $('.album-pic-center img').addClass('zoom-pic');
            ZoomPic();


            $('.close-album').click(function() {

                $('.all-album').fadeOut(500, 'linear', function() {
                    $('.album-load').remove();
                });

                $('.overlay-album').animate({
                    'height': '0%'
                }, 600, 'easeOutExpo', function() {
                    $('.overlay-album').css({
                        'display': 'none'
                    });
                });

                $('html, body').removeClass('no-scroll');

                if ($('.to-scrollAB').length) {
                    var top = $('.to-scrollAB').offset().top;
                    $('to-scrollAB').removeClass('to-scrollAB');

                    if ($(window).width() < 1100) {
                        $('html, body').scrollTop(top - 60);
                    }
                }


                return false;
            });

        }
    });
}



function HouseLoad(url) {
	console.log(url);
    $.ajax({
        url: url,
        cache: false,
        success: function(data) {
            $('.typical-content').append(data);
            if ($(window).width() > 1100) {
                if ($(window).height() / $(window).width() < 0.55) {
                    $('.house-pic').css({
                        'height': $(window).height() - 220,
                        'margin-top': $(window).height() / 2 - ($(window).height() - 220) / 2 - 45
                    });
                } else {
                    $('.house-pic').css({
                        'height': $(window).height() - 300,
                        'margin-top': $(window).height() / 2 - ($(window).height() - 300) / 2 - 45
                    });
                }
                var Style = "fade";
                var Drag = false;

            } else {
                detectBut();
                $('.house-pic').css({
                    'height': 'auto',
                    'margin-top': 0
                });
                var Style = false;
                var Drag = true;
            }


            $('.description').each(function(index, element) {
                var Text = $(this).find('p').text();
                if (Text.length > 0) {
                    console.log(Text)
                    $(this).find('p').css({
                        'display': 'block'
                    });
                } else {
                    $(this).find('p').css({
                        'display': 'none'
                    });
                }
            });




            if ($('.house-details').length <= 1) {
                $('.nextprev').css({
                    'display': 'none'
                });
            } else {
                $('.nextprev').css({
                    'display': 'block'
                });
            }

            $('.content-house-slide').BTQSlider({
                singleItem: true,
                slideSpeed: 800,
                paginationSpeed: 800,
                navigation: false,
                pagination: true,
                mouseDrag: Drag,
                rewindNav: false,
                transitionStyle: Style,

                afterAction: function(el) {
                    $('.house-pic').removeClass('fadeinup').addClass('flipoutx')
                    this.$BTQItems.removeClass('select-house');
                    this.$BTQItems.eq(this.currentItem).addClass('select-house');
                    if ($(window).width() > 1100) {
                        $('.select-house').find('.house-pic').removeClass('flipoutx').addClass('fadeinup');
                        $('.select-house').addClass('s-desktop');
                    } else {
                        $('.select-house').addClass('s-mobile');
                    }
                    Check();
                }
            });

            $('.next').click(function() {
                $('.content-house-slide').trigger('BTQ.next');
            });

            $('.prev').click(function() {
                $('.content-house-slide').trigger('BTQ.prev');
            });

            function Check() {

                $('.content-house-slide').on('mousewheel', '.slide-wrapper ', function(e) {
                    if (e.deltaY > 0) {
                        if (!doWheel) {
                            return;
                        }
                        doWheel = false;
                        if ($(window).width() > 1100) {
                            $('.content-house-slide').trigger('BTQ.prev');
                        }
                        setTimeout(turnWheelTouch, 500);
                    } else {
                        if (!doWheel) {
                            return;
                        }
                        doWheel = false;
                        if ($(window).width() > 1100) {
                            $('.content-house-slide').trigger('BTQ.next');
                        }
                        setTimeout(turnWheelTouch, 500);
                    }
                    e.preventDefault();
                });


                if ($('.select-house:first-child').hasClass('select-house')) {
                    $('.prev').addClass('disable');
                } else {
                    $('.prev').removeClass('disable');
                }
                if ($('.select-house:last-child').hasClass('select-house')) {
                    $('.next').addClass('disable');
                } else {
                    $('.next').removeClass('disable');
                }


            }


            $('.typical-content').stop().animate({
                'opacity': 1
            }, 100, 'linear', function() {

                $('.loadicon').fadeOut(300, 'linear', function() {
                    $('.loadicon').remove();
                });
            });



            $('.zoom').click(function() {
                $('html, body').addClass('no-scroll');
                $(this).parent().addClass('to-scroll');
                if (!$('.loadicon').length) {
                    $('body').append('<div class="loadicon" style="display:block"></div>');
                }
                $('.all-pics').css({
                    'display': 'block'
                });
                if ($('.all-pics .full').length) {
                    $('.full').css({
                        'display': 'block'
                    });
                } else {
                    $('.all-pics').append('<div class="full"  style="display:block"></div>');
                }
                $('.overlay-dark').fadeIn(300, 'linear');

                var activePicLarge = $(this).parent().find('img').attr("src") || $(this).parent().find('img').attr("data-src");
                var newActive = activePicLarge.replace("_s", "_l");
                $('.all-pics').find('.full').append('<img src ="' + (newActive) + '" alt="pic" />');

                if (!$('body .close-pics').length) {
                    $('body').append('<div class="close-pics"></div>');
                }
                if (!$('.all-pics .close-pics-small').length) {
                    $('.all-pics').append('<div class="close-pics-small"></div>');
                }

                $('.all-pics img').load(function() {
                    $('.all-pics').addClass('show');
                    if (TouchLenght == false || !isTouchDevice) {
                        $('.full').addClass('dragscroll');
                        $('.dragscroll').draptouch();
                    } else {
                        $('.full').addClass('pinch-zoom');
                        $('.pinch-zoom').each(function() {
                            new Pic.PinchZoom($(this), {});
                        });
                    }

                    if ($('.full img').length > 1) {
                        $('.full img').last().remove()
                    }

                    $('.loadicon').fadeOut(400, 'linear', function() {
                        if (TouchLenght == false || !isTouchDevice) {
                            detectMargin();
                        }
                        $('.full img').addClass('fadein');
                        $('.loadicon').remove();
                    });

                });

                $('.close-pics, .close-pics-small').click(function() {
                    $('.loadicon').remove();
                    $('.full, .close-pics, .close-pics-small').fadeOut(300, 'linear');
                    $('.overlay-dark').fadeOut(300, 'linear', function() {
                        $('.close-pics, .close-pics-small').remove();
                        $('.all-pics .full, .all-pics .text-length, .all-pics .pinch-zoom-container').remove();
                        $('.all-pics').css({
                            'display': 'none'
                        }).removeClass('show');
                        $('html, body').removeClass('no-scroll');

                        if ($('.to-scroll').length) {
                            var top = $('.to-scroll').offset().top;
                            $('.to-scroll').removeClass('to-scroll');

                            if ($(window).width() < 1100) {
                                $('html, body').scrollTop(top - 60);
                            }
                        }

                    });
                });
                return false;
            });


        }

    });
}




function FocusText() {
    var txtholder = 'HỌ VÀ TÊN (*) ĐIỆN THOẠI (*) EMAIL (*) Họ và Tên (*)  Địa chỉ (*) Địa Chỉ (*) Điện Thoại (*) Điện thoại (*) Email (*) Full name (*) Full Name (*) Address (*) Phone (*)  Điện thoại di động (*)';
    var txtRep = "";
    $('input').focus(function() {
        txtRep = $(this).val();
        if (txtholder.indexOf(txtRep) >= 0) {
            $(this).val("");
        }
    });
    $('input').focusout(function() {
        if ($(this).val() == "") $(this).val(txtRep);
    });
    var cur_text = "";
    $('textarea').focus(function() {
        cur_text = $(this).val();
        if (cur_text == 'Nội dung (*)');
        $(this).val('')
    }).focusout(function() {
        if ($(this).val() == "")
            $(this).val(cur_text)
    });


}

function ScrollHoz() {
    var Scroll = $('.news-list, .sub-news, .content-table, .sub-menu, .sub-nav-typical');
    if ($(window).width() <= 1100) {
        if ($('#news-page').length) {
            $('.scrollB').each(function(index, element) {
                var Thumb = $(this).children().length;
                var Width = $(this).children().width() + 5;
                $(this).width(Thumb * Width);
            });
        }


        $(Scroll).css({
            'overflow-x': 'scroll',
            'overflow-y': 'hidden',
            '-ms-touch-action': 'auto',
            '-ms-overflow-style': 'none',
            'overflow': ' -moz-scrollbars-none',
            '-webkit-overflow-scrolling': 'touch'
        });
        $(Scroll).animate({
            scrollLeft: "0px"
        });
        if (TouchLenght == false || !isTouchDevice) {
            if ($(window).width() <= 1100) {
                $(Scroll).mousewheel(function(e, delta) {
                    e.preventDefault();
                    if ($(window).width() <= 1100) {
                        this.scrollLeft -= (delta * 40);
                    }
                });

                $(Scroll).addClass('dragscroll');
                $('.dragscroll').draptouch();
            }
        }

    }

}

function ScrollNiceA() {
    if ($(window).width() <= 1100) {
        $('.scrollA').getNiceScroll().remove();
        $('.scrollA').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.ani-text .scrollA').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.ani-text .scrollA').getNiceScroll().show();
        $('.ani-text .scrollA').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: true,
            cursoropacitymin: 1
        });
        $('.ani-text .scrollA').scrollTop(0);
    }

}

function ScrollNiceB() {
    if ($(window).width() <= 1100) {
        ScrollHoz();
    } else {
        $('.scrollB').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollB').getNiceScroll().show();
        $('.scrollB').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: false,
            background: "rgba(0,0,0,0.3)",
            cursoropacitymin: 1
        });
        $('.scrollB').scrollTop(0);
    }
}

function ScrollNiceC() {
    if ($(window).width() <= 1100) {
        $('.scrollC').getNiceScroll().remove();
        $('.scrollC').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollC').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollC').getNiceScroll().show();
        $('.scrollC').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: false,
            cursoropacitymin: 1
        });
        $('.scrollC').scrollTop(0);
    }
}

function ScrollNiceD() {
    if ($(window).width() <= 1100) {
        $('.scrollD').getNiceScroll().remove();
        $('.scrollD').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollD').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollD').getNiceScroll().show();
        $('.scrollD').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: true,
            cursorcolor: "#bc902d",
            cursoropacitymin: 0.5
        });
        $('.scrollD').scrollTop(0);
    }

}

function ScrollNiceE() {
    if ($(window).width() <= 1100) {
        $('.scrollE').getNiceScroll().remove();
        $('.scrollE').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollE').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollE').getNiceScroll().show();
        $('.scrollE').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: true,
            cursoropacitymin: 1
        });
        $('.scrollE').scrollTop(0);
    }

}


function ScrollNiceF() {
    if ($(window).width() <= 1100) {
        $('.scrollF').getNiceScroll().remove();
        $('.scrollF').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollF').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollF').getNiceScroll().show();
        $('.scrollF').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: true,
            background: "rgba(0,0,0,0.3)",
            cursoropacitymin: 1
        });
        $('.scrollF').scrollTop(0);
    }

}

function ScrollNiceH() {
    if ($(window).width() <= 1100) {
        $('.scrollH').getNiceScroll().remove();
        $('.scrollH').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.active .scrollH').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.active .scrollH').getNiceScroll().show();
        $('.active .scrollH').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: true,
            cursorcolor: "#bc902d",
            cursoropacitymin: 0.5
        });
        $('.active .scrollH').scrollTop(0);
    }

}

function ScrollNiceG() {
    if ($(window).width() <= 1100) {
        $('.scrollG').getNiceScroll().remove();
        $('.scrollG').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollG').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollG').getNiceScroll().show();
        $('.scrollG').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: false,
            background: "rgba(0,0,0,0.3)",
            cursoropacitymin: 1
        });
        $('.scrollG').scrollTop(0);
    }
}

function ScrollNiceJ() {
    if ($(window).width() <= 1100) {
        $('.scrollJ').getNiceScroll().remove();
        $('.scrollJ').css({
            'overflow-x': 'visible',
            'overflow-y': 'visible'
        });
    } else {
        $('.scrollJ').css({
            'overflow-x': 'hidden',
            'overflow-y': 'hidden'
        });
        $('.scrollJ').getNiceScroll().show();
        $('.scrollJ').niceScroll({
            touchbehavior: true,
            horizrailenabled: false,
            cursordragontouch: true,
            grabcursorenabled: false,
            background: "rgba(0,0,0,0.3)",
            cursoropacitymin: 1
        });
        $('.scrollJ').scrollTop(0);
    }
}

function ScrollNiceHide() {
    $('.scrollA, .scrollB, .scrollC, .scrollD, .scrollF, .scrollH, .scrollG, .scrollJ').getNiceScroll().remove();
}


function LinkPage() {
    $('a.link-load, a.link-home, a.go-page, .news-home a, .content-present a,  a.go-back, a.house,.go-home, .link-icon a, .project-view').click(function(e) {
        e.preventDefault();
        $('.overlay-menu').trigger('click');
        $('.close-video, .close-box').trigger('click');
        $('.top').removeClass('show-in');
        $('.right, .nav-click').removeClass('fadein').addClass('fadeout');
        $('.bottom, .go-top, .hot-news, #present, .map, .scroll-down-desktop').removeClass('fadeinup').removeClass('show').addClass('fadeout');

        if ($('#map-canvas').children().length) {
            $('.close-map').trigger('click');
        }

        $('html, body').addClass('no-scroll');
        linkLocation = $(this).attr("href");
        $('.container').animate({
            'opacity': 0
        }, 300, 'linear', function() {
            window.location = linkLocation;
        });

        return false;
    });

}


function ContentLoad() {
    ResizeWindows();
    detectHeight();
    LinkPage();
    FocusText();
    NavClick();
    SlidePicture();
    Option();
    ScrollHoz();


    /*var IDPage = $('body').attr('id');
	  $('.nav li a[data-name= "' + IDPage + '"]').parent().addClass('current');
	  if($('#project-details-page').length){
		  $('.nav li a[data-name = "projects-page"]').parent().addClass('active');
	  }*/

   

    $('html, body').removeClass('no-scroll');

  
    $('.top').addClass('show-in');
    setTimeout(function() {
        $('.right, .nav-click').addClass('fadein');
        AniText();
    }, 300);
    setTimeout(function() {
        $('.hot-news').addClass('fadeinup');
    }, 500);
    setTimeout(function() {
        $('.bottom, .logo-project').addClass('fadeinup');
    }, 700);
    setTimeout(function() {
        $('#present, .map, .next-prev, .go-back').addClass('fadeinup');
    }, 1000);
    setTimeout(function() {
        if (!$('.sub-nav li:last-child').hasClass('current')) {
            $('.scroll-down-desktop').addClass('show');
        }
    }, 1000);


    //HOME PAGE//
    if ($('#home-page').length) {
        $('.link-home').addClass('current');


        //var PageActive = window.location.hash;
        //PageActive = PageActive.slice(1);	

        if ($(window).width() > 1100) {
            if ($('.popup-pics img').length > 0) {
                //if ($('.popup-pics img').length > 0 && PageActive == 'first-time') {   
                $('.overlay-dark').fadeIn(500, 'linear', function() {
                    $('.popup-pics').fadeIn(500, 'linear');
                    $('body').removeClass('first-time');
                    //PageActive = '';
                });

                $('.close-popup, .overlay-dark').click(function() {
                    $('.popup-pics, .overlay-dark').fadeOut(500, 'linear', function() {});
                    return false;
                });
            }
        }

        $('.scroll-down-desktop').click(function(e) {
            e.preventDefault();
            var Full = $('.slide-bg')[0].swiper;
            Full.slideNext();

        });

    }


    //ABOUT SERVICES PAGE//
    if ($('#scroll-page').length) {


        $('.sub-nav').addClass('show');

        $('.sub-nav li').click(function(e) {
            e.preventDefault();
            $('.sub-nav li').removeClass('current');
            $(this).addClass('current');
            var Name = $(this).find('a').attr('data-name');
            // window.location.hash = Name;

            if ($(window).width() > 1100) {
                var AboutSlide = $('.bg-content')[0].swiper;
                var Num = $(".item-container[data-hash='" + Name + "']").index();
                AboutSlide.slideTo(Num, 1000, true);
            }
            return false;
        });



        $('.link-career').click(function(e) {
            e.preventDefault();
            $('body').append('<div class="loadicon" style="display:block"></div>');
            ScrollNiceHide();
            var url = $(this).attr('href');

            var tmpurl = $(this).attr('href');
            var tmptitle = $(this).attr('data-title');
            var tmpkeyword = $(this).attr('data-keyword');
            var tmpdescription = $(this).attr('data-description');
            var tmpdataname = $(this).attr('data-name');
            changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);

            var title = $(this).find('h3').text();
            $('.list-career').fadeOut(500, 'linear', function() {
                RecruitmentLoad(url, title);
            });
            return false;
        });


        $('.scroll-down-desktop').click(function(e) {
            e.preventDefault();
            $('.sub-nav li.current').next().find('a').trigger('click');
        });

        if ($(window).width() > 1100) {
            /*if(window.location.hash){
					  LocationHash();*/
            if ($('.sub-nav li.current').length) {
                $('.sub-nav li.current').trigger('click');
            } else {
                $('.sub-nav li:first-child').trigger('click');
            }
        } else {
            $('.slider-about').addClass('fadein');
        }

        if ($('.link-career.current').length) {
            setTimeout(function() {
                $('.link-career.current').trigger('click');
            }, 50);
        }

    }

    //PROJECTS PAGE//
    if ($('#projects-page').length) {
        $('.project-content').addClass('active');
        $('.sub-menu').addClass('showup');
        setTimeout(function() {
            ScrollNiceF()
        }, 500);
    }

    //PROJECT DETAILS PAGE//
    if ($('#project-details-page').length) {
        if ($('.sub-nav-typical li').length <= 1) {
            $('.sub-nav-typical').css({
                'display': 'none'
            });
        }

        $('.nav li.current').addClass('active');

        $('.wrap-content').imagesLoaded(function() {
            $('.wrap-content').isotope({
                itemSelector: '.small-facilities',
                percentPosition: true,
            });
        });

        $('.project-element').addClass('show');
        $('.sub-menu').addClass('showup');
        $('.pic-project:not(.small-library, .small-facilities) img, .bg-cover img').addClass('zoom-pic');
        ZoomPic();

        $('.link-page a').click(function(e) {
            e.preventDefault();
            if (!$('.loadicon').length) {
                $('body').append('<div class="loadicon" style="display:block"></div>');
            }

            var ShowDetails = $(this).parent().parent().parent().parent();
            $(this).parent().parent().find('.link-page').removeClass('current');
            $(this).parent().addClass('current');
            var Name = $(this).attr('data-name');
            //window.location.hash = Name; 
            var url = $(this).attr('data-href');
            $(ShowDetails).find('.news-content').addClass('newsload');

            $(ShowDetails).find('.news-content').stop().animate({
                'opacity': 0
            }, 600, 'linear', function() {

                if ($('.newsload').children().length) {
                    $('.scrollC').getNiceScroll().remove();
                    $('.newsload').children().remove();
                    $('div .newsload').removeClass('newsload');
                }

                NewsProjectLoad(url, ShowDetails);

            });


            return false;
        });



        $('.sub-menu li a').click(function(e) {
            e.preventDefault();
            ScrollNiceHide();
            if ($(window).width() > 1100) {
                var allItem = $('.colum-project').length;
                var widthItem = $('.colum-project').width();
                $('.box-content-project').width(allItem * widthItem);

                $('.sub-menu li').removeClass('current');
                $('.colum-project').removeClass('active');
                $(this).parent().addClass('current');

                var Openpage = $(this).attr('data-name');
                //window.location.hash = Openpage;  
                var tmpurl = $(this).attr('href');
                var tmptitle = $(this).attr('data-title');
                var tmpkeyword = $(this).attr('data-keyword');
                var tmpdescription = $(this).attr('data-description');
                var tmpdataname = $(this).attr('data-name');
                //changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);

                var XCurrent = $('.box-content-project').offset().left;
                var XItem = $('.box-content-project .colum-project[data-hash= "' + Openpage + '"]').offset().left;
                $('.colum-project[data-hash= "' + Openpage + '"]').addClass('active');

                var content = $('.colum-project.active').find('.news-content').children();

                if (content.length <= 0) {
                    $('.colum-project.active').find('.link-page:first-child a').trigger('click');
                }
                detectBut();

                $('.box-content-project').stop().animate({
                    'left': XCurrent - XItem
                }, 800, 'easeInOutExpo', function() {
                    $('.colum-project.active ').find('.content-pro-text').addClass('fadein');
                    $('.colum-project.active ').find('.bg-cover').addClass('fadeinup');
                    $('.colum-project.active').find('.colum-box-news-project').addClass('fadeinup');
                    $('.colum-project.active').find('.news-list-project').addClass('fadein');


                    $('.colum-project.active .pic-project').each(function(i) {
                        var box = $(this);
                        setTimeout(function() {
                            $(box).addClass('fadeinup')
                        }, (i + 1) * 200);
                    });

                    $('.colum-project.active .brochure-box').each(function(i) {
                        var box = $(this);
                        setTimeout(function() {
                            $(box).addClass('fadeinup')
                        }, (i + 1) * 200);
                    });

                    if ($('.colum-project.active .scrollC').length) {
                        setTimeout(function() {
                            ScrollNiceC();
                        }, 150);
                    }
                    if ($('.colum-project.active .scrollH').length) {
                        setTimeout(function() {
                            ScrollNiceH();
                        }, 150);
                    }
                    if ($('.colum-project.active .scrollG').length) {
                        setTimeout(function() {
                            ScrollNiceG();
                        }, 150);
                    }
                    if ($('.colum-project.active .scrollJ').length) {
                        setTimeout(function() {
                            ScrollNiceJ();
                        }, 150);
                    }

                    if ($(window).width() > 1100) {
                        $(".pic-project:not(.small-center, .small-library, .small-facilities) img, .bg-cover img").imagePanning();
                    }

                });

            }

            return false;
        });

        $('.prevslide').click(function(e) {
            e.preventDefault();
            if (!doWheel) {
                return;
            }

            doWheel = false;
            $('.sub-menu li.current').prev().find('a').trigger('click');
            setTimeout(turnWheelTouch, 800);
        });

        $('.nextslide').click(function(e) {
            e.preventDefault();
            if (!doWheel) {
                return;
            }

            doWheel = false;
            $('.sub-menu li.current').next().find('a').trigger('click');
            setTimeout(turnWheelTouch, 800);
        });


        $('.sub-nav-typical li').click(function(e) {
            e.preventDefault();

            $('.sub-nav-typical li').removeClass('current');
            $(this).addClass('current');
            var url = $(this).find('a').attr('data-href');
            if (!$('.loadicon').length) {
                $('body').append('<div class="loadicon" style="display:block"></div>');
            }

            $('.typical-content').stop().animate({
                'opacity': 0
            }, 600, 'linear', function() {
                if ($('.content-house').length) {
                    $('.content-house').remove();
                }

                HouseLoad(url);
            });

            return false;
        });

        $('.sub-nav-typical li:first-child').trigger('click');


        if ($(window).width() > 1100) {
            //$('.link-page:first-child a').trigger('click');
            /*if(window.location.hash){
					  LocationHash();*/
            if ($('.sub-menu li.current').length) {
                $('.sub-menu li.current a').trigger('click');
            } else {
                $('.sub-menu li:first-child a').trigger('click');
            }

        }


    }



    //NEWS PAGE//
    if ($('#news-page').length) {

        $('.sub-news').addClass('show');


        $('.link-page a').click(function(e) {
            e.preventDefault();
            if (!$('.loadicon').length) {
                $('body').append('<div class="loadicon" style="display:block"></div>');
            }
            var ShowDetails = $(this).parent().parent().parent().parent();
            $(this).parent().parent().find('.link-page').removeClass('current');
            $(this).parent().addClass('current');
            var Name = $(this).attr('data-name');

            //window.location.hash = Name; 
            var tmpurl = $(this).attr('href');
            var tmptitle = $(this).attr('data-title');
            var tmpkeyword = $(this).attr('data-keyword');
            var tmpdescription = $(this).attr('data-description');
            var tmpdataname = $(this).attr('data-name');
            changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);

            var url = $(this).attr('href');

            $(ShowDetails).find('.news-content').addClass('newsload');

            $(ShowDetails).find('.news-content').stop().animate({
                'opacity': 0
            }, 500, 'linear', function() {

                $('.scrollC').getNiceScroll().remove();
                //if($('.newsload').children().length){
                //$('.scrollC').getNiceScroll().remove();
                $('.newsload').children().remove();
                $('div .newsload').removeClass('newsload');
                //}

                NewsLoad(url, ShowDetails);
                Details = 1;
            });


            return false;
        });


        $('.sub-news li').click(function(e) {
            e.preventDefault();

            var allItem = $('.colum-box').length;
            var widthItem = $('.colum-box').width();
            $('.box-content').width(allItem * widthItem);

            $('.sub-news li').removeClass('current');
            $('.colum-box').removeClass('active');
            $(this).addClass('current');

            var Openpage = $(this).find('a').attr('data-name');

            if (Openpage == "videos" || Openpage == "pictures") {
                var tmpurl = $(this).find('a').attr('href');
                var tmptitle = $(this).find('a').attr('data-title');
                var tmpkeyword = $(this).find('a').attr('data-keyword');
                var tmpdescription = $(this).find('a').attr('data-description');
                var tmpdataname = $(this).find('a').attr('data-name');
                changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);
            }

            var XCurrent = $('.box-content').offset().left;
            var XItem = $('.box-content .colum-box[data-hash= "' + Openpage + '"]').offset().left;
            $('.colum-box[data-hash= "' + Openpage + '"]').addClass('active');


            $('.box-content').stop().animate({
                'left': XCurrent - XItem
            }, 600, 'easeInOutExpo', function() {

                if (Openpage == "videos" || Openpage == "pictures") {
                    //window.location.hash = Openpage;  
                } else {

                    var Lenght = $('.active').find('.news-content').children();

                    if (!$(Lenght).length) {
                        if (!$('.active .link-page').hasClass('current')) {
                            $('.active .link-page:first-child a').trigger('click');
                        } else {
                            $('.active .link-page.current a').trigger('click');
                        }
                    } else {
                        var Name = $('.active .link-page.current a').attr('data-name');
                        var tmpurl = $('.active .link-page.current a').attr('href');
                        var tmptitle = $('.active .link-page.current a').attr('data-title');
                        var tmpkeyword = $('.active .link-page.current a').attr('data-keyword');
                        var tmpdescription = $('.active .link-page.current a').attr('data-description');
                        var tmpdataname = $('.active .link-page.current a').attr('data-name');
                        changeUrl(tmpurl, tmptitle, tmpdescription, tmpkeyword, tmpdataname, tmptitle, tmpdescription);

                        //window.location.hash = Name; 
                    }

                }

                if ($(window).width() > 1100) {
                    $('.box-content, .colum-box.active').css({
                        'height': $(window).height()
                    });
                    $('.colum-box.active').find('.colum-box-news').addClass('fadeinup');
                    $('.colum-box.active').find('.content-album .album-box').addClass('fadeinup');
                    $('.colum-box.active').find('.content-album .video-box').addClass('fadeinup');
                    setTimeout(function() {
                        $('.colum-box.active').find('.news-list').addClass('showup');
                    }, 300);

                } else {
                    var Height = $('.colum-box.active').innerHeight();
                    $('.box-content').css({
                        'height': Height
                    });
                    detectBut();
                }

            });



            return false;
        });


        ScrollNiceB();


        /*if(window.location.hash){
					LocationHash();*/
        if ($('.sub-news li.current').length) {
            $('.sub-news li.current').trigger('click');
        } else {
            $('.sub-news li:first-child').trigger('click');
            //$('.colum-box:first-child').find('.link-page:first-child a').trigger('click');
        }



    }

    //CONTACT PAGE//
    if ($('#contact-page').length) {

        $('.contact-content h2').addClass('fadeinup');
        $('.item-container').addClass('ani-text');
        setTimeout(function() {
            $('.contact-box').addClass('fadeinup');
        }, 500);
        setTimeout(function() {
            $('.contact-form').addClass('fadeinup');
        }, 800);
        var textH = $('.scrollE').innerHeight();
        if (textH >= $(window).height() - 190) {
            setTimeout(function() {
                ScrollNiceE()
            }, 900);
        }



    }


}




function ZoomPic() {

    $('img').click(function() {

        if ($(window).width() <= 740 && $(this).hasClass('zoom-pic')) {
            $('html, body').addClass('no-scroll');

            $(this).parent().addClass('to-scrollZ');
            if (!$('.loadicon').length) {
                $('body').append('<div class="loadicon" style="display:block"></div>');
            }
            $('.all-pics').css({
                'display': 'block'
            });


            if ($('.all-pics .full').length) {
                $('.full').css({
                    'display': 'block'
                });
            } else {
                $('.all-pics').append('<div class="full"  style="display:block"></div>');
            }

            $('.overlay-dark').fadeIn(300, 'linear');
            var activePicLarge = $(this).attr("src");

            $('.all-pics').find('.full').append('<img src ="' + (activePicLarge) + '" alt="pic" />');

            if (!$('.all-pics .close-pics-small').length) {
                $('.all-pics').append('<div class="close-pics-small"></div>');
            }

            $('.all-pics img').load(function() {

                $('.all-pics').addClass('show');

                if (TouchLenght == false || !isTouchDevice) {
                    $('.full').addClass('dragscroll');
                    $('.dragscroll').draptouch();
                } else {
                    $('.full').addClass('pinch-zoom');
                    $('.pinch-zoom').each(function() {
                        new Pic.PinchZoom($(this), {});
                    });
                }



                if ($('.full img').length > 1) {
                    $('.full img').last().remove()
                }

                $('.loadicon').fadeOut(400, 'linear', function() {
                    if (TouchLenght == false || !isTouchDevice) {
                        detectMargin();
                    }

                    $('.full img').addClass('fadein');
                    $('.loadicon').remove();
                });

            });

            setTimeout(function() {
                if (!$('.full img').hasClass('fadein')) {
                    $('.full img').addClass('fadein');
                }
            }, 250);



            $('.close-pics-small, .overlay-dark').click(function() {
                $('.loadicon').remove();
                $('.full, .close-pics-small, .overlay-dark').fadeOut(300, 'linear', function() {

                    $('.all-pics .full,  .all-pics .pinch-zoom-container').remove();
                    $('.close-pics-small').remove();
                    $('.all-pics').css({
                        'display': 'none'
                    }).removeClass('show');

                    if (!$('.album-pic-center').length) {
                        $('html, body').removeClass('no-scroll');
                    }

                    if ($('.to-scrollZ').length) {
                        var top = $('.to-scrollZ').offset().top;
                        $('.to-scrollZ').removeClass('to-scrollZ');

                        if ($(window).width() < 1100) {
                            $('html, body').scrollTop(top - 60);
                        }
                    }

                });
            });

        }
        return false;
    });
}

function Option() {


    $('a.link-pdf, .library-download a').click(function(e) {
        e.preventDefault();
        var url = $(this).attr('href');
        window.open(url, '_blank');
        return false;
    });

    $('.brochure-box').click(function(e) {
        e.preventDefault();
        var url = $(this).find('a').attr('href');
        window.open(url, '_blank');
        return false;
    });


    $('.map-link').click(function(e) {
        e.preventDefault();
        $('html, body').addClass('no-scroll');
        $('.logo.mobile').addClass('fixed');
        if ($('#home-page').length) {
            var Full = $('.slide-bg')[0].swiper;
            Full.stopAutoplay();
        }

        $('.googlemap').addClass('show');
        if (!$('#map-canvas').children().length) {
            initialize();
        }
        return false;
    });

    $('.close-map').click(function(e) {
        e.preventDefault();
        $('html, body').removeClass('no-scroll');
        $('.logo.mobile').removeClass('fixed');
        $('.googlemap').removeClass('show');
        if ($('#home-page').length) {
            var Full = $('.slide-bg')[0].swiper;
            Full.startAutoplay();
        }

        return false;
    });



    $('.view-album, .thumb-album').click(function(e) {
        e.preventDefault();
        var url = $(this).attr('data-href');
        if (!$('.loadicon').length) {
            $('body').append('<div class="loadicon" style="display:block"></div>');
        }
        $('html, body').addClass('no-scroll');
        $('.all-album').fadeIn(100, 'linear');
        $('.overlay-album').css({
            'display': 'block'
        });
        $('.overlay-album').animate({
            'height': '100%'
        }, 800, 'easeOutExpo', function() {
            AlbumLoad(url);
        });
        return false;
    });




    $('a.player, a.play-video, .home-video').click(function(e) {
        e.preventDefault();
        $(this).parent().addClass('to-scrollV');

        if ($('.popup-video img').length) {
            $('.popup-pics, .popup-video').removeClass('fadeinup').addClass('fadeout');
            $('.close-popup').removeClass('fadeinup').addClass('fadeout');
        }

        if ($('.library-load').length) {
            $('.library-center').trigger('BTQ.stop');
        }

        if ($('#home-page').length) {
            var Full = $('.slide-bg')[0].swiper;
            Full.stopAutoplay();
        }


        var idx = $(this).attr('data-href');
        if (!$('.loadicon').length) {
            $('body').append('<div class="loadicon" style="display:block"></div>');
        }
        $('html, body').addClass('no-scroll');
        $('.navigation').css({
            'z-index': 100
        });
        $('.allvideo').css({
            'display': 'block'
        });
        $('.overlay-video').fadeIn(500, 'linear', function() {
            $('.allvideo').addClass('show');
            VideoLoad(idx);
        });
        return false;
    });




    $('.zoom,  .popup-pics-mobile.no-link-popup img, .location-mobile img, .zoom-mobile').click(function() {
        $('html, body').addClass('no-scroll');

        $(this).parent().addClass('to-scroll');
        if (!$('.loadicon').length) {
            $('body').append('<div class="loadicon" style="display:block"></div>');
        }
        $('.all-pics').css({
            'display': 'block'
        });

        if ($('.all-pics .full').length) {
            $('.full').css({
                'display': 'block'
            });
        } else {
            $('.all-pics').append('<div class="full"  style="display:block"></div>');
        }

        $('.overlay-dark').fadeIn(300, 'linear');

        var activePicLarge = $(this).parent().find('img').attr("src") || $(this).parent().find('img').attr("data-src");

        if ($(this).attr("data-src")) {
            var newActive = $(this).attr("data-src");
        } else {
            var newActive = activePicLarge;
        }
        //var newActive = activePicLarge.replace("_s", "_l");



        $('.all-pics').find('.full').append('<img src ="' + (newActive) + '" alt="pic" />');

        if (!$('body .close-pics').length) {
            $('body').append('<div class="close-pics"></div>');
        }
        if (!$('.all-pics .close-pics-small').length) {
            $('.all-pics').append('<div class="close-pics-small"></div>');
        }


        $('.all-pics img').load(function() {
            $('.all-pics').addClass('show');

            if (TouchLenght == false || !isTouchDevice) {
                $('.full').addClass('dragscroll');
                $('.dragscroll').draptouch();
            } else {
                $('.full').addClass('pinch-zoom');
                $('.pinch-zoom').each(function() {
                    new Pic.PinchZoom($(this), {});
                });
            }



            if ($('.full img').length > 1) {
                $('.full img').last().remove()
            }

            $('.loadicon').fadeOut(400, 'linear', function() {
                if (TouchLenght == false || !isTouchDevice) {
                    detectMargin();
                }

                $('.full img').addClass('fadein');
                $('.loadicon').remove();
            });

        });




        $('.close-pics, .close-pics-small').click(function() {
            $('.loadicon').remove();
            $('.full, .close-pics, .close-pics-small').fadeOut(300, 'linear');
            $('.overlay-dark').fadeOut(300, 'linear', function() {
                $('.close-pics, .close-pics-small').remove();
                $('.all-pics .full, .all-pics .text-length, .all-pics .pinch-zoom-container').remove();
                $('.all-pics').css({
                    'display': 'none'
                }).removeClass('show');
                $('html, body').removeClass('no-scroll');

                if ($('.to-scroll').length) {
                    var top = $('.to-scroll').offset().top;
                    $('.to-scroll').removeClass('to-scroll');

                    if ($(window).width() < 1100) {
                        $('html, body').scrollTop(top - 60);
                    }
                }


            });
        });
        return false;
    });




}




function turnWheelTouch() {
    doWheel = true;
    doTouch = true;
}



function detectBut() {

    if ($('#news-page').length) {
        if ($(window).width() <= 1100 && $('.active .link-page').hasClass('current')) {
            var Current = $('.active .link-page.current').parent().parent();
            var Left = $('.active .scrollB').offset().left;
            var XLeft = $('.active .link-page.current').offset().left;
            var Percent = $('.active .news-list').width();
            var Center = $('.news-list').width() / 2 - $('.active .link-page.current').width() / 2;
            $(Current).stop().animate({
                scrollLeft: (XLeft - Center) - Left
            }, 'slow');
        }
        if ($(window).width() <= 1100) {
            if ($('.sub-news ul').length) {
                var Left = $('.sub-news ul').offset().left;
                var XLeft = $('.sub-news li.current').offset().left;
                var Percent = $(window).width() / 100 * 10;
                var Center = ($(window).width() - Percent) / 2 - $('.sub-news li.current').width() / 2;
                var Middle = $(window).width() / 2 - $('.sub-news li.current').width() / 2;
                $('.sub-news').stop().animate({
                    scrollLeft: (XLeft - Center) - Left
                }, 'slow');
            }
        }

    }
    if ($('#projects-page').length) {
        if ($(window).width() <= 1100) {
            if ($('.sub-menu ul').length) {
                var Left = $('.sub-menu ul').offset().left;
                var XLeft = $('.sub-menu li.current').offset().left;
                var Percent = $(window).width() / 100 * 10;
                var Center = ($(window).width() - Percent) / 2 - $('.sub-menu li.current').width() / 2;
                var Middle = $(window).width() / 2 - $('.sub-menu li.current').width() / 2;
                $('.sub-menu').stop().animate({
                    scrollLeft: (XLeft - Center) - Left
                }, 'slow');
            }
        }
    }
    if ($('#project-details-page').length) {
        if ($(window).width() <= 1100) {
            if ($('.sub-nav-typical ul').length) {
                var Left = $('.sub-nav-typical ul').offset().left;
                var XLeft = $('.sub-nav-typical li.current').offset().left;
                var Percent = $(window).width() / 100 * 10;
                var Center = ($(window).width() - Percent) / 2 - $('.sub-nav-typical li.current').width() / 2;
                var Middle = $(window).width() / 2 - $('.sub-nav-typical li.current').width() / 2;
                $('.sub-nav-typical').stop().animate({
                    scrollLeft: (XLeft - Center) - Left
                }, 'slow');
            }
        }
    }


    if ($('.colum-project:first-child').hasClass('active')) {
        $('.prevslide').addClass('disable');
    } else {
        $('.prevslide').removeClass('disable');
    }

    if ($('.colum-project:last-child').hasClass('active')) {
        $('.nextslide').addClass('disable');
    } else {
        $('.nextslide').removeClass('disable');
    }



}



function detectHeight() {
    if ($(window).width() <= 1100) {
        var DH = $(document).innerHeight();
        if (DH > $(window).height() + 100) {
            $('.scroll-down').css({
                'display': 'block',
                'opacity': 1
            });
        } else {
            $('.scroll-down').css({
                'display': 'none',
                'opacity': 0
            });
        }
    }
}


function detectZoom() {
    var ImgW = $('.full img').width();
    var ImgH = $('.full img').height();
    var Yheight = $(window).height();
    var Xwidth = $(window).width();
    if (ImgW > Xwidth) {
        $('.show-zoom').addClass('show');
        $('.full img').addClass('fullsize');
    } else {
        $('.full img').removeClass('fullsize');
    }
}

function detectMargin() {
    var ImgW = $('.full').children().width();
    var ImgH = $('.full').children().height();
    var Yheight = $(window).height();
    var Xwidth = $(window).width();

    if (Xwidth > ImgW) {
        $('.full').children().css({
            'margin-left': Xwidth / 2 - ImgW / 2
        });
    } else {
        $('.full').children().css({
            'margin-left': 0
        });
    }
    if (Yheight > ImgH) {
        $('.full').children().css({
            'margin-top': Yheight / 2 - ImgH / 2
        });
    } else {
        $('.full').children().css({
            'margin-top': 0
        });
    }
}


document.addEventListener('keydown', function(e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 37) {
        $('.sub-menu li.current').prev().find('a').trigger('click');
    }
    if (keyCode === 39) {
        $('.sub-menu li.current').next().find('a').trigger('click');
    }

});

$(document).ready(function() {

    $(document).bind('scroll', function() {
        // var windscroll = $(document).scrollTop();
        var currenttop = $(document).scrollTop()

        if ($(window).width() <= 1100) {
            if (currenttop > 50) {
                $('.scroll-down').fadeOut(500, 'linear');
            } else {
                $('.scroll-down').fadeIn(500, 'linear');
            }


            if (currenttop > $(window).height() / 2) {
                $('.go-top').css({
                    'display': 'block',
                    'opacity': 1
                });
            } else {
                $('.go-top').css({
                    'display': 'none',
                    'opacity': 0
                });
            }


            if ($('.popup-pics-mobile img').length > 0) {

                var Margin = $('.container').css('margin-top');
                var Hight = parseFloat(Margin, 10); //remove pixel
                var Top = $('.popup-pics-mobile').offset().top;
                var Bottom = $('.mobile-intro').offset().top - ($('.mobile-intro').innerHeight() + Hight);
                //console.log(Hight)

                /*if(windscroll <= currenttop && windscroll >= Top-100){
				  $('.popup-img').addClass('absolute');
			   }else{
				  $('.popup-img').removeClass('absolute');
			   }*/

            }

            windscroll = currenttop;

        }



    });

    $('.top').bind('scroll', function() {
        var Topscroll = $('.top').scrollTop();
        if ($(window).width() <= 620) {
            if (Topscroll > 30) {
                $('.right').addClass('hide');
            } else {
                $('.right').removeClass('hide');
            }
        }
    });


    $('.go-top').click(function() {
        $('html, body').animate({
            scrollTop: 0
        }, 'slow');
    });



    $('.container').click(function() {
        $('.top').scrollTop(0);
        $('.container').removeClass('wrap');
        $('.nav-click').removeClass('active');
        $('.overlay-menu, .top, .right').removeClass('show');
        $('.right').removeClass('hide');
        $('html, body').removeClass('no-scroll-nav');
    });

    $('.overlay-menu, .nav').click(function() {
        if ($(window).width() <= 1100) {
            $('.top').scrollTop(0);
            $('.container').removeClass('wrap');
            $('.nav-click').removeClass('active');
            $('.overlay-menu, .top, .right').removeClass('show');
            $('.right').removeClass('hide');
            $('html, body').removeClass('no-scroll-nav');
        }

    });



});

window.onorientationchange = ResizeWindows;
$(window).on("orientationchange", function() {
    if ($(window).width() <= 1100) {
        ScrollHoz();
        if ($('#projects-page, #project-details-page').length) {
            detectBut();
        }

        if ($('.zoom-pic').length && $(window).width() > 740) {
            $('html, body').removeClass('no-scroll');
            $('.close-pics-small').trigger('click');
        }
    }
});
$(window).resize(function() {
    ScrollNiceHide();
    ResizeWindows();
});

$(window).on('resize', function() {
    ResizeWindows();


    if ($('#home-page').length && !$('#map-canvas').children().length) {
        var Full = $('.slide-bg')[0].swiper;
        Full.startAutoplay();
    }



    //-----------------------------			
    //  DESKTOP 	
    if ($(window).width() > 1100) {


        Calculation();

        if ($('.nav-click').hasClass('active')) {
            $('.container').trigger('click');
        }

        if ($('.dragscroll').length) {
            detectMargin();
            $('.dragscroll').draptouch();

        }

        if ($('#home-page').length) {
            if ($('.s-mobile').length) {
                $('div .s-mobile').removeClass('s-mobile');
                var Full = $('.slide-bg')[0].swiper;
                Full.destroy(true, true);
                ResizeWindows();
                setTimeout(function() {
                    SlidePicture();
                }, 150);
            }
        }


        if ($('#scroll-page').length) {
            if (!$('.item-active').length) {
                SlidePicture();
            }

            if ($('.album-box').length) {
                $('.album-box').data('BTQSlider').reinit();
            }
            if ($('.member-box').length) {
                $('.member-box').data('BTQSlider').reinit();
            }

            /*if (!$('.sub-nav li').hasClass('current')) {
		        $('.sub-nav li:first-child ').trigger('click');
		      }else{
			    $('.sub-nav li.current ').trigger('click');
				
		      }*/

        }



       


        if ($('.scrollA, .scrollB, .scrollC, .scrollD, .scrollE, .scrollF').length) {
            $('.scrollB').css({
                'width': '100%'
            });
            setTimeout(function() {
                ScrollNiceA();
                ScrollNiceB();
                ScrollNiceC();
                ScrollNiceD();
                ScrollNiceE();
                ScrollNiceF();
            }, 150);
        }


        //  DESKTOP 

        //-----------------------------		

        //  MOBILE 		
    } else {

        detectHeight();



        if (TouchLenght == false || !isTouchDevice) {
            Calculation();
            ScrollHoz();
            if ($('#news-page').length || $('#projects-page').length) {
                detectBut();
            }

            if ($(window).width() > 740) {
                if ($('.all-pics').hasClass('show')) {
                    $('.close-pics-small, .close-pics').trigger('click');
                }
            }



        }


        ///////////////
        if ($('#home-page').length) {

            if ($('.s-desktop').length) {
                $('div .s-desktop').removeClass('s-desktop');
                var Full = $('.slide-bg')[0].swiper;
                Full.destroy(true, true);
                ResizeWindows();
                setTimeout(function() {
                    SlidePicture();
                }, 150);
            }



        }
        if ($('#scroll-page').length) {
            if ($('.item-active').length) {
                var AboutSlide = $('.bg-content')[0].swiper;
                AboutSlide.destroy(true, true);
            }
            $('.slider-about').addClass('fadein');

        }

      

        if ($('.popup-pics img').length) {
            $('.close-popup').trigger('click');
        }

        if ($('.dragscroll').length) {
            detectMargin();
            $('.dragscroll').draptouch();

        }



    }



    //  MOBILE 	 
    //-----------------------------	

    if ($('#news-page').length) {
        var Open = $('.colum-box.active').attr('data-hash');
        $(".sub-news li a[data-name='" + Open + "']").trigger('click');
    }



}, 250);


function LocationHash() {
    var PageActive = window.location.hash;
    PageActive = PageActive.slice(1);
    Arrhash = PageActive.split('/');

    $(".sub-nav li a[data-name='" + PageActive + "']").trigger('click');

    if ($('#news-page').length && Details == 0) {
        $(".link-page a[data-name='" + PageActive + "']").parent().addClass('current');
        $(".sub-news li a[data-name='" + Arrhash[0] + "']").trigger('click');
    }

    if ($('#project-details-page').length) {
        $(".sub-menu li a[data-name='" + PageActive + "']").trigger('click');
    }


}



/*$(window).bind("popstate", function(e) {
	e.preventDefault();
	  LinkPage();
	  LocationHash();
});*/


$(window).bind("popstate", function(e) {
    if ($(window).width() > 1100) {
        e.preventDefault();
        LinkPage();
    }
    var httpserver = $('.httpserver').text();

    if ($(window).width() > 1100) {

        if (e.originalEvent.state !== null) {
            var tmp_url = e.originalEvent.state.path;
            var tmp_dataName = e.originalEvent.state.dataName;
            var tmptitle = e.originalEvent.state.title;
            var tmpurl = document.URL;

            changeUrl(tmp_url, tmptitle, '', '', tmp_dataName, '', '');

            var temp_url_1 = tmp_url.replace(httpserver, "");
            var tmp_1 = temp_url_1.split('/');

            if ($('#scroll-page').length) {
                if ($('.close-album').length) {
                    $('.close-album').trigger('click');
                }

                $(".nav li a").each(function(index, element) {
                    if ($(element).attr('href') == tmp_url) {
                        window.history.back();
                    }
                });
                $(".sub-nav li a").each(function(index, element) {
                    if ($(element).attr('href') == tmp_url) {
                        $(element).parent().trigger('click');
                    }
                });
            }

            
           

        } else {
            var tmpurl = document.URL;

            var temp_url_1 = tmpurl.replace(httpserver, "");
            var tmp_1 = temp_url_1.split('/');

            if ($('#scroll-page').length) {
                if ($('.close-album').length) {
                    $('.close-album').trigger('click');
                }

                $(".nav li a").each(function(index, element) {
                    if ($(element).attr('href') == tmpurl) {
                        window.history.back();
                    }
                });
                $(".sub-nav li a").each(function(index, element) {
                    if ($(element).attr('href') == tmpurl) {
                        $(element).parent().trigger('click');
                    }
                });

            }





        }
    } else {

        if (e.originalEvent.state !== null) {
            var tmp_url = e.originalEvent.state.path;
        } else {
            var tmp_url = document.URL;
        }

        var temp_url_1 = tmp_url.replace(httpserver, "");
        var tmp_1 = temp_url_1.split('/');

     


    }

});


if (iOS) {
    $(window).bind("pageshow", function(event) {
        if (event.originalEvent.persisted) {
            window.location.reload();
        }
    });
}