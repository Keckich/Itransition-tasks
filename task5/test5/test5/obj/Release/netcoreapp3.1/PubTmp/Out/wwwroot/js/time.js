$(function () {
    timeZone();
})

function timeZone() {
    var time = new Date().getTimezoneOffset();
    console.log(time);
    document.cookie = 'timezone' + '=' + encodeURI(time);
}