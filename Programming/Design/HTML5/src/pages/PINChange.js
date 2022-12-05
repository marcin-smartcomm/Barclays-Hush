let PINChangeEntered = [];
let currentPIN, newPIN, newPINConfirmed;

function InitializePINChangeVariables()
{
  for(let i = 0; i < 10; i++)
  {
    PINNumberBtn[i] = document.getElementById(`PINChange${i}`).addEventListener('click', function() {
      PINChangeBtnPressed(i);
    });
  }

  PINTextArea = document.getElementById("PINInputTextField");
  
  backBtn = document.getElementById("backBtn")
  
  if(backBtn)
  {
    backBtn.addEventListener('click', function() {
      openSubpage("Admin");
    });
  }
  
  currentPIN = "";
  newPIN = "";
  newPINConfirmed = "";
  document.getElementById("message2-container").style.visibility = "hidden";
  document.getElementById("message3-container").style.visibility = "hidden";
  document.getElementById("message4-container").style.visibility = "hidden";
}

function PINChangeBtnPressed(BtnPressed)
{
  if(!PINEntryEnabled)
    return;

  let starsNum = 0;
  for(let i = 0; i < 4; i++)
  {
    if(PINChangeEntered[i] == null)
    {
      starsNum++;
      PINChangeEntered[i] = BtnPressed;

      for(let j = 0; j < starsNum; j++)
      {
        PINTextArea.value += "*";
      }

      if(i == 3)
        EvaluateChangePIN();

      return;
    }
  }
}

function EvaluateChangePIN()
{
  if(currentPIN[0] == null)
  {
    for(let i = 0; i < 4; i++)
    {
      if(PINEntered[i] != PINChangeEntered[i])
      {
        PINTextArea.value = "Wrong PIN !"
        PINTextArea.style.color = "red";
        PINEntryEnabled = false;
        PINChangeEntered = [];
        setTimeout(resetInputField, 1500);
        return;
      }
    }

    CurrentPINCorrect();
  }
  else if(newPIN[0] == null)
  {
    document.getElementById("promptAnimation2").style.animation = "goGreen 0.5s forwards";
    document.getElementById("promptAnimation2").style.animationIterationCount = 1;
    document.getElementById("message3-container").style.visibility = "visible";

    newPIN = PINChangeEntered;
    PINChangeEntered = [];
    resetInputField();
  }
  else if(newPINConfirmed[0] == null)
  {
    for(let i = 0; i < 4; i++)
    {
      if(newPIN[i] != PINChangeEntered[i])
      {
        PINTextArea.value = "Wrong PIN !"
        PINTextArea.style.color = "red";
        PINEntryEnabled = false;
        PINChangeEntered = [];
        setTimeout(resetInputField, 1500);
        return;
      }
    }

    NewPINCorrect();
  }
}

function NewPINCorrect()
{
  document.getElementById("promptAnimation3").style.animation = "goGreen 0.5s forwards";
  document.getElementById("promptAnimation3").style.animationIterationCount = 1;
  document.getElementById("message4-container").style.visibility = "visible";
  PINEntryEnabled = false;
  sendMessage(`PINChange:${PINChangeEntered[0]}${PINChangeEntered[1]}${PINChangeEntered[2]}${PINChangeEntered[3]}`);
  PINEntered = PINChangeEntered;
  PINChangeEntered = [];
  setTimeout(resetInputField, 1500);
  setTimeout(GoBackToHomePage, 1500);
}

function GoBackToHomePage()
{
  openSubpage("Home");
}

function CurrentPINCorrect()
{
  document.getElementById("promptAnimation1").style.animation = "goGreen 0.5s forwards";
  document.getElementById("promptAnimation1").style.animationIterationCount = 1;
  document.getElementById("message2-container").style.visibility = "visible";

  currentPIN = PINChangeEntered;
  PINChangeEntered = [];
  resetInputField();
}

function resetInputField()
{
  PINTextArea.value = ""
  PINTextArea.style.color = "black";
  PINEntryEnabled = true;
}