//Get Date and Time
document.onload = openSubpage("ScreenSaver");

setInterval(getDateAndTimeTimer, 1000);
function getDateAndTimeTimer()
{
  const d = new Date();
  var day = "";
  var month = "";
  var hours = "";
  var minutes = "";
  var seconds = "";
  if(d.getDate() < 10)
    day = "0"+d.getDate();
  else
    day = d.getDate();
  if(d.getMonth() < 9)
    month = "0"+(d.getMonth()+1);
  else
    month = (d.getMonth()+1);

  if(d.getHours() < 10)
    hours = "0"+d.getHours();
  else
    hours = d.getHours();

  if(d.getMinutes() < 10)
    minutes = "0"+d.getMinutes();
  else
    minutes = d.getMinutes();

  if(d.getSeconds() < 10)
    seconds = "0"+d.getSeconds();
  else
    seconds = d.getSeconds();

  document.getElementById("date-time").innerHTML = 
  day+"/"+month+"/"+d.getFullYear()+" "+hours+":"+minutes+":"+seconds;
}