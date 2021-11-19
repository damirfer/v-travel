class TourService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}tour`
    }
    getTourByBooking(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/byBookingForMobile/${id}`);
    }

    getInfo(bookingId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getInfo/${bookingId}`);
    }

}

export default TourService