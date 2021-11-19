import jsonToFormData from 'json-form-data'

function Day(axios, baseUrl) {
    this.axios = axios;
    this.baseUrl = `${baseUrl}day`;

    this.get = function (id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/${id}`);
    }
    this.getListItem = function (id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}/getListItem/${id}`);
    }

    this.getAll = function () {
        let self = this;
        return self.axios.get(`${self.baseUrl}`);
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
        // Form data salje 1 ako je true ali na back-u taj property cita kao false - JAKO CUDNO!!
        if (formData.get("IsBIncluded") == "1") {
            formData.delete("IsBIncluded");
            formData.append("IsBIncluded", true);
        }
        if (formData.get("IsLIncluded") == "1") {
            formData.delete("IsLIncluded");
            formData.append("IsLIncluded", true);
        }
        if (formData.get("IsDIncluded") == "1") {
            formData.delete("IsDIncluded");
            formData.append("IsDIncluded", true);
        }
        return self.axios.put(`${self.baseUrl}`, formData);
    }

    this.remove = function (id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}/${id}`);
    }
}

export default Day