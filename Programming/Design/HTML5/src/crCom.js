let _webSocket = new WebSocket('ws://192.168.1.137:50000');
//let _webSocket = new WebSocket('ws://10.10.23.134:50001');

let daysDataFromProcessor = "";

_webSocket.onmessage = function(e) {
    onMessage(e);
}

_webSocket.onopen = function(e) {
    ping();
    setInterval(ping, 10000);
    connStatus('green', 'Connected');
    socketConnected = true;
}

function sendMessage(message)
{
    _webSocket.send("STRING[1,"+message+"]");
    //console.log(message);
}

_webSocket.onerror = function(e)
{
    console.log("error connecting");
    location.reload();
}

let socketConnected = false;
async function ping() {   
    //console.log("Websocket Ready state: "+_webSocket.readyState);
    if (_webSocket.readyState === 0 || _webSocket.readyState === 3)
    {
        socketConnected = false;
        location.reload();
    }
    
    if(socketConnected)
    {
        _webSocket.send('STRING[1,__ping__]');
    }

    tm = setTimeout(function () {
        connStatus('red', 'Disconnected');
    }, 5000);
}

function connStatus(color, message)
{
    //const connMessage = document.getElementById('connStatus');
    //connMessage.setAttribute("style", `color: ${color};`);
    //connMessage.textContent = message;
}

function pong() {
    connStatus('green', 'Connected');
    clearTimeout(tm);
}

function onMessage(e) {
  const msg = e.data;
  const value = getBoundString_EndLastIndex(msg, ",", "]"); 
  console.log(e.data);
    if (value == '__pong__') {
        pong();
        return;
    }
    if(value.includes("Login"))
    {
        //In PINPage.js
        EvaluateLoginResult(value);
    }
    if(value.includes("DaysData"))
    {
        let daysDataIncoming = "";
        daysDataIncoming = value.replace('DaysData ', '');
        daysDataFromProcessor = JSON.parse(daysDataIncoming);

        //In Scheduler.js
        ProcessScheduledDaysData(daysDataFromProcessor);
    }
    if(value.includes("VideoMatrix"))
    {
        let dataIncoming = "";
        dataIncoming = value.replace('VideoMatrix:', '');

        //In VideoMatrix.js
        ProcessVideoMatrixMessage(dataIncoming);
    }
}
 
function getBoundString_EndLastIndex(msg, startChar, stopChar)
{
    let response = "";
         
    if (msg != null && msg.length > 0)
    {
        let start = msg.indexOf(startChar);
             
        if (start >= 0)
        {
            start += startChar.length;
                 
            let end = msg.lastIndexOf(stopChar);
             
            if (start < end)
            {
                response = msg.substring(start, end);
            }
        }
    }
         
    return response;
}