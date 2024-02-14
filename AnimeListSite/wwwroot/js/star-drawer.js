function drawStars(rating) {
    const container = document.getElementById("stars");

    container.innerHTML = '';

    for (let i = 0; i < 10; i++) {
        const star = document.createElement("span");
        star.innerHTML = "&#9734;";
        star.classList.add("gray-star");

        star.addEventListener("mouseover", function () {
            highlightStars(i + 1);
        });
        star.addEventListener("click", function () {
            sendRating(i + 1);
        });

        if (i < rating) {
            star.innerHTML = "&#9733;";
            star.classList.remove("gray-star");
            star.classList.add("gold-star");
        }

        container.appendChild(star);
    }
}

function highlightStars(count) {
    const container = document.getElementById("stars");
    const stars = container.children;

    for (let i = 0; i < stars.length; i++) {
        stars[i].classList.remove("hover-star");
    }

    for (let i = 0; i < count; i++) {
        stars[i].classList.add("hover-star");
    }
}

function sendRating(rating) {
    console.log("Отправлен рейтинг:", rating);
}
