let LEDWallBrightness = [];
let SelectedInputOnOutput = [];

function InitializeVideoMatrixVariables()
{
  LEDWallBrightness[1] = 50;
  LEDWallBrightness[2] = 50;
  LEDWallBrightness[3] = 50;
  LEDWallBrightness[4] = 50;
  
  SelectedInputOnOutput[1] = 1;
  SelectedInputOnOutput[2] = 2;
  SelectedInputOnOutput[3] = 3;
  SelectedInputOnOutput[4] = 4;
  SelectedInputOnOutput[5] = 9;

  backBtn = document.getElementById("backBtn")

  if(backBtn)
  {
    backBtn.addEventListener('click', function() {
      openSubpage("Admin");
    });
  }

  InitializeBrightnessBtns();
  InitializeBrightnessSliders();
  InitializeIOMatrix();
}

function InitializeBrightnessBtns()
{
  for(let i = 1; i < 5; i++)
  {
    document.getElementById(`out${i}-brightness-up`).addEventListener('click', function() {
      sendMessage(`VideoMatrix:Brightness:${i}:Up`);
    })
  } 
  for(let i = 1; i < 5; i++)
  {
    document.getElementById(`out${i}-brightness-down`).addEventListener('click', function() {
      sendMessage(`VideoMatrix:Brightness:${i}:Down`);
    })
  } 
}

function InitializeBrightnessSliders()
{
  for(let i = 1; i < 5; i++)
  {
    let sliderVal = 100 - LEDWallBrightness[i];

    document.getElementById(`out${i}-brightness-slider`).style.background = `linear-gradient(#858585 ${sliderVal}%,#00aeef 0 100%,#2c3749 0`

    document.getElementById(`out${i}-brightness-label`).innerHTML = LEDWallBrightness[i]+"%";
  }
}

function ChangeBrightnessValue(wall, brightness)
{
  LEDWallBrightness[wall] = brightness;

  if(currentSubpage == "VideoMatrix")
  {
    let sliderVal = 100 - brightness;

    document.getElementById(`out${wall}-brightness-slider`).style.background = `linear-gradient(#858585 ${sliderVal}%,#00aeef 0 100%,#2c3749 0`

    document.getElementById(`out${wall}-brightness-label`).innerHTML = LEDWallBrightness[wall]+"%";
  }
}

function InitializeIOMatrix()
{
  for(let i = 1; i < 6; i++)
  {
    for(let j = 1; j < 10; j++)
    {
      document.getElementById(`matrix-i${j}0${i}`).addEventListener('click', function()
      {
        sendMessage(`VideoMatrix:ChangeInput:${j}:${i}`);
      })
    }
  }

  for(let i = 1; i < 6; i++)
  {
    ChangeInput(SelectedInputOnOutput[i], i);
  }
}

function ChangeInput(input, output)
{
  SelectedInputOnOutput[output] = input;
  
  if(currentSubpage == "VideoMatrix")
  {
    ClearCurrentInput(output);

    document.getElementById(`matrix-i${input}0${output}`).classList.remove("empty");
    document.getElementById(`matrix-i${input}0${output}`).classList.add("filled");
  }
}

function ClearCurrentInput(output)
{
  for(let i = 1; i < 10; i++)
  {
    document.getElementById(`matrix-i${i}0${output}`).classList.remove("filled");
    document.getElementById(`matrix-i${i}0${output}`).classList.add("empty");
  }
}

function ProcessVideoMatrixMessage(message)
{
  if(message.includes("ChangeInput"))
  {
    ChangeInput(parseInt(message.split(":")[1]), parseInt(message.split(":")[2]));
  }
  if(message.includes("Brightness"))
  {
    ChangeBrightnessValue(parseInt(message.split(":")[1]), parseInt(message.split(":")[2]));
  }
}