import jsonToFormData from 'json-form-data'


function CountryService(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}country`;

    this.get = function (id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    this.getAll = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    this.getByIndex = function (index) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByIndex/${index}`);
    }
    this.getByName = function (name) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByName/${name}`);
    }
    this.getCountriesCount = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getCount`);
    }
    this.getByTour = function (tourId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByTour/${tourId}`);
    }

    this.add = function (model, file) {
        let self = this;
        var formData = new FormData();
        formData = jsonToFormData(model);

        if (file != undefined) {
            formData.append(file.name, file);
        }
        return self.axios.post(`${self.baseUrl}`, formData);
    }

    this.update = function (model, file) {
        let self = this;
        var formData = new FormData();
        formData = jsonToFormData(model);

        if (file != undefined) {
            formData.append(file.name, file);
        }
        return self.axios.put(`${self.baseUrl}`, formData);
    }

    this.remove = function (id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}/${id}`);
    }
}

export default CountryService

//formdata.append('file', JSON.stringify(uploadJson));

