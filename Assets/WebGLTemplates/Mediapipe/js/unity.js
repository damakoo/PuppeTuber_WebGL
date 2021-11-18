const container_parent = document.getElementById('container');
const container_child = document.getElementById('unity-container');
const puppetvideo = document.getElementById('puppetvideo');
const startbutton = document.getElementById('start');
const howtoplaybutton = document.getElementById('howtoplay');
const backbutton = document.getElementById('back');
const puppeimg = document.getElementById('puppeimg');
const lisence = document.getElementById('lisence');

let resize = () => {
    if (container_parent.clientWidth <= container_parent.clientHeight * 1.55) {
        container_child.style.width = container_parent.clientWidth.toString() + 'px';
        container_child.style.height = (container_parent.clientWidth / 1.55).toString() + 'px';
        videosource.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
        lisence.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
        howtoplaybutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
        startbutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
        backbutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
    } else  if (container_parent.clientWidth >= container_parent.clientHeight * 1.65) {
        container_child.style.width = (container_parent.clientHeight * 1.65).toString() + 'px';
        container_child.style.height = container_parent.clientHeight.toString() + 'px';
        videosource.style.transform = "scale(" + container_parent.clientHeight/750 + ")";
        lisence.style.transform = "scale(" + container_parent.clientHeight/700 + ")";
        howtoplaybutton.style.transform = "scale(" + container_parent.clientHeight/700 + ")";
        startbutton.style.transform = "scale(" + container_parent.clientHeight/700 + ")";
        backbutton.style.transform = "scale(" + container_parent.clientHeight/700 + ")";
    } else {
        container_child.style.width = '100%';
        container_child.style.height = '100%';
        videosource.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";
        lisence.style.transform = "scale(" + container_parent.clientWidth/1200 + ")"; 
        howtoplaybutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")"; 
        startbutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")"; 
        backbutton.style.transform = "scale(" + container_parent.clientWidth/1200 + ")";        
    }
}
let fadevideo = () => {
    startbutton.style.display = `block`;
    startbutton.disabled = false;
    howtoplaybutton.style.display = `block`;
    howtoplaybutton.disabled = false;
    puppetvideo.style.display = 'none';
    puppetvideo.pause();
    puppetvideo.currentTime = 0;
    puppeimg.style.display = 'block';
}
let main = () => {
    window.addEventListener('load', resize, false);
    window.addEventListener('resize', resize, false);
    puppetvideo.addEventListener(`ended`,fadevideo,false)
}
main(); 
function StartButtonClick(){
    puppeimg.style.display = 'none';
startbutton.style.display = `none`;
startbutton.disabled = true;
howtoplaybutton.style.display = `none`;
howtoplaybutton.disabled = true;
backbutton.style.display = `none`;
backbutton.disabled = true;
lisence.style.display = 'none';
navigator.mediaDevices.enumerateDevices().then(gotDevices).catch(handleError);
};

function BackButtonClick(){
    startbutton.style.display = `block`;
    startbutton.disabled = false;
    howtoplaybutton.style.display = `block`;
    howtoplaybutton.disabled = false;
    puppetvideo.style.display = 'none';
    puppetvideo.pause();
    puppetvideo.currentTime = 0;
    puppeimg.style.display = 'block';
backbutton.style.display = `none`;
backbutton.disabled = true;
lisence.style.display = 'block';
};

function HowToPlayButtonClick(){
    puppetvideo.style.display = 'block';
    puppetvideo.play();
    backbutton.style.display = `block`;
    backbutton.disabled = false;
    puppeimg.style.display = 'none';
startbutton.style.display = `none`;
startbutton.disabled = true;
howtoplaybutton.style.display = `none`;
howtoplaybutton.disabled = true;
lisence.style.display = 'none';
};
startbutton.onclick = StartButtonClick;
backbutton.onclick = BackButtonClick;
howtoplaybutton.onclick = HowToPlayButtonClick;
