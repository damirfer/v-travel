function BookingService(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}booking`;

    this.get = function(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    this.getAll = function() {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    this.add = function(model) {
        let self = this;
        return self.axios.post(`${self.baseUrl}`, model);
    }

    this.update = function(model) {
        let self = this;
        return self.axios.put(`${self.baseUrl}`, model);
    }

    this.remove = function(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}/${id}`);
    }

    this.getByIndex = function(index) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByIndex/${index}`);
    }

    this.getByName = function(name) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByName/${name}`);
    }

    this.getBookingsCount = function() {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getCount`);
    }
}

export default BookingService