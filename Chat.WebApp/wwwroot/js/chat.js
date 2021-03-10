var connection = new signalR.HubConnectionBuilder().withUrl("/chat/").build();
document.getElementById("btnSend").disabled = true;
var id = userId;
var mainDiv = document.getElementById("messagesList");

connection.start().then(function () {
    document.getElementById("btnSend").disabled = false;
    mainDiv.scrollTop = mainDiv.scrollHeight;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnSend").addEventListener("click", function (e) {
    var message = document.getElementById("userMessage").value;
    document.getElementById("userMessage").value = '';
    mainDiv.scrollTop = mainDiv.scrollHeight;
    connection.invoke("PostMessage", userId, userName, message).catch(function (err) {
        return console.error(err.toString());
    });

    e.preventDefault();
});

connection.on("GetMessage", function (userId, userName, message, date) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt");
    var msgList = document.getElementById("messagesList");

    var sent = false;
    if (userId == id)
        sent = true;

    var li = document.createElement("li");
    var div = document.createElement("div");

    if (sent) {
        li.style = "text-align: -webkit-right;"
        div.className = "message sent";
    }
    else {
        li.style = "text-align: -webkit-left;"
        div.className = "message received";
    }

    var p = document.createElement("p");
    p.textContent = userName;
    p.style = "font-weight: bold";

    var pDate = document.createElement("p");
    pDate.textContent = date;
    pDate.style = "font-size: 12px; margin-top: 7px; text-align: right";

    div.appendChild(p);
    div.append(msg);
    div.append(pDate);

    li.appendChild(div);
    console.log(msgList, msgList.children.length)

    if (msgList.children.length == 50)
        msgList.removeChild(msgList.children[0]);

    document.getElementById("messagesList").appendChild(li);
    mainDiv.scrollTop = mainDiv.scrollHeight;

});
