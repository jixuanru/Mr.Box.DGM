/**
 * cbpAnimatedHeader.js v1.0.0
 * http://www.codrops.com
 *
 * Licensed under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 *
 * Copyright 2013, Codrops
 * http://www.codrops.com
 */
var cbpAnimatedHeader = (function () {
    var docElem = document.documentElement,
        header = document.querySelector('.navbar-default'),
        didScroll = false,
        changeHeaderOn = 200;

    function init() {
        window.addEventListener('scroll', function (event) {
            if (!didScroll) {
                didScroll = true;
                setTimeout(scrollPage, 250);
            }
        }, false);
        //$(".Colorhover").css("color", "#1ab394");
    }

    function scrollPage() {
        
        var sy = scrollY();
        if (sy >= changeHeaderOn) {
            classie.add(header, 'navbar-scroll');
            $(".navbar-brand img").attr("src", "../../Content/image/logo2.png");
            $(".Colorhover").css("color", "#FFF");
            $(".language ul li a").css("color", "#676a6c");
            $(".language ul li a").mouseover(function () {
                $(this).css("color", "#1ab394");
            }).mouseleave(function () {
                $(this).css("color", "#676a6c");
            });
            $(".navbar-wrapper .navbar").css("border-bottom", "1px solid #676a6c");
            
        }
        else {
            classie.remove(header, 'navbar-scroll');
            $(".navbar-brand img").attr("src", "../../Content/image/logo.png");
            $(".Colorhover").css("color", "#FFF");
            $(".language ul li a").css("color", "#FFF");
            $(".language ul li a").mouseover(function () {
                $(this).css("color", "#1ab394");
            }).mouseleave(function () {
                $(this).css("color", "#FFF");
            });
            $(".navbar-wrapper .navbar").css("border-bottom", "none");
        }
        didScroll = false;
    }

    function scrollY() {
        return window.pageYOffset || docElem.scrollTop;
    }

    init();

})();