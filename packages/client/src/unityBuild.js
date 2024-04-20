// Define a function that returns a promise which resolves when the condition is met
function waitForCondition(condition, interval = 1000) {
  return new Promise((resolve, reject) => {
      const checkCondition = () => {
          if (condition()) {
              resolve();
          } else {
              setTimeout(checkCondition, interval); // Check the condition again after the specified interval
          }
      };
      checkCondition(); // Start checking the condition
  });
}

var container = document.querySelector("#unity-container");
var canvas = document.querySelector("#unity-canvas");
var loadingBar = document.querySelector("#unity-loading-bar");
var progressBarFull = document.querySelector("#unity-progress-bar-full");
var fullscreenButton = document.querySelector("#unity-fullscreen-button");
var warningBanner = document.querySelector("#unity-warning");

// Shows a temporary message banner/ribbon for a few seconds, or
// a permanent error message on top of the canvas if type=='error'.
// If type=='warning', a yellow highlight color is used.
// Modify or remove this function to customize the visually presented
// way that non-critical warnings and error messages are presented to the
// user.
function unityShowBanner(msg, type) {
  function updateBannerVisibility() {
    warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
  }
  var div = document.createElement('div');
  div.innerHTML = msg;
  warningBanner.appendChild(div);
  if (type == 'error') div.style = 'background: red; padding: 10px;';
  else {
    if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
    setTimeout(function() {
      warningBanner.removeChild(div);
      updateBannerVisibility();
    }, 5000);
  }
  updateBannerVisibility();
}

var buildUrl = "Build";
var loaderUrl = buildUrl + "/BuildWebGL.loader.js";
var config = {
  dataUrl: buildUrl + "/BuildWebGL.data.br",
  frameworkUrl: buildUrl + "/BuildWebGL.framework.js.br",
  codeUrl: buildUrl + "/BuildWebGL.wasm.br",
  streamingAssetsUrl: "StreamingAssets",
  companyName: "DefaultCompany",
  productName: "Clean-or-Covet",
  productVersion: "1.0",
  showBanner: unityShowBanner,
};

// By default, Unity keeps WebGL canvas render target size matched with
// the DOM size of the canvas element (scaled by window.devicePixelRatio)
// Set this to false if you want to decouple this synchronization from
// happening inside the engine, and you would instead like to size up
// the canvas DOM size and WebGL render target sizes yourself.
// config.matchWebGLToCanvasSize = false;

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
  // Mobile device style: fill the whole browser client area with the game canvas:

  var meta = document.createElement('meta');
  meta.name = 'viewport';
  meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
  document.getElementsByTagName('head')[0].appendChild(meta);
  container.className = "unity-mobile";
  canvas.className = "unity-mobile";

  // To lower canvas resolution on mobile devices to gain some
  // performance, uncomment the following line:
  // config.devicePixelRatio = 1;


} else {
  // Desktop style: Render the game canvas in a window that can be maximized to fullscreen:

  canvas.style.width = "960px";
  canvas.style.height = "600px";
}

loadingBar.style.display = "block";

// Wait discord setup  
/*
waitForCondition(() => typeof GetDiscordUserId() !== 'undefined')
.then(() => {
    console.log('GetDiscordUserId Condition met!'); // This will be executed when someVariable becomes 10
})
.catch((error) => {
    console.error('GetDiscordUserId Error occurred:', error);
});
waitForCondition(() => typeof GetDiscordUserName() !== 'undefined')
.then(() => {
    console.log('GetDiscordUserName Condition met!'); // This will be executed when someVariable becomes 10
})
.catch((error) => {
    console.error('GetDiscordUserName Error occurred:', error);
});
*/
waitForCondition(() => GetChannelName() !== 'NotAssigned')
.then(() => {
    console.log('GetDiscordChannelName Condition met!', GetChannelName()); // This will be executed when someVariable becomes 10
})
.catch((error) => {
    console.error('GetDiscordChannelName Error occurred:', error);
});
waitForCondition(() => GetChannelId() !== 'NotAssigned')
.then(() => {
    console.log('GetDiscordChannelId Condition met!', GetChannelId()); // This will be executed when someVariable becomes 10
})
.catch((error) => {
    console.error('GetDiscordChannelId Error occurred:', error);
});

var script = document.createElement("script");
script.src = loaderUrl;
script.onload = () => {
  createUnityInstance(canvas, config, (progress) => {
    progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
          loadingBar.style.display = "none";
          fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
          };
        }).catch((message) => {
          alert(message);
        });
      };

document.body.appendChild(script);
