var connection = new signalR.HubConnectionBuilder().withUrl("/chat/").build();
document.getElementById("btnSend").disabled = true;


connection.start().then(function () {
    document.getElementById("btnSend").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnSend").addEventListener("click", function (e) {
    var user = userName;
    var message = document.getElementById("userMessage").value;
    connection.invoke("PostMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    e.preventDefault();
});

connection.on("GetMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt");
    var encodedMsg = user + " : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
