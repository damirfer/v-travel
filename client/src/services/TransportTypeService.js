function TravelerService(axios, baseUrl) {
  this.axios = axios;
  this.baseUrl = `${baseUrl}transportType`;

  this.get = function (id) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/${id}`);
  }
  this.getByIndex = function (index) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/getByIndex/${index}`);
  }
  this.getByName = function (name) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/getByName/${name}`);
  }
  this.getAll = function () {
    let self = this;
    return self.axios.get(`${self.baseUrl}`);
  }

  this.getCount = function () {
    let self = this;
    return self.axios.get(`${self.baseUrl}/getCount`);
  }

  this.add = function (model) {
    let self = this;
    return self.axios.post(`${self.baseUrl}`, model);
  }

  this.update = function (model) {
    let self = this;
    return self.axios.put(`${self.baseUrl}`, model);
  }

  this.remove = function (model) {
    let self = this;
    return self.axios.post(`${self.baseUrl}/Delete`, model, {
      headers: {
        'Content-Type': 'application/json',
      }
    });
  }
}

export default TravelerService