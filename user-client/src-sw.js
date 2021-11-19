importScripts(
  "https://storage.googleapis.com/workbox-cdn/releases/3.5.0/workbox-sw.js"
);

workbox.routing.registerRoute(
  new RegExp("https://sighter-travel.azurewebsites.net/"),
  workbox.strategies.cacheFirst()
);

workbox.routing.registerRoute(
  new RegExp("https://fonts.(?:googleapis|gstatic).com/(.*)"),
  workbox.strategies.cacheFirst({
    cacheName: "google-fonts",
    plugins: [
      new workbox.expiration.Plugin({
        maxEntries: 30
      }),
      new workbox.cacheableResponse.Plugin({
        statuses: [0, 200]
      })
    ]
  })
);

workbox.routing.registerRoute(
  new RegExp("https://maps.googleapis.com/maps/api/js?key=AIzaSyD_YqzkcMD1K9UOwcxdBe8Ftiur8Wt43jY&libraries=places"),
  workbox.strategies.cacheFirst({
    cacheName: "google-maps",
    plugins: [
      new workbox.expiration.Plugin({
        maxEntries: 30
      }),
      new workbox.cacheableResponse.Plugin({
        statuses: [0, 200]
      })
    ]
  })
);

workbox.routing.registerRoute(
  new RegExp("https://maps.googleapis.com/(.*)"),
  workbox.strategies.cacheFirst({
    cacheName: "google-places",
    plugins: [
      new workbox.expiration.Plugin({
        maxEntries: 30
      }),
      new workbox.cacheableResponse.Plugin({
        statuses: [0, 200]
      })
    ]
  })
);
workbox.precaching.precacheAndRoute([]);

// {
//   "url": "static/js/manifest.json",
//   "revision": "719965303b0177f201f52eaf598sdffd"
// }