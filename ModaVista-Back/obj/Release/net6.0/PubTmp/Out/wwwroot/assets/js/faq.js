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

    //Faq Menu Open/Close
    $("#faqs .faq .head").click(function () {
        let dataId = $(this).attr("data-id");

        $(`#faqs .faq-${dataId} .body`).animate({
            height: 'toggle'
        });

        if ($(`#faqs .faq-${dataId} .head .icons .fa-minus`).hasClass("d-none")) {
            $(`#faqs .faq-${dataId} .head .icons .fa-plus`).addClass("d-none");
            $(`#faqs .faq-${dataId} .head .icons .fa-minus`).removeClass("d-none");
        }
        else {
            $(`#faqs .faq-${dataId} .head .icons .fa-plus`).removeClass("d-none");
            $(`#faqs .faq-${dataId} .head .icons .fa-minus`).addClass("d-none");
        }
    })
})