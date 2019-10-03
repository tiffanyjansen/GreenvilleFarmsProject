$(document).ready(function () {
    $('a').each(function ()
    {
        if ($(this).prop('href') == window.location.href)
        {
            if ($(this).hasClass("nav-link")) {
                $(this).addClass('active');
            }           
        }
    });
});