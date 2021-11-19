<template>
  <div id="login-container">
    <div id="form-content">
      <div v-if="keyboardClosed">
        <h1 class="sighter-login-title">LOGIN</h1>
        <h2 class="sighter-login-message">
          Please enter provided information.
        </h2>
      </div>
      <span class="error-message" v-if="isLoginFailed"
        >Provided login informations are not valid.</span
      >
      <input
        @focus="keyboardClosed = false"
        @blur="keyboardClosed = true"
        type="text"
        placeholder="Username"
        v-model="loginObject.Username"
      />

      <input
        @focus="keyboardClosed = false"
        @blur="keyboardClosed = true"
        type="password"
        placeholder="Code"
        v-model="loginObject.Password"
      />
      <button @click="Login">Login</button>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loginObject: {
        Username: "",
        Password: "",
      },
      isLoginFailed: false,
      token: null,
      keyboardClosed: true,
    };
  },
  methods: {
    Login() {
      this.$store.state.services.homeService
        .login(this.loginObject)
        .then((response) => {
          localStorage.setItem("user-token", response.data.token);
          localStorage.setItem("login-success", "true");
          this.$store.state.services.setAxiosHeader();
          this.getBookingData();
          this.isLoginFailed = false;
        })
        .catch((err) => {
          this.isLoginFailed = true;
        });
    },
    getBookingData() {
      this.$store.state.services.bookingService.getData().then((response) => {
        localStorage.setItem("tourId", response.data.TourId);
        localStorage.setItem("bookingId", response.data.BookingId);
        localStorage.setItem("traveler", response.data.TravelerName);
        this.$router.push({
          name: "TourOverview",
        });
      });
    },
  },
  created() {
    if (localStorage.getItem("login-success") === "true") {
      this.$router.push({
        name: "TourOverview",
      });
    }
  },
};
//TEST
</script>

<style scoped>
#black-overlay {
  height: 100%;
  width: 100%;
  position: absolute;
  z-index: 0;
  background-color: rgba(0, 0, 0, 0.4);
}

#login-container {
  background-color: #f1f2f2;
  height: 100vh;
  text-align: center;
  position: relative;
  padding-top: 20vh;
}

#login-container div {
  margin: 0 0;
}

#form-content {
  padding: 0 30px;
  height: 100vh;
  background-color: #f1f2f2;
  border: none;
}
.logo-bottom-space {
  margin-bottom: 50px;
}
.sighter-login-title {
  font-size: 70px;
  color: #0284a8;
  text-align: left;
  margin: 25px 0 0 0;
}
.sighter-login-message {
  font-weight: 300;
  color: #999;
  font-size: 14px;
  text-align: left;
  margin-bottom: 25%;
  margin-top: 0;
  margin-left: 5px;
  font-family: "Roboto Slab", serif;
}

#form-content input {
  font-size: 19px;
  width: 100%;
  background-color: transparent;
  border: none;
  border-bottom: 1px solid #0284a8;
  margin-bottom: 30px;
  color: #0284a8;
  outline: none;
}
#form-content button {
  font-size: 19px;
  text-transform: uppercase;
  font-weight: 600;
  color: white;
  outline: none;
  border: 1px solid #0284a8;
  margin-top: 20px;
}
#form-content button {
  background: #0284a8;

  border-radius: 20px;
  padding: 0.5em 2.5em;
}

#sighter-logo {
}
#sighter-logo img {
  width: 100px;
  margin-top: 20px;
}

@media only screen and (min-width: 1024) {
  #login-container {
    width: 30%;
    height: 70vh;
    margin: 15vh auto 0;
    overflow: hidden;
    box-shadow: 0 0 25px rgba(0, 0, 0, 0.2);
    border-radius: 10px;
  }
}
@media only screen and (max-width: 330px) {
  .sighter-login-title {
    font-size: 25px;
  }
  .sighter-login-message {
    font-size: 14px;
  }
}
</style>
