const container_parent = document.getElementById('container');
const container_child = document.getElementById('unity-container');

let resize = () => {
    if (container_parent.clientWidth <= container_parent.clientHeight * 1.55) {
        container_child.style.width = container_parent.clientWidth.toString() + 'px';
        container_child.style.height = (container_parent.clientWidth / 1.55).toString() + 'px';
    } else  if (container_parent.clientWidth >= container_parent.clientHeight * 1.65) {
        container_child.style.width = (container_parent.clientHeight * 1.65).toString() + 'px';
        container_child.style.height = container_parent.clientHeight.toString() + 'px';
    } else {
        container_child.style.width = '100%';
        container_child.style.height = '100%';
    }
}
let main = () => {
    window.addEventListener('load', resize, false);
    window.addEventListener('resize', resize, false);
}
main(); 
