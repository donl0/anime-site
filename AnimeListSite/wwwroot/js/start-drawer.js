function drawStars(rating) {
	const container = document.getElementById("stars");

	container.innerHTML = '';

	for (let i = 0; i < 10; i++) {
		const star = document.createElement("span");
		star.innerHTML = "&#9734;";
		star.classList.add("gray-star");
		container.appendChild(star);
	}

	const goldStarsCount = Math.round(rating);

	for (let i = 0; i < goldStarsCount; i++) {
		const star = container.children[i];
		star.innerHTML = "&#9733;";
		star.classList.remove("gray-star");
		star.classList.add("gold-star");
	}

	const lastGoldStar = container.children[goldStarsCount];
	if (lastGoldStar) {
		const percentage = (rating - goldStarsCount) * 100;
		lastGoldStar.style.width = `${percentage}%`;
	}
}

drawStars(8.66);