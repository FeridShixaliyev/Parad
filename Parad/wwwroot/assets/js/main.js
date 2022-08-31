

// Swiper

var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 30,
    slidesPerGroup: 3,
    loop: true,
    loopFillGroupWithBlank: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});

// Masonry section
document.querySelectorAll('.container .row .masonry .item img').forEach(image => {
    image.onclick = () => {
        document.querySelector('.container .row .popup-img').style.display = 'block'
        document.querySelector('.container .row .popup-img img').src = image.getAttribute('src')
    }
})
document.querySelector('.container .row .popup-img').onclick = () => {
    document.querySelector('.container .row .popup-img').style.display = 'none'
}

