<template>
  <div class="container">
    <div>
      <img src="https://img.icons8.com/ios/50/ffffff/error.png" />
      <p>Sorry, currently we're not supporting devices larger than tablet.</p>
      <p>We're are working really hard on that. Thank you for your patience!</p>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
.container {
  height: 100vh;
  width: 100%;
  position: relative;
  background-color: #0284a8;
  color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.container img {
  margin-bottom: 20px;
}
.container p {
  margin: 5px;
}
</style>
