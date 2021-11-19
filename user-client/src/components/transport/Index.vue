<template>
  <div class="transport-container">
    <div class="chat-header-container">
      <p class="chat-header">Transport</p>
      <div class="header-line"></div>
    </div>
    <transport-list></transport-list>
  </div>
</template>
<script>
import TransportList from "./List";
export default {
  components: { TransportList },
  data() {
    return {};
  },
  methods: {},
};
</script>
<style scoped>
.transport-container {
  height: 100vh;
  padding-top: 10vh;
  padding-bottom: 10vh;
}
.chat-header {
  box-shadow: 0px 2px 25px -10px rgba(0, 0, 0, 0.4);
  height: 7vh;
  text-align: center;
  text-transform: uppercase;
  margin: 0;
  padding-top: 15px;
  position: fixed;
  top: 0;
  width: 100%;
  background-color: #f7f7f7;
  font-size: 18px;
  font-weight: bold;
}
.chat-header::after {
  content: "";
  position: absolute;
  width: 55px;
  height: 2px;
  background-color: #0284a8;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
}
</style>
