chrome.extension.onMessage.addListener(function(request, sender, sendResponse) {
    if (request.name == 'screenshot') {
        chrome.tabs.captureVisibleTab(null, null, function(dataUrl) {			
            sendResponse({ screenshotUrl: dataUrl });
        });
    }
    return true;
});
