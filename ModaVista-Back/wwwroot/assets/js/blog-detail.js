$(document).ready(function () {

    // Mobile Menu Accordion
    $(".menu-pages span").click(function () {
        $(".menu-pages").toggleClass("menu-pages-open");

        if ($(".menu-pages span i").hasClass("fa-chevron-right")) {
            $(".menu-pages span i").removeClass("fa-chevron-right")
            $(".menu-pages span i").addClass("fa-chevron-down")
        }
        else {
            $(".menu-pages span i").addClass("fa-chevron-right")
            $(".menu-pages span i").removeClass("fa-chevron-down")
        }
    })

    //Mobile Menu Open/Close
    $(".menu-btn").click(function () {
        $(".menu").toggleClass("menu-open");
    })

    //Firm Logo Carousel
    var firmLogoSlider = $('#firm-logo-carousel');

    firmLogoSlider.owlCarousel({
        loop: false,
        margin: 10,
        items: 5,
        dots: false,
        responsive: {
            600: {
                items: 1,
            },
            768: {
                items: 3,
            },
            1200: {
                items: 5,
            }
        }
    })

})