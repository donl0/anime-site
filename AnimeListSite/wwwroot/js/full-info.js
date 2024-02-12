function startAnimation() {
    var button = document.getElementById("open-lists");
    button.classList.add("animated");
    setTimeout(function () {
        button.classList.remove("animated");
    }, 150);
}

function toggleListsHandler() {
    var listsHandler = document.getElementById("lists-handler");
    listsHandler.classList.toggle("hidden");
}

function OnAddToListButtonClicked() {
    startAnimation();
    toggleListsHandler();
}