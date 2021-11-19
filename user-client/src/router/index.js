import Vue from "vue";
import Router from "vue-router";

//Components
//login
import Login from "../components/Login.vue"

//not supported screen
import NotSupportedScreen from "../components/NotSupportedScreen.vue"

//TourOverview
import TourOverview from "../components/tour/TourOverview.vue";

//Explore
import Explore from "../components/explore/Index.vue";
import ExploreList from "../components/explore/List.vue";

//Tour info
import TourInfo from "../components/tour/Info.vue";

//Country
import CountryDetails from "../components/country/SingleDetails.vue";

//Transport
import TransportIndex from "../components/transport/Index.vue";
import TransportList from "../components/transport/List.vue";

//Place
import PlaceDetails from "../components/place/SingleDetails.vue";

//Chat
import Chat from "../components/chat/Chat.vue"

Vue.use(Router);


const routes = [
  {
    path: "/",
    name: "Login",
    component: Login
  },
  {
    path: "/NotSupportedScreen",
    name: "NotSupportedScreen",
    component: NotSupportedScreen
  },
  {
    path: "/TourOverview",
    name: "TourOverview",
    component: TourOverview
  },
  {
    path: "/Explore",
    name: "ExploreIndex",
    component: Explore
  },
  {
    path: "/ExploreList/:id",
    name: "ExploreList",
    component: ExploreList
  },
  {
    path: "/TourInfo",
    name: "TourInfo",
    component: TourInfo
  },
  {
    path: "/CountryDetails/:id",
    name: "CountryDetails",
    component: CountryDetails
  },
  {
    path: "/Transport",
    name: "TransportIndex",
    component: TransportIndex
  },
  {
    path: "/TransportList/:id",
    name: "TransportList",
    component: TransportList
  },
  {
    path: "/PlaceDetails/:id",
    name: "PlaceDetails",
    component: PlaceDetails
  },
  {
    path: "/Chat",
    name: "Chat",
    component: Chat
  }
];
const router = new Router({
  routes
});
router.beforeEach((to, from, next) => {
  if (window.outerWidth > 768 && to.path !== '/NotSupportedScreen') {
    next('/NotSupportedScreen');
  } else {
    next();
  }
})
export default router;
