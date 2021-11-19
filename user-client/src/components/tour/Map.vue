<template>
  <div class="full-height">
    <div id="map">
      <iframe
        src="https://www.google.com/maps/d/u/0/embed?mid=122mezo1CRDJYYQhOef_-qrhDTQS_v9d8"
        width="400"
        height="640"
      ></iframe>
    </div>
  </div>
</template>

<script>
export default {
  props: ["tourCities", "isMapTriggered"],
  data() {
    return {
      userCoorPath: [],
    };
  },
  methods: {
    setTourMap() {
      let self = this;
      var map;
      var mapOptions = {
        center: new google.maps.LatLng(
          self.tourCities[0].Latitude,
          self.tourCities[0].Longitude
        ),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
      };
      function initialize() {
        map = new google.maps.Map(document.getElementById("map"), mapOptions);
        var userCoor = [];
        var userCoorPath = [];
        //Get lat and long by name of city
        var lat,
          long,
          i = 0;
        var marker, i;
        for (i; i < self.tourCities.length; i++) {
          var cityAdress = self.tourCities[i].Name;
          lat = self.tourCities[i].Latitude;
          long = self.tourCities[i].Longitude;

          marker = new google.maps.Marker({
            position: new google.maps.LatLng(lat, long),
            map: map,
          });

          google.maps.event.addListener(
            marker,
            "click",
            (function (marker, j) {
              return function () {
                infowindow.setContent(self.tourCities[j].Name);
                infowindow.open(map, marker);
              };
            })(marker, i)
          );
          var infowindow = new google.maps.InfoWindow();
          userCoor.push([self.tourCities[i].Name, lat, long]);
          userCoorPath.push(new google.maps.LatLng(lat, long));
        }
        var userCoordinate = new google.maps.Polyline({
          path: userCoorPath,
          strokeColor: "#212121",
          strokeOpacity: 1,
          strokeWeight: 1,
        });
        userCoordinate.setMap(map);
      }
      initialize();
    },
  },
  watch: {
    isMapTriggered() {
      // this.setTourMap();
    },
  },
};
</script>
<style scoped>
#map {
  height: 100%;
}
</style>
