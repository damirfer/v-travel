<template>
  <div class="place">
    <div @click="closePlaceModal" class="back-arrow-container">
      <img
        @click="closePlaceModal"
        class="back-arrow"
        src="https://img.icons8.com/ios/50/0284a8/left-filled.png"
      />
    </div>
    <div
      class="header"
      :style="{ backgroundImage: 'url(' + place.PhotoUrl + ')' }"
    >
      <div class="black-overlay"></div>
      <div class="header-title">
        <h1>{{ place.Name }}</h1>
        <p>
          Working hours: <span>{{ place.WorkingHours }}</span>
        </p>
      </div>
    </div>
    <p class="title">Description</p>
    <div class="title-line"></div>
    <div class="content">{{ place.Description }}</div>
  </div>
</template>

<script>
export default {
  props: ["placeId"],
  data() {
    return {
      place: {},
    };
  },
  methods: {
    getPlace() {
      this.$store.state.services.placeService
        .get(this.placeId)
        .then((response) => {
          this.place = response.data;
        });
    },
    closePlaceModal() {
      this.$emit("closePlaceModal");
    },
  },
  created() {
    this.getPlace();
  },
};
</script>

<style scoped>
.back-arrow-container {
  height: 35px;
  width: 35px;
  position: absolute;
  top: 0px;
  left: 0px;
  z-index: 1000;
  background-color: white;
  border-radius: 50%;
  margin: 20px 35px 35px 20px;
}

.back-arrow {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 70%;
  transform: translate(-50%, -50%);
  z-index: 1000;
}
.place {
  position: relative;
  padding-bottom: 35px;
}
.black-overlay {
  position: absolute;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 0;
}
.title {
  font-family: "Montserrat", sans-serif;
  font-size: 20px;
  margin: 0;
  color: white;
  padding: 15px;
  color: #0284a8;
  text-transform: uppercase;
  font-weight: 600;
  margin-top: 45px;
}

.title-line {
  width: 60px;
  height: 3px;
  border-radius: 10px;
  background-color: #0284a8;
  margin-top: -11px;
  margin-left: 15px;
  margin-bottom: 15px;
}

.content {
  padding: 10px 15px;
  font-weight: 300;
  line-height: 140%;
}

.header {
  height: 100vh;
  position: relative;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  color: white;
}

.header-title {
  position: absolute;
  width: 100%;
  text-align: center;
  top: 50%;
  transform: translateY(-50%);
}
</style>
