class Accommodation {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}accommodation`
    }
    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }
    getForMobile(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getForMobile/${id}`);
    }

    getAll() {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    getByCity(cityId) {
        //TODO -> Naći drugi način slanja liste integera
        let self = this;
        var brokenId ="?";
        cityId.forEach(element => {
            brokenId += "id=" + element + "&";
        });
        brokenId = brokenId.substring(0,brokenId.length - 1);
        return self.axios.get(`${self.baseUrl}/getByCity/${brokenId}`);
    }

    add(model) {
        let self = this;
        return self.axios.post(`${self.baseUrl}`, model);
    }

    update(model) {
        let self = this;
        return self.axios.put(`${self.baseUrl}`, model);
    }

    remove(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}/${id}`);
    }
}

export default Accommodation