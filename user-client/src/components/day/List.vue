<template>
    <div class="days-container" ref="days">
        
        <app-day-single
        v-for="(day,i) in days"
        :key="i"
        :day="day"></app-day-single>
    </div>
</template>
<script>
import DaySingle from "./Single";
export default {
  props: ["days"],
  components: {
    appDaySingle: DaySingle
  },
  data() {
    return {
      isLoading: true
    };
  },
  created(){
  },
  methods: {
  },
  watch:{
    days(){
    }
  }
};
</script>
<style scoped>
.days-container {
  padding: 0 10px;
  position: relative;
}

</style>
