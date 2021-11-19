<template>
<div class="schedule clearAfterFloats" :style="{'padding-bottom': 45 +'px'}" >
  <!-- <div class="title-bar">
          schedule
    </div> -->
    <v-dialog v-model="isDialog" width="40%" persistent>
        <div class="dialogContent">
            <v-icon class="close-icon" @click="isDialog = false">close</v-icon>
            <app-item-create-update 
            :schedule="selectedSchedule"
            :isUpdate="isScheduleItemUpdate"
            @createScheduleItem="createScheduleItem"
            @updateScheduleItem="updateScheduleItem"
            ></app-item-create-update>
        </div>
    </v-dialog>

    <app-single 
    class="schedule-item"
    v-for="schedule in schedules.ScheduleItems" 
    :key="schedule.ScheduleId" 
    @parseSelectedSchedule="parseSelectedSchedule"
    :schedule="schedule"></app-single>

    <div class="schedule-actions clearAfterFloats d-flex align-center" :style="{'bottom': 0 +'px'}">
        <div @click="changeIsCreateNew" class="text-left pl-5" style="cursor: pointer">
              <v-icon class="d-inline-block mr-2" style="float:left" color="black">control_point</v-icon>
              <span class="text-left d-inline-block mt-1">Add schedule item</span>
        </div>
        <div style="text-align: right; padding-right: 2rem;">
            <v-btn round v-if="showSaveChanges" class="primary" @click="updateSchedule">Save changes</v-btn>
        </div>
    </div>
</div>
</template>
<script>
import Single from "./item/Single";
import ItemCreateOrUpdate from "./item/CreateOrUpdate";
export default {
  props: ["scheduleId","bottom"],
  components: {
    appSingle: Single,
    appItemCreateUpdate: ItemCreateOrUpdate
  },
  data() {
    return {
      schedules: {
        ScheduleId: 0,
        ScheduleItems: []
      },
      selectedSchedule: {},
      showSaveChanges: false,
      isDialog: false,
      isScheduleItemUpdate: false
    };
  },
  methods: {
    updateScheduleItem(scheduleItem) {
      var scheduleItems = this.schedules.ScheduleItems;
      var index = -1;
      scheduleItems.forEach((item,i) => {
        if (item.ScheduleItemId == scheduleItem.ScheduleItemId) {
          index = i;
        }
      });
      this.schedules.ScheduleItems.splice(index, 1);
      this.schedules.ScheduleItems.splice(index, 0, scheduleItem);
      this.selectedSchedule = {};
      this.isDialog = false;
      this.showSaveChanges = true;
    },
    createScheduleItem(schedule) {
      this.schedules.ScheduleItems.push(schedule);
      this.showSaveChanges = true;
      this.isDialog = false;
    },
    parseSelectedSchedule(schedule) {
      this.selectedSchedule = schedule;
      this.isScheduleItemUpdate = true;
      this.isDialog = true;
    },
    getScheduleList() {
      this.$store.state.services.scheduleService
        .get(this.scheduleId)
        .then(response => {
          console.log(this.schedules);
          this.schedules = response.data;
        });
    },
    changeIsCreateNew() {
      this.isScheduleItemUpdate = false;
      this.isDialog = true;
    },
    updateSchedule() {
      this.schedules.ScheduleId = this.scheduleId;
      this.$store.state.services.scheduleService
        .update(this.schedules)
        .then(response => {
          this.getScheduleList();
          this.showSaveChanges = false;
        });
    }
  },
  created() {
    this.getScheduleList();
  },
  watch: {
    isDialog() {
      if (!this.isDialog) {
        this.selectedSchedule = {};
      }
    }
  }
};
</script>

<style scoped>
.dialogContent {
  background-color: white;
  padding: 30px;
  position: relative;
}
.close-icon{
  position:absolute;
  top: 15px;
  right: 15px;
}
.schedule {
  width: 100%;
  height: 100%;
  min-height: 420px;
  max-height: 550px;
  overflow-y: auto;
}

.actions {
  position: absolute;
  width: 100%;
  bottom: 10px;
}

.actions div {
  width: 50%;
}

.schedule-actions {
  position: absolute;
  width: 100%;
  height: 50px;
  background-color: white;
  padding-top: .8rem;
  padding-bottom: .8rem;
  border-top: 1px solid rgba(0,0,0,.3);
}
.schedule-actions div {
  float: left;
  width: 50%;
}
.schedule-item:nth-child(odd){
  background-color: #f7f7f7;
}
</style>
