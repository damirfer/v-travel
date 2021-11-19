class CityService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}city`
    }
    get(id) {
        let self = this;
        console.log(id);
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    getAll() {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    getByCountry(id) {
        //TODO -> Naći drugi način slanja liste integera
        let self = this;
        var brokenId ="?";
        id.forEach(element => {
            brokenId += "id=" + element + "&";
        });
        brokenId = brokenId.substring(0,brokenId.length - 1);
        return self.axios.get(`${self.baseUrl}/getByCountries/${brokenId}`);
    }

}

export default CityService