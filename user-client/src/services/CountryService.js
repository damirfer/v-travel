class CountryService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}country`
    }
    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    getAll() {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }
    getInfo(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getInfo/${id}`);
    }
} 
export default CountryService


