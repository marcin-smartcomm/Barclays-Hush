let PINNumberBtn = [];
let PINEntered = [];
let PINTextArea;
let PINEntryEnabled = true;

let changingPIN = false;

function InitializePINPageVariables()
{
  for(let i = 0; i < 10; i++)
  {
    PINNumberBtn[i] = document.getElementById(GetStringID(i)).addEventListener('click', function() {
      PINBtnPressed(i);
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
}

function GetStringID(idNum)
{
  return "PIN"+idNum;
}

function PINBtnPressed(BtnPressed)
{
  if(!PINEntryEnabled)
    return;

  let starsNum = 0;
  for(let i = 0; i < 4; i++)
  {
    if(PINEntered[i] == null)
    {
      starsNum++;
      PINEntered[i] = BtnPressed;

      for(let j = 0; j < starsNum; j++)
      {
        PINTextArea.value += "*";
      }

      if(i == 3)
        EvaluatePIN();

      return;
    }
  }
}

function EvaluatePIN()
{
  sendMessage("Login:"+PINEntered[0]+PINEntered[1]+PINEntered[2]+PINEntered[3]);
  
  PINTextArea.value = "";
}

function EvaluateLoginResult(systemResponse)
{
  if(systemResponse.includes("Success"))
  {
    openSubpage("Home");
  }
  else
  {
    PINTextArea.value = "Wrong PIN !"
    PINTextArea.style.color = "red";
    PINEntryEnabled = false;
    PINEntered = [];
    setTimeout(resetInputField, 1500);
  }
}

function resetInputField()
{
  PINTextArea.value = ""
  PINTextArea.style.color = "black";
  PINEntryEnabled = true;
}