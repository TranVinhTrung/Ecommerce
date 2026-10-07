
// slider 1
var swiper = new Swiper(".main-slider", {
    loop: true,
    autoplay: {
      delay: 2500,
      disableOnInteraction: false,
    },
    navigation: {
      nextEl: ".main-slider-icon-right",
      prevEl: ".main-slider-icon-left",
    },
  });


  // slider 2
  var swiper = new Swiper(".categories-slider", {
    slidesPerView: 2,
    spaceBetween: 16,
    loop: true,
    navigation: {
      nextEl: ".categories-slider-icon-right",
      prevEl: ".categories-slider-icon-left",
    },
    breakpoints: {
      640: {
        slidesPerView: 2,
        spaceBetween: 24,

      },
      768: {
        slidesPerView: 3,
        spaceBetween: 24,
      },
      1024: {
        slidesPerView: 4,
        spaceBetween: 24,
      },
      1200: {
        slidesPerView: 5,
        spaceBetween: 24,
      },
    },
  });


  // slider 3
  var swiper = new Swiper(".dealsSwiper", {
    loop: true,
    navigation: {
      nextEl: ".deal-slide-icon-left",
      prevEl: ".deal-slide-icon-right",
    },
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });



   // slider 4
   var swiper = new Swiper(".Instagram-Swiper", {
    slidesPerView: 2,
    spaceBetween: 16,
    loop: true,
    breakpoints: {
      640: {
        slidesPerView: 2,
        spaceBetween: 24,

      },
      768: {
        slidesPerView: 3,
        spaceBetween: 24,
      },
      1024: {
        slidesPerView: 4,
        spaceBetween: 24,
      },
      1200: {
        slidesPerView: 5,
        spaceBetween: 24,
      },
    },
  });


  