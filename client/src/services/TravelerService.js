function TravelerService(axios, baseUrl) {
  this.axios = axios;
  this.baseUrl = `${baseUrl}traveler`;

  this.get = function (id) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/${id}`);
  }

  this.getAll = function () {
    let self = this;
    return self.axios.get(`${self.baseUrl}`);
  }

  this.getByBooking = function (bookingId) {
    let self = this;
    return self.axios.get(`${self.baseUrl}/getByBooking/${bookingId}`);
  }
  this.getAccommodationCount = function () {
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