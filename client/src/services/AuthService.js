function Auth(axios, baseUrl) {

  this.axios = axios;
  this.baseUrl = `${baseUrl}auth`;

  this.login = function (user) {
    let self = this;
    return self.axios.post(`${self.baseUrl}/login`, user);
  }
  this.getData = function (token) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/getData`);
  }
}

export default Auth;
