function DashboardService(axios, baseUrl) {
  this.axios = axios;
  this.baseUrl = `${baseUrl}dashboard`;

  this.get = function () {
    let self = this;
    return self.axios.get(`${self.baseUrl}`);
  }
}

export default DashboardService
