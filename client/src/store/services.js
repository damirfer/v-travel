import Axios from 'axios'
import { router } from '../router'

import authService from "../services/AuthService";
import countryService from '../services/CountryService'
import cityService from '../services/CityService'
import accommodationService from '../services/AccommodationService'
import accommodationTypeService from '../services/AccommodationTypeService'
import amenitiesService from '../services/AmenitiesService'
import tourService from '../services/TourService'
import bookingService from '../services/BookingService'
import dayService from '../services/DayService'
import scheduleService from '../services/ScheduleService'
import guideService from '../services/GuideService'
import dashboardService from '../services/DashboardService'
import languageService from '../services/LanguageService'
import travelerService from '../services/TravelerService'
import placeService from '../services/PlaceService'
import placeTypeService from '../services/PlaceTypeService'
import transportService from '../services/TransportService'
import transportTypeService from '../services/TransportTypeService'




// let apiUrl = 'https://fittraveldemo.azurewebsites.net/'
// let apiUrl = 'https://sighter-travel.azurewebsites.net/'
// let apiUrl = 'https://travel-demo.azurewebsites.net/'
let apiUrl = 'http://localhost:5000/'

// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'

Axios.interceptors.request.use(function (config) {
    // Do something before request is sent
    console.log(config);
    config.headers['Authorization'] = getTokenWithPrefix();
    // config.headers['Content-Type'] = 'application/json';
    // config.headers.common.Accept = "application/json";
    return config;
}, function (error) {
    console.log(error);
    // Do something with request error
    return Promise.reject(error);
});

Axios.interceptors.response.use(function (response) {
    console.log(response);


    return response;
}, function (error) {
    const { status } = error.response;
    if (status === 401 || status === 403) {
        router.push({ name: 'Login' });
    }

    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    return Promise.reject(error);
});

function getTokenWithPrefix() {
    let token = localStorage.getItem("user-token");
    if (token) {
        return `Bearer ${token}`;
    } else {
        return '';
    }
};


export default {
    authService: new authService(Axios, apiUrl),
    countryService: new countryService(Axios, apiUrl),
    cityService: new cityService(Axios, apiUrl),
    accommodationService: new accommodationService(Axios, apiUrl),
    accommodationTypeService: new accommodationTypeService(Axios, apiUrl),
    amenitiesService: new amenitiesService(Axios, apiUrl),
    tourService: new tourService(Axios, apiUrl),
    bookingService: new bookingService(Axios, apiUrl),
    dayService: new dayService(Axios, apiUrl),
    scheduleService: new scheduleService(Axios, apiUrl),
    guideService: new guideService(Axios, apiUrl),
    dashboardService: new dashboardService(Axios, apiUrl),
    languageService: new languageService(Axios, apiUrl),
    travelerService: new travelerService(Axios, apiUrl),
    placeService: new placeService(Axios, apiUrl),
    placeTypeService: new placeTypeService(Axios, apiUrl),
    transportService: new transportService(Axios, apiUrl),
    transportTypeService: new transportTypeService(Axios, apiUrl),

    setAxiosHeader() {
        //Token header configuration
        Axios.defaults.headers.common[
            "Authorization"
        ] = `Bearer ${localStorage.getItem("user-token")}`;
    },
    resetAxiosHeader() {
        Axios.defaults.headers.common[
            "Authorization"
        ] = "";
    },
    getTokenWithPrefix() {
        let token = localStorage.getItem("user-token");
        if (token) {
            return `Bearer ${token}`;
        } else {
            return '';
        }
    },
    checkRole(role) {
        var hasRole = false;
        var roles = localStorage.getItem("user-roles").split(",");
        roles.forEach(r => {
            if (r == role) {
                hasRole = true;
                return;
            }
        });
        return hasRole;
    }

}