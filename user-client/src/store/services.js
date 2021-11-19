import Axios from 'axios'

import homeService from '../services/HomeService'
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
import transportService from '../services/TransportService'
import placeService from '../services/PlaceService'


let apiUrl = 'https://fittraveldemo.azurewebsites.net/'
// let apiUrl = 'https://sighter-travel-admin.com/'
// let apiUrl = 'http://localhost:60000/'
// let apiUrl = 'https://192.168.0.42:5001/'


// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'
//Token header configuration
Axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('user-token')}`;

export default {
  homeService: new homeService(Axios, apiUrl),
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
  transportService: new transportService(Axios, apiUrl),
  placeService: new placeService(Axios, apiUrl),

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
}