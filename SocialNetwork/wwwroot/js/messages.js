function onLoadScrollMessagesDown() {
	const box = document.getElementsByClassName('messagesBox')[0];
	box.scrollTop = box.scrollHeight;
}

window.addEventListener('load', onLoadScrollMessagesDown);