

$(function () {

// slider 3
var swiper = new Swiper(".search-category-slider", {
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
      }
  },
  });


  
});