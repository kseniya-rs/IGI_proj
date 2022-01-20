"use strict";

function run_js_with_model(dialog) {
	console.log("HERE IS DIALOG:", dialog);
	
	var messageTextarea = document.getElementById("messageTextarea");
	var messagesBox = document.getElementById("messagesBox");
	
	var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

	console.log(connection);
	connection.on("ReceiveMessage", function (dialogId, senderId, message) {
		
		var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
		
		var quote = document.createElement("blockquote");
		
		var author = null;
		if (dialog.myID != senderId) {
			quote.classList.add("bg-success");
			author = dialog.addressee;
			
		} else {
			quote.classList.add("blockquote-reverse");
		}
		
		if (author) {
			var quote_author = document.createElement("b")
			quote_author.innerText = author + ":";
			quote.appendChild(quote_author);
		}
		var text = document.createElement("p");
		text.innerText = msg;
		quote.appendChild(text);

		messagesBox.appendChild(quote);
		messagesBox.scrollTop = messagesBox.scrollHeight;
	});

	connection.start().catch(function (err) {
		return console.error(err.toString());
	});

	function sendMessage() {
		var dialogId = dialog.id;
		var senderId = dialog.myID;
		var message = messageTextarea.value;
		
		connection.invoke("SendMessage", dialogId, senderId, message).catch(function (err) {
			return console.error(err.toString());
		});
		messageTextarea.innerHTML = "";
	}

	document.getElementById("messageForm").addEventListener("submit", function (event) {
		event.preventDefault();
		
		sendMessage();
	});

	document.getElementById("messageForm").addEventListener("keyup", function (event) {
		event.preventDefault();

		if (event.keyCode === 13 && !event.shiftKey) {
			sendMessage();
		}
	});
}