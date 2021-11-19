class TransportService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}transport`
    }
    byCityAndType(transportTypeId, cityId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/GetByCityAndType/${transportTypeId}/${cityId}`);
    }
    getByTour(tourId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByTour/${tourId}`);
    }
}

export default TransportService