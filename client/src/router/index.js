import Vue from "vue";
import Router from "vue-router";

//Components
import Login from "../components/Login.vue";
import AdminHome from "../components/admin/AdminHome.vue";

//Country
import Countries from "../components/admin/country/List.vue";
import CountryCU from "../components/admin/country/CreateOrUpdate.vue";

//Accomodation
import Accommodations from "../components/admin/accommodation/List.vue";
import AccommodationCU from "../components/admin/accommodation/CreateOrUpdate.vue";

//Tour
import Tours from "../components/admin/tour/List.vue";
import TourCU from "../components/admin/tour/CreateOrUpdate.vue";
import Overview from "../components/admin/tour/Overview.vue";

//Booking
import Bookings from "../components/admin/booking/List.vue";
import BookingsCU from "../components/admin/booking/CreateOrUpdate.vue";


//City
import Cities from "../components/admin/city/List.vue";
import CityCU from "../components/admin/city/CreateOrUpdate.vue";

//Guides
import Guides from "../components/admin/guide/List.vue";
import GuideCU from "../components/admin/guide/CreateOrUpdate.vue";

//Dashboard
import Dashboard from "../components/admin/dashboard/Dash.vue"

//Language
import Language from "../components/admin/language/List.vue"

//Amenity
import Amenity from "../components/admin/accommodation/amenity/List.vue"
import AmenityCU from "../components/admin/accommodation/amenity/CreateOrUpdate.vue"

//Place
import Place from "../components/admin/place/List.vue"
import PlaceCU from "../components/admin/place/CreateOrUpdate.vue"

//Transport
import Transport from "../components/admin/transport/List.vue"
import TransportCU from "../components/admin/transport/CreateOrUpdate.vue"

//Traveler
import Traveler from "../components/admin/booking/traveler/List.vue"

//TransportType
import TransportType from "../components/admin/transportType/List.vue"
import TransportTypeCU from "../components/admin/transportType/CreateOrUpdate.vue"

Vue.use(Router);

const routes = [{
  path: "/",
  name: "Login",
  component: Login
},
{
  path: "/AdminHome",
  name: "AdminHome",
  component: AdminHome,
  children: [

    //Countries
    {
      path: "Countries",
      name: "CountryList",
      component: Countries,
      meta: {
        roleAccessList: 'all',
        title: 'Countries'
      },
    },
    {
      path: "Countries/Edit/:id",
      name: "CountryEdit",
      component: CountryCU,
      meta: {
        roleAccessList: 'all',
        title: 'Country edit',
        isChild: true,
        parentRoute: '/AdminHome/Countries',
        parentName: 'Countries',
      }
    },
    {
      path: "Countries/CreateNew",
      name: "CountryCreate",
      component: CountryCU,
      meta: {
        roleAccessList: 'all',
        title: 'Create new',
        isChild: true,
        parentRoute: '/AdminHome/Countries',
        parentName: 'Countries',
      }
    },


    //Accommodation
    {
      path: "Accommodations",
      name: "AccommodationIndex",
      component: Accommodations,
      meta: {
        roleAccessList: 'all',
        title: 'Accommodations',
      }
    },
    {
      path: "Accommodation/CreateNew",
      name: "AccommodationCreate",
      component: AccommodationCU,
      meta: {
        roleAccessList: 'all',
        title: 'Create new',
        isChild: true,
        parentRoute: '/AdminHome/Accommodations',
        parentName: 'Accommodations',
      }
    },
    {
      path: "Accommodation/Edit/:id",
      name: "AccommodationEdit",
      component: AccommodationCU,
      meta: {
        roleAccessList: 'all',
        title: 'Accommodation edit',
        isChild: true,
        parentRoute: '/AdminHome/Accommodations',
        parentName: 'Accommodations',
      }
    },
    //Amenity
    {
      path: "Amenity/AccommodationAmenity",
      name: "AccommodationAmenity",
      component: Amenity,
      meta: {
        roleAccessList: 'all',
        title: 'Accommodation amenities',
      }
    },
    {
      path: "Amenity/RoomAmenity",
      name: "RoomAmenity",
      component: Amenity,
      meta: {
        roleAccessList: 'all',
        title: 'Room amenities',
      }
    },

    //Tour
    {
      path: "Tour",
      name: "TourIndex",
      component: Tours,
      meta: {
        roleAccessList: 'all',
        title: 'Tours',
      }
    },
    {
      path: "Tour/CreateNew/:isUpdate",
      name: "TourCreate",
      component: TourCU,
      meta: {
        roleAccessList: 'all',
        title: 'Create tour',
        isChild: true,
        parentRoute: '/AdminHome/Tour',
        parentName: 'Tours',
      }
    },
    {
      path: "Tour/Edit/:tourId",
      name: "TourEdit",
      component: TourCU,
      meta: {
        roleAccessList: 'all',
        title: 'Edit tour',
        isChild: true,
        parentRoute: '/AdminHome/Tour',
        parentName: 'Tours',
      }
    },
    {
      path: "Tour/overview/:tourId",
      name: "TourOverview",
      component: Overview,
      meta: {
        roleAccessList: 'all',
        title: 'Overview',
        isChild: true,
        parentRoute: '/AdminHome/Tour',
        parentName: 'Tours',
      }
    },


    //Booking
    {
      path: "Booking",
      name: "BookingIndex",
      component: Bookings,
      meta: {
        roleAccessList: 'all',
        title: 'Bookings'
      }
    },
    {
      path: "Booking/CreateNew",
      name: "BookingCreate",
      component: BookingsCU,
      meta: {
        roleAccessList: 'all',
        title: 'Create new booking',
        isChild: true,
        parentRoute: '/AdminHome/Booking',
        parentName: 'Bookings',
      }
    },
    {
      path: "Booking/Edit/:bookingId",
      name: "BookingEdit",
      component: BookingsCU,
      meta: {
        roleAccessList: 'all',
        title: 'Edit booking',
        isChild: true,
        parentRoute: '/AdminHome/Booking',
        parentName: 'Bookings',
      }
    },
    {
      path: "Booking/Travelers/:bookingId",
      name: "Travelers",
      component: Traveler,
      meta: {
        roleAccessList: 'all',
        title: 'Add Travelers',
        isChild: true,
        parentRoute: '/AdminHome/Booking',
        parentName: 'Bookings',
      }
    },



    //Cities
    {
      path: "Cities",
      name: "CityIndex",
      component: Cities,
      meta: {
        roleAccessList: 'all',
        title: 'Cities',
      }
    },
    {
      path: "Cities/Edit/:cityId",
      name: "CityEdit",
      component: CityCU,
      meta: {
        roleAccessList: 'all',
        title: 'Edit city',
        isChild: true,
        parentRoute: '/AdminHome/Cities',
        parentName: 'Cities',
      }
    },
    {
      path: "Cities/CreateNew/:isUpdate",
      name: "CityCreate",
      component: CityCU,
      meta: {
        roleAccessList: 'all',
        title: 'Create City',
        isChild: true,
        parentRoute: '/AdminHome/Cities',
        parentName: 'Cities',
      }
    },


    //Guides
    {
      path: "Guides",
      name: "GuideList",
      component: Guides,
      meta: {
        roleAccessList: 'admin',
        title: 'Guides',
      }
    }, {
      path: "Guide/Edit/:id",
      name: "GuideEdit",
      component: GuideCU,
      meta: {
        roleAccessList: 'admin',
        title: 'Guide edit',
        isChild: true,
        parentRoute: '/AdminHome/Guides',
        parentName: 'Guides',
      }
    }, {
      path: "Guides/CreateNew",
      name: "GuideCreate",
      component: GuideCU,
      meta: {
        roleAccessList: 'admin',
        title: 'Guide create',
        isChild: true,
        parentRoute: '/AdminHome/Guides',
        parentName: 'Guides',

      }
    },

    //Dashboard
    {
      path: "Dashboard",
      name: "Dashboard",
      component: Dashboard,
      meta: {
        roleAccessList: 'all',
        title: 'Dashboard',
      }
    },

    //Languages
    {
      path: "Languages",
      name: "Language",
      component: Language,
      meta: {
        roleAccessList: 'all',
        title: 'Languages',
      }
    },
    //Places
    {
      path: "Places",
      name: "PlaceList",
      component: Place,
      meta: {
        roleAccessList: 'all',
        title: 'Places',
      }
    },
    {
      path: "Places/CreateNew",
      name: "PlaceCreate",
      component: PlaceCU,
      meta: {
        roleAccessList: 'all',
        title: 'Place create',
        isChild: true,
        parentRoute: '/AdminHome/Places',
        parentName: 'Places',
      }
    },
    {
      path: "Places/Edit/:id",
      name: "PlaceEdit",
      component: PlaceCU,
      meta: {
        roleAccessList: 'all',
        title: 'Place edit',
        isChild: true,
        parentRoute: '/AdminHome/Places',
        parentName: 'Places',
      }
    },

    //Transport
    {
      path: "Transport",
      name: "TransportList",
      component: Transport,
      meta: {
        roleAccessList: 'all',
        title: 'Transports',
      }
    },
    {
      path: "Transport/CreateNew",
      name: "TransportCreate",
      component: TransportCU,
      meta: {
        roleAccessList: 'all',
        title: 'Transport create',
        isChild: true,
        parentRoute: '/AdminHome/Transport',
        parentName: 'Transports',
      }
    },
    {
      path: "Transport/Edit/:id",
      name: "TransportEdit",
      component: TransportCU,
      meta: {
        roleAccessList: 'all',
        title: 'Transport edit',
        isChild: true,
        parentRoute: '/AdminHome/Transport',
        parentName: 'Transports',
      }
    },

    //Transport type
    {
      path: "TransportType",
      name: "TransportTypeList",
      component: TransportType,
      meta: {
        roleAccessList: 'all',
        title: 'Trasport types',
      }
    },
    {
      path: "TransportType/CreateNew",
      name: "TransportTypeCreate",
      component: TransportTypeCU,
      meta: {
        roleAccessList: 'all',
        title: 'Trasport type create',
        isChild: true,
        parentRoute: '/AdminHome/TransportType',
        parentName: 'Transport types',
      }
    },
    {
      path: "TransportType/Edit/:id",
      name: "TransportTypeEdit",
      component: TransportTypeCU,
      meta: {
        roleAccessList: 'all',
        title: 'Trasport type edit',
        isChild: true,
        parentRoute: '/AdminHome/TransportType',
        parentName: 'Transport types',
      }
    }
  ]
}
];

export const router = new Router({
  mode: 'history',
  routes: routes
});

router.beforeEach((to, from, next) => {
  if (to.name !== 'Login' && !checkAccess(to.meta.roleAccessList)) next({ name: 'Login' })
  else next()
})

function checkAccess(roleAccessList) {
  var hasAccess = false;
  if (localStorage.getItem("admin") != null) {
    if (roleAccessList === 'all') {
      return true;
    }
    var admin = localStorage.getItem("admin");
    if (admin) {
      hasAccess = true;
    }

  }
  return hasAccess;
}
