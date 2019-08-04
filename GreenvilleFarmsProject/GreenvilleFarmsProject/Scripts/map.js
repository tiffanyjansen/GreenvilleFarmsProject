/*
 * This script will add the map centered at Greenville Farms,
 * with special Tractor Icon, with a content box that appears when
 * the icon is clicked on.
 */ 

var map;
function initMap() {
    // The location of the farm
    var farm = { lat: 45.592590, lng: -123.125160 };

    // The map, centered at the farm
    var map = new google.maps.Map(document.getElementById('map'), { zoom: 10, center: farm });

    // Get the tractor icon
    var icon = "/Content/Images/tractor.svg";

    // The marker, positioned at the farm, with the tractor icon.
    var marker = new google.maps.Marker({
        position: farm,
        map: map,
        icon: icon,
        title: 'Greenville Farms (Oregon)'
    });

    // The html for the 'popup' box
    var contentString = '<div class="card border-warning">' +
        '<div class="card-body" style="text-align:center;">' +
        '<h4>Greenville Farms</h4>' +
        '<h7>43500 NW Greenville Rd, Forest Grove, OR 97116</h7>' +
        '<br>' +
        '<a href="https://goo.gl/maps/RGwWQKeWBCwYdLaCA" target="_blank">Get Directions</a>'
    '</div>' +
        '</div>';

    // The 'popup' box
    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    // Add listener for when a user clicks the marker.
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
}