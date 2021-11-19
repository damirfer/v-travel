class PlaceService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}place`
    }
    get(placeId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${placeId}`);
    }
    getByTour(tourId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByTour/${tourId}`);
    }
}

export default PlaceService