var qntUpdated = false;
var qntUpdater = false;
var qntAjaxUpdate = false;


$(document).ready(function() {

    mainMenu.init();

    colorboxInit();

    pngFixInit();

    shopCartInit();

    shopCartDetailInit();

    sliderInit();

    carouselInit();

    spoilerInit();

    floatMenu();

    photoGallery();

    initMobile();

    initAccessibility();

    initRatingStars();

    initProductTabs();

    initSocial();

    initQuantity();

    initButtontop();
});

function initButtontop() {

    if ($('.scrollTop').length) {
        var fotter_bottom = $('.footer').outerHeight(true);;
        var fotter_right = $('.footer').outerWidth() + $('.footer').offset().left;

        $('.scrollTop').css({left: fotter_right + 30, bottom: fotter_bottom + 30 });

        $(window).scroll(function(){
            if ($(this).scrollTop() > 100) {
                $('.scrollTop').fadeIn();
            } else {
                $('.scrollTop').fadeOut();
            }
        });

        $('.scrollTop').click(function(){
            $('html, body').animate({scrollTop : 0},800);
            return false;
        });
    }
}

function initQuantity() {
    $('.productQuantity').css({display: 'inline-block'});

    $('.btnProductQntAdd').click(function (event) {
        event.preventDefault();
        $(this).parents('form').find('input').each(function() {
            var qntNow = parseInt($(this).val());
            if (qntNow<999)
                $(this).val(qntNow+1);
        });
    });

    $('.btnProductQntDel').click(function (event) {
        event.preventDefault();
        $(this).parents('form').find('input').each(function() {
            var qntNow = parseInt($(this).val());
            if (qntNow>1)
                $(this).val(qntNow-1);
        });
    });

}

function initSocial() {
    var socialButtons = $('.productSocial');
    var social = socialButtons.find('.social');
    socialButtons.show();
    social.hide();
    socialButtons.find('button').on('click', function () {
        social.toggle();
    });
}
function initProductTabs() {
    var tabs = $('[data-tabs]');
    var tab_item = tabs.find('[data-tab]');
    tabs.prepend('<ul class="tabs"></ul>');
    tab_item.each(function(){
        var id = $(this).attr('data-tab');
        var h = $(this).find('h2').first().text();

        $(this).find('h2').first().hide();
        tabs.find('ul.tabs').append('<li data-tab-id="'+id+'">'+h+'</li>');
    });

    tab_item.first().show().siblings().hide();
    var tab_id = tabs.find('[data-tab-id]');
    tab_id.first().addClass('current');
    tab_id.on('click', function(){
        var id = $(this).attr('data-tab-id');
        $(this).addClass('current').siblings().removeClass('current');
        tabs.find('[data-tab="'+id+'"]').show().siblings().hide();
    });

}

function initRatingStars() {
    var label = $('.checkBlock .rating label');
    label.on('click', function(){
        $(this).addClass('checked');
    });
    label.mouseover(function(){

        var rate = $(this).attr('title');

        $('.checkBlock .rating label').first().text(rate);

        $('.checkBlock .rating input').each(function() {
            if ($(this).prop('checked')) {
                $('.checkBlock .rating input').removeClass('checked');
                $(this).addClass('checked');
            }
        });

    }).mouseout(function(){
        if (label.hasClass('checked')){
            var rate = $(this).parent().find('input:checked').next().attr('title');

            $('.checkBlock .rating label').first().text(rate);
        } else {
            $('.checkBlock .rating label').first().text('');
        }
    });

}

function initAccessibility() {
    if (!$('.accessibility_menu').length) return;

    var fontClasses = new Array('afont1','afont2', 'afont3');

    $('.accessibility_menu ul.fonts a').on('click', function(e) {
        var url = $(this).attr('href');
        var menu = $('.accessibility_menu ul.fonts');
        updateBodyClass($(this), menu, fontClasses);
        return false;
    });

    var header = $('.headerWrapper');
    if (!(header.is(':has(.logo)') || header.is(':has(.text.content)') || header.is(':has(.mainMenuVertical)'))) {
        $('.headerImage').css({height: 'auto', minHeight: 'initial'});
    }
    $('.blind_settings').on('click', function(){
        var panel = $('.settingPanel');
        if (panel.hasClass('open')){
            panel.slideUp();
            panel.removeClass('open');
        } else {
            panel.slideDown();
            panel.addClass('open');
        }
    });
}

function updateBodyClass(link, menu, classes)
{
    var newClass = link.attr('id').substr(5);

    if ($('body').hasClass(newClass)) return;
    var url = link.attr('href');

    $.get( url, function( r ) {
        if (r!=='success') return;

        menu.find('li').each(function () { $(this).removeClass('current'); });
        link.parent().addClass('current');

        $.each(classes, function(index, cls) {
            if (cls == newClass) {
                $('body').addClass(newClass);
            } else {
                $('body').removeClass(cls);
            }
        });
    });
}

function initMobile() {
    if (!$('.useMobileVersion').length) return;
    initMobileMainMenu();
    initMobileSearchMenu();
    initMobileTeasers();
    initMobileLangMenu();
    initMobilePopUp();

    $('iframe').each(function () {
        $(this).wrap('<div class="tableWrapper"></div>');
    });
    $('table').each(function () {
        $(this).wrap('<div class="tableWrapper"></div>');
    });
    $('.switchers').find('strong').parent('li').addClass('current');
}

function initMobilePopUp() {
    var popUp = $('.cartPopUp');
    var $form = $('.add_to_cart_js');

    $btn.on('submit', function(){
        var popUpBuffer = popUp.detach();
        popUpBuffer.appendTo('body');
    });

    $('.content').find('img').removeClass();
}

function initMobileMainMenu() {
    var mDrop = $('.menuDropdown');
    mDrop.find('li ul').hide();
    mDrop.find('li.current ul').show();
    mDrop.hide();

    $('.menuCollapse').on('click', function () {
        mDrop.slideToggle();
    });

    $('.wgExtraMenuMain ul li ul').parent().addClass('hasChild');
}

function initMobileLangMenu() {
    $('.selectedLang').on('click', function(){
        var lDrop = $(this).next('.serviceMenuLang');
        $(this).hide();
        lDrop.addClass('active');
        lDrop.show();
    });

    $(document).click(function(event) {
        if ($(event.target).closest('.serviceMenuLang.active').length || $(event.target).closest('.selectedLang').length ) return;
        $('.serviceMenuLang.active').hide();
        $('.serviceMenuLang.active').removeClass('active');
        $('.selectedLang').show();
        event.stopPropagation();
    });

}

function initMobileSearchMenu() {
    $('.serviceMenuSearch .input input').on('focus', function () {
        $(this).className = 'textBlack';
        if ($(this).val() == $(this).data('default')) $(this).val('');
    }).on('blur', function () {
        $(this).className = 'textGray';
        if ($(this).val() == '') $(this).val($(this).data('default'));
    });

    $('.serviceMenuSearch .searchClose').on('click', function () {
        $(this).parents('form').fadeOut();

        var input = $(this).parents('form').find('input').one();
        input.val(input.data('default'));
//            e.preventDefault();
        return false;
    });

    $('.serviceMenuSearch').on('click', function () {
        //$(this).find('input').val('');
        $(this).children('form').fadeIn();
    });

}

function initMobileTeasers() {
    var slickSlideItem = $('.wgWideTriggers').find('.slick-slide').children('.item');
    var colors = ['#e2f8b4', '#def0fa', '#fbcdd5', '#fbf9cd', '#cdfbf1', '#fbe4cd'];
    var colorI = 0;

    slickSlideItem.each(function () {
        if (colorI < colors.length) {
            $(this).css({backgroundColor: colors[colorI]});
        } else {
            colorI = -1;
        }
        colorI++;
    });
}

var mainMenu = new function() {

    this.li = $('.mainMenuPullDown > ul > li');
    this.submenu = $('.mainMenuPullDown > ul > li > ul:first');

    this.init = function() {

        $('.mainMenuPullDown > ul > li').hover(
            function() {
                mainMenu.clear();

                if($(this).hasClass('hasChildren')) mainMenu.li = $(this).addClass('hover');
                mainMenu.submenu = $(this).find('ul:first').show();
            },
            function() {
                closeTimer = setTimeout(function() {
                    mainMenu.submenu.hide();
                    mainMenu.li.removeClass('hover');
                }, 500);
            }
        );

        $(document).click(function(e){
            if(e.target.parentElement.parentNode.localName != 'li' && typeof mainMenu.submenu !== 'undefined') {
                mainMenu.submenu.hide();
                mainMenu.li.removeClass('hover');
            }
        });

    };

    this.clear = function() {

        if(typeof mainMenu.submenu !== 'undefined' && typeof closeTimer !== 'undefined'){
            clearTimeout(closeTimer);
            mainMenu.submenu.hide();
            mainMenu.li.removeClass('hover');
        }

    };

};

function colorboxInit() {

    if($.isFunction($.fn.colorbox)) {
        $('a[rel="gallery"]').colorbox({ width: '75%', height: '75%' });
        // $('a[rel="gallery"]').colorbox();
    }

}

function pngFixInit() {

    if($.isFunction($.fn.pngFix)) {
        $(document).pngFix();
    }

}

function shopCartInit() {

    $cartPopUp = $('.cartPopUp');

    if($cartPopUp.length != 0) {

        $('.add_to_cart_js').submit(function() {
            $form = $(this);
            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                data: $(this).serialize(),
                success: function(data) {

                    $('.wgCartBlock').replaceWith(data);
                    $form.find('input').val(1);
                    $cartPopUp.overlay({
                        top: '50%',
                        load: true,
                        close: '#product_added_close',
                        closeOnClick: false
                    }).load();

                }
            });
            return false;
        });

    }

}

function shopCartDetailInit()
{
    // remove old data
    qntUpdated = false;
    qntAjaxUpdate = false;

    $('.cartItemQuanity').prop("disabled", false).keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if (e.keyCode == 8)
        {
            updateQnt($(this), false, 5000);
            return;
        }

        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+V
            (e.keyCode == 86 && e.ctrlKey === true) ||
                // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
        updateQnt($(this), false, 5000);

    }).change(function () {
        updateQnt($(this));
    }).bind('paste', function () {
        updateQnt($(this));
    });

    $('.btnQntAdd').click(function (event) {
        event.preventDefault();

        if (qntAjaxUpdate == true) return;

        var qntInput = $(this).parent().find('input').one();
        var qntNow = parseInt(qntInput.val());
        updateQnt(qntInput, qntNow+1);
    });

    $('.btnQntDel').click(function (event) {
        event.preventDefault();
        if (qntAjaxUpdate == true) return;

        var qntInput = $(this).parent().find('input').one();
        var qntNow = parseInt(qntInput.val());
        updateQnt(qntInput, qntNow-1);
    });

    $('.btnOrder').click(function () {
        if (false !== qntUpdated)
            event.preventDefault();
    });

}

function updateQnt(qntInput, qnt, ttl)
{
    if (false == qntUpdated) qntUpdated = {};

    var product = qntInput.data('product');
    if (!(product in qntUpdated)) qntUpdated[product] = 1;

    $('.btnOrder').addClass('disabled');

    if (typeof (ttl) === 'undefined') ttl = 2000;
    if (typeof (qnt) === 'undefined') qnt = false;

    if  (qnt!==false)
    {
        if (qnt > 999) qnt = 999;
        if (qnt < 1) qnt = 1;
        qntInput.val(qnt);
    }

    if (qntUpdater) clearTimeout(qntUpdater);
    qntUpdater = setTimeout(function() { ajaxUpdateCart(); }, 1500);
}


function blockCartUI()
{
    $('.cartItemQuanity').prop("disabled", true);
    $('#cartEdit').addClass('disabled');
}

function ajaxUpdateCart()
{
    var data = {};
    var i=0;


    for(var p in qntUpdated)
    {
        var cq = parseInt($('#cart_product_'+p).val());
        var oq = parseInt($('#cart_product_'+p).data('value'));
        if (cq == oq) continue;

        var name = 'quantity[' + p + ']';
        data[name] = cq;
        i++;
    }

    if (i>0)
    {
        var updateUrl  = $('#cartEdit').data('url');
        qntAjaxUpdate = true;
        blockCartUI();


        $.ajax({
            type: "POST",
            url: updateUrl,
            data: data
        }).done(function( result ) {
            $('#cartEdit').replaceWith(result);
            shopCartDetailInit();
        });
    }
    else
    {
        // temporary test!
        qntUpdated = false;
        $('.btnOrder').removeClass('disabled');
    }


}


function sliderInitOld() {

    var sliders_params = {
        content: '.sliderContent',
        children: 'div',
        transition: 'vertical',
        animationSpeed: 500,
        autoplay: false,
        pauseOnHover: true,
        bullets: true,
        arrows: true,
        arrowsHide: true,
        prev: 'prev',
        next: 'next',
        animationStart: function(){},
        animationComplete: function(){}
    };

    $('.slider').each(function() {
        var slider = $(this);

        sliders_params.transition = slider.data('transition');
        sliders_params.autoplay = (String(slider.data('autoplay'))=="true");
        sliders_params.autoplaySpeed = slider.data('autoplay-speed');
        sliders_params.bullets = (String(slider.data('bullets'))=="true");
        sliders_params.arrowsHide = (String(slider.data('arrows-hide'))=="true");
        sliders_params.arrows = (String(slider.data('arrows'))=="true");

        slider.mobilyslider(sliders_params);
    });

}

function sliderInit() {

    if ($.isFunction($.fn.slick)) {

        $('.slider').each(function () {
            var slider = $(this);
            var slider_params = {
                dots: (String(slider.data('bullets'))=="true"),
                slidesToShow: 1,
                slidesToScroll: ($(this).data('scroll')) ? slidesNum : 1,
                autoplay: (String(slider.data('autoplay'))=="true"),
                autoplaySpeed: slider.data('autoplay-speed'),
                speed: $(this).data('speed'),
                arrows: (String(slider.data('arrows'))=="true")
            };

            if (slider.data('transition') == "fade"){
                slider_params.fade = true;
            } else if (slider.data('transition') == "vertical"){
                slider_params.vertical = true;
            }

            slider.slick(slider_params);

        });

    }
}

function carouselInit() {

    if ($.isFunction($.fn.slick)) {

        $('[data-carousel]').each(function () {

            if ($(this).data('carousel')) {

                if ($(this).hasClass('mobile')) {
                    if (($(document).width()) < 560){
                        slidesNum = 1;
                    } else {
                        slidesNum = 2;
                    }
                } else {
                    var cWidth = $(this).width();
                    var itemWidth = 220;

                    var itemWidthMin = 220; // 320
                    var itemWidthAvg = 225;
                    var itemWidthMax = 230; //


                    var c1 = Math.floor(cWidth / itemWidthMin);
                    var c2 = Math.floor(cWidth / itemWidthAvg);
                    var c3 = Math.floor(cWidth / itemWidthMax);

                    if (c1 > c2) {
                        itemWidth = itemWidthMin;
                    } else if (c2 > c3) {
                        itemWidth = itemWidthAvg;
                    } else {
                        itemWidth = itemWidthMax;
                    }
                    /*
                     if (cWidth > 900) {
                     itemWidth = 230;
                     } else if (cWidth > 500 && cWidth < 900) {
                     itemWidth = 227;
                     }
                     */
                    var slidesNum = Math.floor(cWidth / itemWidth);
                    if (slidesNum == 0) slidesNum = 1;
                }

                $(this).slick({
                    slidesToShow: slidesNum,
                    slidesToScroll: ($(this).data('scroll')) ? slidesNum : 1,
                    autoplay: $(this).data('autoplay'),
                    autoplaySpeed: $(this).data('interval'),
                    speed: $(this).data('speed')
                });

            }

        });

        $('[data-carousel] .item.fade').hover(
            function () {
                $(this).find('.description').fadeIn(150);
            },
            function () {
                $(this).find('.description').fadeOut(150);
            }
        );

    }

}

function spoilerInit() {

    $('ul.accordion > li > span').on('click', function (e) {

        e.preventDefault();

        if (!$(this).parents('ul').hasClass('spoilers') && !$(this).hasClass('open')) {
            $(this).closest('ul.accordion').find('ul').slideUp(150);
            $(this).closest('ul.accordion').find('span').removeClass('open');
        }

        $(this).parent().find('> ul').slideToggle(150);
        $(this).parent().find('> span').toggleClass('open');

    });

    //$('ul.accordion > li').click(function (e) {

    //    e.preventDefault();

    //    if (!$(this).parent().hasClass('spoilers') && !$(this).find('> span').hasClass('open')) {
    //        $(this).closest('ul.accordion').find('ul').slideUp(150);
    //        $(this).closest('ul.accordion').find('span').removeClass('open');
    //    }

    //    $(this).find('> ul').slideToggle(150);
    //    $(this).find('> span').toggleClass('open');

    //});

}

function floatMenu() {

    var $menuFloat = $('.menuFloat');

    if ($menuFloat.length != 0) {

        var $menuClone = $menuFloat.clone(true),
            menuFloatOffset = $menuFloat.offset().top,
            menuFloatHeight = $menuFloat.height(),
            menuFloatWidth = $menuFloat.width() + parseInt($menuFloat.css('padding-left')) + parseInt($menuFloat.css('padding-right')),
            menuFloatOffsetLeft = $menuFloat.offset().left,
            menuFloatGrid = 'float_' + $menuFloat.parent().attr('class').split('_')[1];

        $menuClone.addClass('init').addClass(menuFloatGrid).appendTo('body');

        $menuClone.find('> ul').css({width: menuFloatWidth, marginLeft: menuFloatOffsetLeft});

        $(window).resize(function () {

            menuFloatOffsetLeft = $menuFloat.offset().left;
            $menuClone.find('> ul').css({width: menuFloatWidth, marginLeft: menuFloatOffsetLeft});
            return false

        });

        $(window).scroll(function () {

            var documentScrollState = (navigator.userAgent.search(/MSIE/) != -1) ? document.documentElement.scrollTop : window.scrollY;

            if (documentScrollState > menuFloatOffset /*+ menuFloatHeight*/) {

                $menuClone.show();

            } else {

                $menuClone.hide();

            }

        });

    }

}

function photoGallery() {

    var $gallery = $('.blockGallery'),
        effects = (typeof $gallery.data('style') != 'undefined') ? $gallery.data('style') : '';

    switch (effects) {

        case 'js_s7':

            $gallery.find('> a').each(function () {
                $(this).hoverdir({
                    hoverDelay: 50,
                    inverse: true
                });
            });

            break;

    }

}
