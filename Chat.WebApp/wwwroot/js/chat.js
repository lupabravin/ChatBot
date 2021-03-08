﻿var connection = new signalR.HubConnectionBuilder().withUrl("/chat/").build();
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

connection.on("GetMessage", function (userId, userName, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt");

    var sent = false;
    if (userId == id)
        sent = true;

    console.log(userId, id, sent)

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

    div.appendChild(p);
    div.append(msg);

    li.appendChild(div);

    document.getElementById("messagesList").appendChild(li);
    mainDiv.scrollTop = mainDiv.scrollHeight;

});
