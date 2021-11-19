function Schedule(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}schedule`;

    this.get = function (id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    this.getAll = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    this.add = function (model) {
        let self = this;
        return self.axios.post(`${self.baseUrl}`, model);
    }

    this.update = function (model) {
        let self = this;
        return self.axios.put(`${self.baseUrl}`, model);
    }

    this.remove = function (id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}/${id}`);
    }
}

export default Schedule