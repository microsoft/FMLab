document.addEventListener("screenshot", function() {
    chrome.extension.sendMessage({name: 'screenshot'}, function(response) {
        var dataURL = response.screenshotUrl;
        var image = new Image();
        image.src = dataURL;

        // Cut the task recorder pane from the screenshot.
        var ratio = window.devicePixelRatio;
		var canvas = document.createElement("canvas");
		var context = canvas.getContext('2d');
		canvas.width = window.innerWidth * ratio;
		canvas.height = window.innerHeight * ratio;
		var taskRecorderPaneWidth = document.getElementById('asidePane').clientWidth * ratio;
		context.drawImage(image, 0, 0, (canvas.width - taskRecorderPaneWidth), canvas.height, 0, 0, canvas.width, canvas.height);
		var croppedImage = canvas.toDataURL('image/png');

		var origin = window.location.protocol + "//" + window.location.host;
		window.postMessage(croppedImage, origin);
    });
});

var isInstalledNode = document.createElement('div');
isInstalledNode.id = 'screenshotExtensionIsInstalled';
document.body.appendChild(isInstalledNode);