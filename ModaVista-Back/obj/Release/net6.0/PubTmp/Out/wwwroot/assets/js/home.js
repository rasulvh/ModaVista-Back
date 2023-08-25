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

    //Slider
    var firstSlider = $('#first-slider');

    firstSlider.owlCarousel({
        loop: true,
        margin: 10,
        items: 1,
        autoplay: true,
        autoplayTimeout: 5000,
    }).on('changed.owl.carousel', function (event) {
        // Remove the previous animation classes
        $('.item .text span').removeClass('animate__animated animate__fadeInUp');
        $('.item .text h1').removeClass('animate__animated animate__fadeInUp');
        $('.item .text p').removeClass('animate__animated animate__fadeInUp');
        $('.item .text a').removeClass('animate__animated animate__fadeInUp');
        $('.item .sale').removeClass('animate__animated animate__rotateIn');

        // Select the current slide's text element and add the animation classes
        var currentSlide = event.item.index;
        $('.item .text').eq(currentSlide).find('h1').addClass('animate__animated animate__fadeInUp');
        $('.item .text').eq(currentSlide).find('p').addClass('animate__animated animate__fadeInUp');
        $('.item .text').eq(currentSlide).find('span').addClass('animate__animated animate__fadeInUp');
        $('.item .text').eq(currentSlide).find('a').addClass('animate__animated animate__fadeInUp');
        $('.item').eq(currentSlide).find('.sale').addClass('animate__animated animate__rotateIn');
    });

    $('#slider .fa-chevron-right').click(function () {
        firstSlider.trigger('next.owl.carousel');
    })
    $('#slider .fa-chevron-left').click(function () {
        firstSlider.trigger('prev.owl.carousel', [300]);
    })

    //Women Clothing Tab Menu
    $('#women-banner .head p').click(function () {
        $('#women-banner p.active').addClass('de-active')
        $('#women-banner p.active').removeClass('active')
        if ($(this).hasClass('de-active')) {
            $(this).removeClass('de-active')
        }
        $(this).addClass('active')

        let dataId = $(this).attr('data-id')

        $('#women-banner .datas div.owl-carousel').each(function () {
            if ($(this).attr('data-id') == dataId) {
                if ($(this).hasClass('d-none')) {
                    $(this).removeClass('d-none')
                }
            }
            else {
                if (!$(this).hasClass('d-none')) {
                    $(this).addClass('d-none')
                }
            }
        })
    })

    var womenSlider1 = $('#women-carousel-1');

    if (womenSlider1.children().length > 3) {
        womenSlider1.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        womenSlider1.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#women-banner .datas .fa-chevron-right').click(function () {
        womenSlider1.trigger('next.owl.carousel');
    })
    $('#women-banner .datas .fa-chevron-left').click(function () {
        womenSlider1.trigger('prev.owl.carousel', [300]);
    })

    var womenSlider2 = $('#women-carousel-2');

    if (womenSlider2.children().length > 3) {
        womenSlider2.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        womenSlider2.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                    loop: true,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#women-banner .datas .fa-chevron-right').click(function () {
        womenSlider2.trigger('next.owl.carousel');
    })
    $('#women-banner .datas .fa-chevron-left').click(function () {
        womenSlider2.trigger('prev.owl.carousel', [300]);
    })

    var womenSlider3 = $('#women-carousel-3');

    if (womenSlider3.children().length > 3) {
        womenSlider3.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        womenSlider3.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                    loop: true,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#women-banner .datas .fa-chevron-right').click(function () {
        womenSlider3.trigger('next.owl.carousel');
    })
    $('#women-banner .datas .fa-chevron-left').click(function () {
        womenSlider3.trigger('prev.owl.carousel', [300]);
    })

    var womenSlider4 = $('#women-carousel-4');

    if (womenSlider4.children().length > 3) {
        womenSlider4.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        womenSlider4.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                    loop: true,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#women-banner .datas .fa-chevron-right').click(function () {
        womenSlider4.trigger('next.owl.carousel');
    })
    $('#women-banner .datas .fa-chevron-left').click(function () {
        womenSlider4.trigger('prev.owl.carousel', [300]);
    })

    //Men Clothing Tab Menu
    $('#men-banner .head p').click(function () {
        $('#men-banner p.active').addClass('de-active')
        $('#men-banner p.active').removeClass('active')
        if ($(this).hasClass('de-active')) {
            $(this).removeClass('de-active')
        }
        $(this).addClass('active')

        let dataId = $(this).attr('data-id')

        $('#men-banner .datas div.owl-carousel').each(function () {
            if ($(this).attr('data-id') == dataId) {
                if ($(this).hasClass('d-none')) {
                    $(this).removeClass('d-none')
                }
            }
            else {
                if (!$(this).hasClass('d-none')) {
                    $(this).addClass('d-none')
                }
            }
        })
    })

    var menSlider1 = $('#men-carousel-1');

    if (menSlider1.children().length > 3) {
        menSlider1.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        menSlider1.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#men-banner .datas .fa-chevron-right').click(function () {
        menSlider1.trigger('next.owl.carousel');
    })
    $('#men-banner .datas .fa-chevron-left').click(function () {
        menSlider1.trigger('prev.owl.carousel', [300]);
    })

    var menSlider2 = $('#men-carousel-2');

    if (menSlider2.children().length > 3) {
        menSlider2.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        menSlider2.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#men-banner .datas .fa-chevron-right').click(function () {
        menSlider2.trigger('next.owl.carousel');
    })
    $('#men-banner .datas .fa-chevron-left').click(function () {
        menSlider2.trigger('prev.owl.carousel', [300]);
    })

    var menSlider3 = $('#men-carousel-3');

    if (menSlider3.children().length > 3) {
        menSlider3.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        menSlider3.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#men-banner .datas .fa-chevron-right').click(function () {
        menSlider3.trigger('next.owl.carousel');
    })
    $('#men-banner .datas .fa-chevron-left').click(function () {
        menSlider3.trigger('prev.owl.carousel', [300]);
    })

    var menSlider4 = $('#men-carousel-4');

    if (menSlider4.children().length > 3) {
        menSlider4.owlCarousel({
            loop: true,
            margin: 10,
            items: 3,
            autoplay: true,
            autoplayTimeout: 3000,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }
    else {
        menSlider4.owlCarousel({
            loop: false,
            margin: 10,
            items: 3,
            responsive: {
                600: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
    }

    $('#men-banner .datas .fa-chevron-right').click(function () {
        menSlider4.trigger('next.owl.carousel');
    })
    $('#men-banner .datas .fa-chevron-left').click(function () {
        menSlider4.trigger('prev.owl.carousel', [300]);
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