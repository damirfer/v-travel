<template>
  <div id="newMessage-container">
    <form class="clear-floats" @submit.prevent="addMessage">
      <div class="message-container" :style="{'height' : height }">
        <img
          @click="$emit('messageSent')"
          class="closeBtn"
          src="https://img.icons8.com/material/100/333333/multiply.png"
        >

        <input
          class="message"
          placeholder="Message..."
          type="text"
          name="new-message"
          autocomplete="off"
          v-model="newMessage"
        >
      </div>
      <p v-if="feedback">{{feedback}}</p>
      <div class="send-img" :style="{'height': height }" @click="addMessage">
        <img src="https://img.icons8.com/color/100/333333/sent.png">
      </div>
    </form>
  </div>
</template>

<script>
import db from "@/firebase/init";
export default {
  name: "NewMessage",
  props: ["height"],
  data() {
    return {
      newMessage: null,
      feedback: null
    };
  },
  created() {
  },
  methods: {
    addMessage() {
      if (this.newMessage) {
        this.$emit("messageSent");
        db.collection(localStorage.getItem("bookingId"))
          .add({
            content: this.newMessage,
            name: localStorage.getItem("traveler"),
            timestamp: Date.now()
          })
          .catch(err => {
            console.log(err);
          });
        this.newMessage = null;
        this.feedback = null;
        this.$emit('newMessage');
      } else {
        this.feedback = "You must enter a message";
      }
    }
  }
};
</script>

<style scoped>
#newMessage-container {
  text-align: center;
  width: 100%;
  position: relative;
  top: 50%;
  transform: translateY(-50%);
}
.message-container {
  float: left;
  width: 70%;
  text-align: right;
  position: relative;
}
.closeBtn {
  position: absolute;
  left: 10px;
  width: 17px;
  top: 50%;
  transform: translateY(-50%);
}
.message {
  border-radius: 10px;
  background-color: lightgray;
  border: none;
  padding: 8px 15px;
  display: inline-block;
  margin-top: 1vh;
  outline: none;
}
.send-img {
  width: 30%;
  float: left;
  text-align: left;
  padding-left: 10px;
  padding-bottom: 15px;
}
.send-img img {
  height: 100%;
  margin-top: 1vh;
}
</style>
