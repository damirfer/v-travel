class Schedule {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}schedule`
    }
    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    getByTour(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/GetByTour/${id}`);
    }
}

export default Schedule