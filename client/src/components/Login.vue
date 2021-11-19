<template>
  <div id="background-login">
    <div class="login-overlay"></div>
    <div class="login-container">
      <h1 class="login-title">LOGIN</h1>
      <h3 class="login-subtitle">Please enter your login information</h3>
      <!--<label for="user-name">User name</label>-->
      <div class="input-container">
        <input
          class="login-input"
          type="text"
          name="user-name"
          id="user-name"
          placeholder="Username"
          style="margin-bottom: 10px"
          v-model="user.Username"
        />
        <!--<label class="label-margin" for="user-passowrd">Password</label>-->
        <input
          class="login-input"
          type="password"
          name="user-passowrd"
          id="user-passowrd"
          placeholder="Password"
          @keyup.enter="login"
          v-model="user.Password"
        />
      </div>
      <p class="password-recovery">
        <span class="error-message" v-if="isLoginFailed"
          >Login information incorrect.</span
        >
      </p>
      <v-btn
        @click="login"
        class="login-button login-primary-btn"
        style="letter-spacing: 2px"
      >
        LOGIN
      </v-btn>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      user: {
        Username: "",
        Password: "",
      },
      isLoginFailed: false,
      isLoading: false,
    };
  },
  methods: {
    login() {
      this.isLoading = true;
      this.isLoginFailed = false;
      this.$store.state.services.authService
        .login(this.user)
        .then((result) => {
          localStorage.setItem("user-token", result.data.token);
          // this.$store.state.services.setAxiosHeader();
          this.getUserData(result.data);
          this.isLoginFailed = false;
          this.isLoading = false;
        })
        .catch((err) => {
          this.isLoginFailed = true;
          this.isLoading = false;
        });
      // console.log(this.user);
    },
    getUserData(token) {
      this.$store.state.services.authService
        .getData(token)
        .then((result) => {
          console.log(result.data);
          // localStorage.setItem("user-roles", result.data.UserRoles);
          localStorage.setItem("user", result.data.UserId);
          localStorage.setItem("user-name", result.data.UserName);
          localStorage.setItem("user-profile", result.data.UserProfile);
          localStorage.setItem("admin", result.data.IsAdmin);
          this.$router.push("/AdminHome/Dashboard");
        })
        .catch((err) => {});
    },
  },
  created() {
    let userToken = localStorage.getItem("user-token");
    if (userToken) {
      this.getUserData(userToken);
    }
  },
};
</script>
<style scoped>
@import url("https://fonts.googleapis.com/css?family=Montserrat:300,:400,:500,:700");

#background-login {
  position: relative;
  width: 100%;
  height: 100vh;
  background-image: url("/static/img/login.jpg");
  background-size: cover;

  background-repeat: no-repeat;
  font-family: "Montserrat", sans-serif;
  overflow-x: hidden;
}

.login-input {
  padding: 0.3em 1em;
  font-size: 16px;
  font-family: "Montserrat", sans-serif;
  border-radius: 20px;
  color: #222;
  -webkit-box-shadow: none;
  box-shadow: none;
  outline: none;
  border: 1px solid #999;
  background-color: #fcfcfc;
  width: 70%;
}

.login-container {
  position: absolute;
  top: 50%;
  left: 50%;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  height: 70%;
  width: 25%;
  border-radius: 20px;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  -webkit-box-shadow: 0px 0px 25px rgba(0, 0, 0, 0.25);
  box-shadow: 0px 0px 25px rgba(0, 0, 0, 0.25);
  background-color: #f7f7f7;
  overflow: hidden;
  text-align: center;
}

.sighter-logo {
  position: absolute;
  top: 5%;
  right: 5%;
  height: 55px;
  width: 55px;
}

.login-title {
  position: absolute;
  top: 28%;
  left: 10%;
  font-size: 2.3em;
  font-family: "Montserrat", sans-serif;
  font-weight: 600;
  color: #0284a8;
}

.login-subtitle {
  position: absolute;
  top: 40%;
  left: 10%;
  font-size: 1em;
  font-family: "Montserrat", sans-serif;
  color: #555;
  font-weight: 300;
}

.login-overlay {
  position: absolute;
  height: 100%;
  width: 100%;
  /* FF3.6-15 */
  /* Chrome10-25,Safari5.1-6 */
  background: linear-gradient(
    135deg,
    rgba(255, 255, 255, 0) 0%,
    white 49%,
    white 100%
  );
  /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
  filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#00ffffff', endColorstr='#ffffff',GradientType=1 );
  /* IE6-9 fallback on horizontal gradient */
  opacity: 0.77;
}

.password-recovery {
  position: absolute;
  bottom: 4%;
  left: 5%;
  font-family: "Montserrat", sans-serif;
  color: #555;
}

.error-message {
  color: #bb0000;
}

.input-container {
  position: relative;
  top: 65%;
  left: 50%;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
}

.login-button {
  padding: 0.45em 1.8em;
  font-size: 16px;
  font-weight: 600;
  text-transform: uppercase;
  position: absolute;
  bottom: 5%;
  right: 5%;
  font-family: "Montserrat", sans-serif;
  color: #fefefe;
  border-radius: 20px;
}

.login-primary-btn {
  background: #0284a8;
  /* Old browsers */
  /* FF3.6-15 */
  /* Chrome10-25,Safari5.1-6 */
  background: linear-gradient(135deg, #0284a8 0%, #027595 100%);
  /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
  filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#0284a8', endColorstr='#027595',GradientType=1 );
  -webkit-box-shadow: 5px 5px 25px rgba(2, 132, 168, 0.6);
  box-shadow: 5px 5px 25px rgba(2, 132, 168, 0.6);
}
</style>
