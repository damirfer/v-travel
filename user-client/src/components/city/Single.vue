<template>
    <div class="section">
        <div class="title" @click="isContentOpen = !isContentOpen">
            <span>{{ city.Name }}</span>
            <span v-if="!isContentOpen">
                <img :src="require('../../assets/icons/angle-down.png')" alt="Open">
            </span>
            <span v-else>
                <img :src="require('../../assets/icons/angle-up.png')" alt="Close">
            </span>
        </div>
        <div class="content" v-if="isContentOpen">
            <div 
            class="city-photo"
            :style="{ backgroundImage: 'url(' + city.PhotoUrl + ')' } "
            >

            </div>
            <p>{{ city.GeneralInfo }}</p>
        </div>
    </div>
</template>
<script>
export default {
  props: ["city"],
  data() {
    return {
      isContentOpen: false
    };
  }
};
</script>
<style scoped>
.section{
    margin: 3px 0;
    font-size: 16px;
}
.title{
    padding: 10px 15px;
    font-size: 'Montserrat', sans-serif;
}

.title span{
  font-family: 'Montserrat', sans-serif;
}

.section .title span:nth-child(1) {
  font-size: 1.2em;
}
.section .title span:nth-child(2) {
  float: right;
  margin-right: 10px;
}
.section .title span:nth-child(2) img {
  height: 13px;
}
.content {
  padding: 10px 15px;
  transition: all 1s;
  line-height: 140%;
  font-weight: 300;
}
.city-photo{
    height: 40vh;
    background-size: cover;
    background-position: center;
    border-radius: 10px;
    margin-bottom: 35px;
    box-shadow: 0 0 15px rgba(153, 153, 153, 0.35);
}
</style>
