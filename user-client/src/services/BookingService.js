class BookingService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}booking`
    }
    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    getAll() {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }
    getData() {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getData`);
    }
    getCitiesByDay(bookingId){
        let self = this;
        return self.axios.get(`${self.baseUrl}/getCitiesByDay/${bookingId}`);
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

export default BookingService