let unityInstanceReady = false; // Flag to track if Unity is ready
let unityInstance; // Store the Unity instance

// Function to display a banner with messages during Unity loading process
function unityShowBanner(message, type) {
    var banner = document.createElement("div");
    banner.innerHTML = message;
    banner.style.padding = "10px";
    banner.style.color = "white";

    // Style the banner based on the type
    if (type === "error") {
        banner.style.backgroundColor = "red";
    } else if (type === "warning") {
        banner.style.backgroundColor = "yellow";
    } else {
        banner.style.backgroundColor = "green";
    }

    // Append the banner to the body or a specific div
    document.body.appendChild(banner);

    // Optionally, remove the banner after 5 seconds
    setTimeout(() => {
        banner.remove();
    }, 5000);
}

// document.addEventListener("DOMContentLoaded", () => {
//     initializeUnity();
// });

// Function to initialize Unity and set the ready flag
function initializeUnity() {
    console.log("Ready 1");
    var buildUrl = "Build";
    var loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
    var config = {
        dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
        frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
        codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "{{{ COMPANY_NAME }}}",
        productName: "{{{ PRODUCT_NAME }}}",
        productVersion: "{{{ PRODUCT_VERSION }}}",
        showBanner: unityShowBanner
    };
    console.log("Ready 2");

    var script = document.createElement("script");
    script.src = loaderUrl;
    script.onload = () => {
        console.log("Ready 3");
        createUnityInstance(document.querySelector("#unity-canvas"), config, (progress) => {
            console.log("Unity loading progress: " + Math.round(progress * 100) + "%");
        }).then((instance) => {
            unityInstance = instance;
            console.log("Unity instance is now ready.");
            unityInstanceReady = true;
        }).catch((message) => {
            console.error("Failed to load Unity:", message);
        });
    };
    document.body.appendChild(script);
}

// Initialize Unity instance
initializeUnity();

// Function to start recording
function startRecording() {
    navigator.mediaDevices.getUserMedia({ audio: true })
        .then(function (stream) {
            audioContext = new (window.AudioContext || window.webkitAudioContext)();
            let microphone = audioContext.createMediaStreamSource(stream);
            recorder = new Recorder(microphone, { numChannels: 1 });
            recorder.record();
            console.log("Recording started...");
        })
        .catch(function (error) {
            console.error("Error accessing microphone:", error);
        });
}

// Function to stop recording and send audio data
function stopRecording() {
    if (recorder) {
        recorder.stop();
        recorder.exportWAV(function (blob) {
            let reader = new FileReader();
            reader.onloadend = function () {
                let audioArrayBuffer = reader.result;
                sendAudioToUnity(audioArrayBuffer); // Send audio data to Unity
            };
            reader.readAsArrayBuffer(blob);
        });
        console.log("Recording stopped...");
    } else {
        console.error("Recorder is not initialized.");
    }
}

// Function to send audio to Unity
function sendAudioToUnity(audioArrayBuffer) {
    if (unityInstanceReady && unityInstance) {
        // Unity is ready, send audio data
        let base64Audio = arrayBufferToBase64(audioArrayBuffer);
        unityInstance.SendMessage("AudioRecorder", "OnAudioRecorded", base64Audio);
    } else {
        console.error("Unity instance is not ready yet. Retrying...");
        setTimeout(() => {
            sendAudioToUnity(audioArrayBuffer); // Retry after 500ms
        }, 500); // Retry delay (500ms)
    }
}



// Helper function to convert ArrayBuffer to Base64
function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary); // Convert to base64
}
