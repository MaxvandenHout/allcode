import * as admin from "firebase-admin"

admin.initializeApp({
    credential: admin.credential.applicationDefault(),
    databaseURL: "https://chatter-35056.firebaseio.com"
  });
  export default admin; 