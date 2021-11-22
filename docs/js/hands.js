const source = document.getElementsByClassName('input_video')[0];
const videoElement = document.getElementsByClassName('output_video')[0];
const canvasElement = document.getElementsByClassName('output_canvas')[0];
const canvasCtx = canvasElement.getContext('2d');
const videosource =document.getElementById('videoSource');
var landmark0;
var videoStream = null;
var Videoname_before = "";
var Videoname = "";
const videoSelect = document.querySelector('select#videoSource');
const selectors = [videoSelect];

function gotDevices(deviceInfos) {
  // Handles being called several times to update labels. Preserve values.
  const values = selectors.map((select) => select.value);
  selectors.forEach((select) => {
    while (select.firstChild) {
      select.removeChild(select.firstChild);
    }
  });
  for (let i = 0; i !== deviceInfos.length; ++i) {
    const deviceInfo = deviceInfos[i];
    const option = document.createElement('option');
    option.value = deviceInfo.deviceId;
    if (deviceInfo.kind === 'videoinput') {
      option.text = deviceInfo.label || `camera ${videoSelect.length + 1}`;
      videoSelect.appendChild(option);
    }
  }
  selectors.forEach((select, selectorIndex) => {
    if (
      Array.prototype.slice
        .call(select.childNodes)
        .some((n) => n.value === values[selectorIndex])
    ) {
      select.value = values[selectorIndex];
    }
  });
}
navigator.mediaDevices.enumerateDevices().then(gotDevices).catch(handleError);

function handleError(error) {
  console.log(
    'navigator.MediaDevices.getUserMedia error: ',
    error.message,
    error.name
  );
}

function gotStream(stream) {
  window.stream = stream;
  videoStream = stream;
  playVideo(source);
  // Refresh button list in case labels have become available
  return navigator.mediaDevices.enumerateDevices();
}

videoSelect.onchange = start;

function start() {
  Videoname = videoSelect.value;
const videoSource = videoSelect.value;
const constaints = {
  video: { deviceId: videoSource ? { exact: videoSource } : undefined },
  audio: false,
};
navigator.mediaDevices
  .getUserMedia(constaints)
  .then(gotStream)
  .catch(handleError);
  var camera = new Camera(videoElement, {
       onFrame: async () => {
         if (Videoname != Videoname_before) return;
         await hands.send({ image: source });
      },
      width: 1280,
      height: 720
     });
      camera.start();
     window.setTimeout(VideoStart,1000);
  }
  function playVideo(video) {
    video.srcObject = videoStream;
    video.play();
  }
  

function onResults(results) {
  landmarkresult = results;
  canvasCtx.save();
  canvasCtx.clearRect(0, 0, canvasElement.width, canvasElement.height);
  canvasCtx.drawImage(
    results.image, 0, 0, canvasElement.width, canvasElement.height);
    findhand = (results.multiHandLandmarks != null);
  if (results.multiHandLandmarks) {
    landmark0 = results.multiHandLandmarks[0];
    for (const landmarks of results.multiHandLandmarks) {
      drawConnectors(canvasCtx, landmarks, HAND_CONNECTIONS,
                     {color: '#00FF00', lineWidth: 5});
      drawLandmarks(canvasCtx, landmarks, {color: '#FF0000', lineWidth: 2});
    }
  }
  canvasCtx.restore();
}

function showresult(){
  if(landmark0){
    localStorage.setItem("handpos",JSON.stringify(landmark0));
  }
}

const hands = new Hands({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`;
}});
hands.setOptions({
  maxNumHands: 1,
  modelComplexity: 1,
  minDetectionConfidence: 0.5,
  minTrackingConfidence: 0.5
});
hands.onResults(onResults);


function appearcanvas(){
  canvasElement.style = "display:block";
  videosource.style = "display:block";
}
function fadecanvas(){
  canvasElement.style = "display:none";
  videosource.style = "display:none";
}
function VideoStart() {
  Videoname_before = Videoname;
}
start();
