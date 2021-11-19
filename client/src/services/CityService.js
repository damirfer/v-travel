import jsonToFormData from 'json-form-data'

function CityService(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}city`;

    this.get = function (id) {
        let self = this;
        console.log(id);
        return self.axios.get(`${self.baseUrl}/${id}`);
    }

    this.getAll = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
    }

    this.getByCountry = function (id) {
        //TODO -> Naći drugi način slanja liste integera
        let self = this;
        var brokenId = "?";
        id.forEach(element => {
            brokenId += "id=" + element + "&";
        });
        brokenId = brokenId.substring(0, brokenId.length - 1);
        return self.axios.get(`${self.baseUrl}/getByCountries/${brokenId}`);
    }


    this.getByIndex = function (index) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByIndex/${index}`);
    }

    this.getByName = function (name) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getByName/${name}`);
    }

    this.getCitiesCount = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getCount`);
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

export default CityService