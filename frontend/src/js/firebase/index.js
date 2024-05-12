import { initializeApp } from "firebase/app";4

const firebaseConfig = {
  apiKey: "AIzaSyAvmX84OXKXMFnRc2KMs7oZPBJ1zFrxbcE",
  authDomain: "real-estate-dff2d.firebaseapp.com",
  projectId: "real-estate-dff2d",
  storageBucket: "real-estate-dff2d.appspot.com",
  messagingSenderId: "270186076337",
  appId: "1:270186076337:web:121b02803d1a60206c2a90"
};

export const firebaseApp = initializeApp(firebaseConfig);