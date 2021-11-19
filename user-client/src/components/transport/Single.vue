<template>
  <div>
    <div v-if="transports.TransportList.length > 0">
      <p class="cityName">{{ transports.CityName }}</p>
      <div
        v-for="transport in transports.TransportList"
        :key="transport.Name"
        class="header-image"
        :style="{ backgroundImage: 'url(' + transport.PhotoUrl + ')' }"
      >
        <div class="black-overlay"></div>
        <a :href="'tel:' + transport.Phone" class="clickable-container">
          <p class="name">{{ transport.Name }}</p>
          <div class="transport-info">
            <img
              src="https://img.icons8.com/material/24/000000/phone--v1.png"
            />
            <p>click to call</p>
            <a class="phoneNumber" :href="'tel:' + transport.Phone">{{
              transport.Phone
            }}</a>
          </div>
        </a>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["transports"],
};
</script>
<style scoped>
.transport {
  box-shadow: 0 0 15px rgba(153, 153, 153, 0.35);
  margin: 20px 0;
}
.header-image {
  height: 250px;
  position: relative;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  border-radius: 10px;
  margin-bottom: 10px;
}
.black-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.4);
  border-radius: 10px;
}
.clickable-container {
  display: block;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  text-align: center;
  color: white;
}
.cityName {
  display: inline-block;
  margin: 15px auto;
  position: relative;
  left: 50%;
  transform: translateX(-50%);
  text-transform: uppercase;
  font-size: 17px;
}
.cityName::after {
  content: "";
  display: block;
  margin-left: -10%;
  width: 120%;
  height: 1px;
  background-color: #0284a8;
}
.clickable-container .name {
  position: absolute;
  font-size: 28px;
  font-weight: bold;
  margin-top: 10px;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.transport-content {
  padding: 20px 40px;
  box-sizing: border-box;
  display: flex;
  align-items: center;
}
.divider {
  float: left;
  height: 56px;
  width: 1px;
  margin: 0 25px;
  background-color: #535151;
  border-radius: 50%;
}
.transport-info {
  width: 100%;
  position: absolute;
  bottom: 10px;
  text-align: center;
}
.transport-info img {
  width: 30px;
}
.transport-info p {
  font-size: 12px;
  margin-top: 0;
  margin-bottom: 2px;
}
.transport-info a {
  font-size: 19px;
  text-decoration: none;
  color: white;
  letter-spacing: 0.08em;
}
</style>
