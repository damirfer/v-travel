<template>
    <div class="section">
        <div class="title" @click="isContentOpen = !isContentOpen">
            <span>{{ section.Title }}</span>
            <span v-if="!isContentOpen">
                <img :src="require('../assets/icons/angle-down.png')" alt="Open">
            </span>
            <span v-else>
                <img :src="require('../assets/icons/angle-up.png')" alt="Close">
            </span>
        </div>
        <div class="content" v-if="isContentOpen">
            <p>{{ section.Content }}</p>
        </div>
    </div>
</template>
<script>
export default {
  props: ["section"],
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
</style>
