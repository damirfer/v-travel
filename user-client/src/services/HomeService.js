class Home {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}home`
    }

    login(model) {
        let self = this;
        return self.axios.post(`${self.baseUrl}/login`, model);
    }

}

export default Home