function review(key) {
    console.log("We have entered the function.");
    var url = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + "ChIJjabqtVQclVQR3B7E_iqiahQ" + "&fields=reviews&key=" + key;
    $.ajax({ //ajax call
        type: "GET",
        dataType: "json",
        url: url,
        success: successAjax,
        error: errorAjax
    });
}

function successAjax(json) {
    var reviews = JSON.parse(json);
    console.log(reviews);
    console.log(reviews.length);
    if (reviews.length > 0) {
        $('#reviews').append('<h1>Google Reviews</h1>');
    }
}

function errorAjax() {
    console.log("There has been an error.");
}