<template>
  <div id="chat-container" ref="chat">
    <div v-if="showSendMessage" class="chat-header-container">
      <p ref="chatHeader" class="chat-header">Chat</p>
      <div class="header-line"></div>
    </div>
    <div>
      <div ref="messageContainer">
        <div
          v-for="message in messages"
          :key="message.id"
          class="message-container"
        >
          <div v-if="message.name == traveler">
            <p class="name text-right">You</p>
            <div class="content content-right">
              <p>{{ message.content }}</p>
              <p class="text-right time">
                {{ formatTime(new Date(message.timestamp)) }}
              </p>
            </div>
          </div>
          <div v-else>
            <p class="name">{{ message.name }}</p>
            <div class="content content-left">
              <p>{{ message.content }}</p>
              <p class="text-right time">
                {{ formatTime(new Date(message.timestamp)) }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div
        v-if="!showSendMessage"
        class="newMessage"
        :style="{ bottom: bottom, height: newMessageHeight }"
      >
        <appNewMessage
          @newMessage="scrollToBottom"
          :height="newMessageHeight"
          @messageSent="messageSent"
        />
      </div>
    </div>
    <div
      @click="showMessageContainer"
      class="show-message-container"
      v-if="showSendMessage"
    >
      <img src="https://img.icons8.com/color/100/333333/sent.png" />
    </div>
  </div>
</template>

<script>
import NewMessage from "./NewMessage";
import db from "@/firebase/init";
export default {
  components: {
    appNewMessage: NewMessage,
  },
  data() {
    return {
      messages: [],
      traveler: "",
      bottom: "0px",
      showSendMessage: true,
      newMessageHeight: "",
    };
  },
  methods: {
    messageSent() {
      this.bottom = "-7vh";
      this.showSendMessage = true;
    },
    showMessageContainer() {
      this.bottom = "0";
      this.showSendMessage = false;
    },
    formatTime(date) {
      var hours = date.getHours();
      var minutes = date.getMinutes();
      var ampm = hours >= 12 ? "pm" : "am";
      hours = hours % 12;
      hours = hours ? hours : 12; // the hour '0' should be '12'
      minutes = minutes < 10 ? "0" + minutes : minutes;
      var strTime = hours + ":" + minutes + " " + ampm;

      return strTime;
    },
    scrollToBottom() {
      this.$refs.messageContainer.scrollIntoView(false);
    },
  },
  created() {
    this.traveler = localStorage.getItem("traveler");
    let ref = db
      .collection(localStorage.getItem("bookingId"))
      .orderBy("timestamp");

    ref.onSnapshot((snapshot) => {
      snapshot.docChanges().forEach((change) => {
        if (change.type == "added") {
          let doc = change.doc;
          console.log(doc.data());
          this.messages.push({
            id: doc.id,
            name: doc.data().name,
            content: doc.data().content,
            timestamp: doc.data().timestamp,
          });
        }
      });
      this.$nextTick(() => {
        this.scrollToBottom();
      });
    });
  },
  mounted() {
    this.newMessageHeight = this.$refs.chatHeader.clientHeight + "px";
  },
};
</script>

<style scoped>
#chat-container {
  height: 100vh;
  padding-bottom: 8vh;
  padding-top: 6vh;
  overflow-y: scroll;
  position: relative;
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

.message-container {
  padding: 5px 20px;
}
.name {
  margin-bottom: 3px;
  font-weight: bold;
  text-transform: capitalize;
}
.content {
  background-color: #e3e3e3;
  padding: 10px;
  margin-top: 0;
  border-radius: 10px;
}
.content-left {
  border-top-left-radius: 0;
  margin-right: 10%;
}
.content-right {
  background-color: #6fbed4;
  color: white;
  border-top-right-radius: 0;
  margin-left: 10%;
}
.newMessage {
  position: fixed;
  bottom: 0;
  z-index: 1000;
  width: 100%;
  background-color: white;
}
.show-message-container {
  width: 45px;
  height: 45px;
  border-radius: 50%;
  position: fixed;
  right: 20px;
  bottom: 11vh;
  background-color: white;
  box-shadow: 0px 2px 25px -10px rgba(0, 0, 0, 0.4);
}
.show-message-container img {
  width: 75%;
  position: relative;
  top: 50%;
  left: 50%;
  transform: translate(-55%, -45%);
}
.time {
  margin-bottom: 0;
}
</style>
