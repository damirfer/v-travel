
function Ammenities(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}amenity`;

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

    this.getByIndex = function (index, typeId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByIndex/${index}/${typeId}`);
    }

    this.getByName = function (name, typeId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByName/${name}/${typeId}`);
    }

    this.getCount = function (typeId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getCount/${typeId}`);
    }
}

export default Ammenities