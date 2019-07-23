$(document).ready(function () {
    $('a').each(function ()
    {
        if ($(this).prop('href') == window.location.href)
        {
            $(this).addClass('active'); $(this).parents('li').addClass('active');
        }
    });
});

//$('#home').click(function () {
//    $('#home').addClass('active');
//    //$('#home a').addClass('active');
//    $('#about').removeClass('active');
//    //$('#about a').removeClass('active');
//    $('#contact').removeClass('active');
//    //$('#contact a').removeClass('active');
//});