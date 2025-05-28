// TODO: Replace the following with your app's Firebase project configuration
var firebaseConfig = {
    
  apiKey: "AIzaSyCyJxP0ZsflVrqLMs6ULa-2T1tRFvysLCI",
  authDomain: "testing-a962b.firebaseapp.com",
  databaseURL: "https://testing-a962b-default-rtdb.firebaseio.com",
  projectId: "testing-a962b",
  storageBucket: "testing-a962b.firebasestorage.app",
  messagingSenderId: "835710571851",
  appId: "1:835710571851:web:f6bc83dde12ebfe27d44ce",
  measurementId: "G-93MEPSQKPQ"
  };
  
  // Initialize Firebase
  firebase.initializeApp(firebaseConfig);

  function StoreStringInLocalStorage(key, value) {
    localStorage.setItem(key, value);
    console.log("Stored in localStorage: " + key + " = " + value);
  }
  
  // Function to retrieve a string from localStorage
  function RetrieveStringFromLocalStorage(key)
   {
    var value = localStorage.getItem(key)
  ;
    console.log("Retrieved from localStorage: " + key + " = " + value);
  
    // Send the retrieved value back to Unity
    if (value === null) {
        value = "null"; // Handle case where the key doesn't exist
    }
    unityInstance.SendMessage('WebGLStorage', 'OnDataRetrieved', value);
  }


  function OpenFileDialog() {
    var input = document.createElement('input');
    input.type = 'file';
    input.accept = 'image/png, image/jpeg, image/jpg, application/pdf, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document';

    input.onchange = function (event) {
        var file = event.target.files[0];
        var reader = new FileReader();

        reader.onload = function () {
            var base64String = reader.result.split(',')[1]; // Extract Base64 data
            console.log("Sending to Unity:", base64String.substring(0, 30)); // Debug

            // Ensure unityInstance is correctly referenced
            if (window.unityInstance) {
                window.unityInstance.SendMessage('FileLoader', 'OnFileSelected', base64String);
            } else {
                console.error("Unity instance not found! Ensure your Unity WebGL is properly loaded.");
            }
        };

        // Read files as Base64 depending on type
        if (file.type.startsWith("image/")) {
            reader.readAsDataURL(file); // Read images as Data URL (Base64)
        } else {
            reader.readAsArrayBuffer(file); // Read PDF/DOC as binary buffer
        }
    };

    input.click();
}


//
function detectTabFocusChange() {
    document.addEventListener('visibilitychange', function () {
      if (document.hidden) {
        // Tab is minimized or lost focus
        console.log("Tab is minimized or lost focus JS--");
        sendMessageToUnity('OnTabMinimized');
      } else {
        // Tab is back in focus
        console.log("Tab is back in focus JS---");
        sendMessageToUnity('OnTabFocused');
      }
    });
  }

  // Function to send messages to Unity
  function sendMessageToUnity(methodName) {
    if (window.unityInstance) {
      // Send the message to Unity
      window.unityInstance.SendMessage('TabMinimizeHandler', methodName);
    } else {
      console.error("Unity instance not found! Ensure Unity WebGL is properly loaded.");
    }
  }

  // Initialize tab focus detection
  detectTabFocusChange();