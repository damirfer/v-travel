<template>
    <div
     class="schedule-single clearAfterFloats"
     @mouseout="changeBtnDisplay('none')"
     @mouseover="changeBtnDisplay('block')"
     >
        <div class="left">
            <span>{{ schedule.TimeStamp }}</span>
        </div>
        <div class="left">
            <p>
              {{ schedule.Content.substring(0,40) }}
              <span v-if="schedule.Content.length > 40">...</span>
            </p>
        </div>
        <v-icon 
        class="btn" 
        @click="parseSelectedSchedule"
        :style="{ display: btnDisplay }">edit</v-icon>
    </div>
</template>
<script>
export default {
  props: ["schedule"],
  data() {
    return {
      btnDisplay: "none"
    };
  },
  methods: {
    parseSelectedSchedule() {
      this.$emit("parseSelectedSchedule", this.schedule);
    },
    changeBtnDisplay(display) {
      this.btnDisplay = display;
    }
  }
};
</script>

<style scoped>
.btn {
  display: none;
  position: absolute;
  right: 5px;
  top: 50%;
  transform: translateY(-50%);
}
.schedule-single {
  position: relative;
  margin: 10px 0;
  padding: 10px 0;
}
.schedule-single div:nth-child(1) {
  width: 20%;
}
.schedule-single div:nth-child(2) {
  width: 80%;
}
</style>

