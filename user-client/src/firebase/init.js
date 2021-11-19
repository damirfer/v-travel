  // Initialize Firebase
  import firebase from 'firebase'
  import firestore from 'firebase/firestore'

  var config = {
    apiKey: "AIzaSyD3ta9OBRA9jY2ecOEjfzO5cvYp6vUDmJU",
    authDomain: "travel-chat-8acf6.firebaseapp.com",
    databaseURL: "https://travel-chat-8acf6.firebaseio.com",
    projectId: "travel-chat-8acf6",
    storageBucket: "travel-chat-8acf6.appspot.com",
    messagingSenderId: "556024018259"
  };
  
const firebaseApp = firebase.initializeApp(config);
firebase.firestore().settings({timestampsInSnapshots:true})

 export default firebaseApp.firestore()
