import { PLUS_SIGN, MINUS_SIGN } from './constants.js';

function updateBookmarkSign(element) {
    var currentSign = element.text();
    element.text(currentSign === PLUS_SIGN ? MINUS_SIGN : PLUS_SIGN);
}

$(document).ready(function () {
    $('.add-remove-button').on('click', function () {
        var bookmark = $(this).data('bookmark');
        var animeId = $(this).data('anime-id');

        var bookmarkSignElement = $(this).find('.bookmark-sign');

        $.ajax({
            type: 'POST',
            url: '/User/AddRemoveFromBookmark',
            data: {
                bookmark: bookmark,
                animeId: animeId
            },
            success: function (result) {
                updateBookmarkSign(bookmarkSignElement);
            },
            error: function (error) {
                console.error(error);
            }
        });
    });
});
