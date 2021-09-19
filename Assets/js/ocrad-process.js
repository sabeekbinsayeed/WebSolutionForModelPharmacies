var image = document.getElementById('image');
var string = OCRAD(image);

var finalString = string.replace(/\s|[0-9]|\W|[#$%^&*()]/g, "-");

console.log(finalString);

window.location.href = "/home/prescription/handle/?key=" + finalString;