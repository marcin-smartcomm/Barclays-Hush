let backBtn;
let currentSubpage;
let blankOutBtnsVis = false;

function openSubpage(file)
{
    currentSubpage = file;

    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", './pages/'+file+'.html', false);
    rawFile.onreadystatechange = function ()
    {
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                var allText = rawFile.responseText;
                document.querySelector('#subpageSection').innerHTML = allText;
            }
        }
    }
    rawFile.send(null);
    rawFile.DONE;
    
    InitializeSubpageVariables(file);
}

function ToggleBlankOutBtns()
{
  blankOutBtnsVis = !blankOutBtnsVis;

  if(blankOutBtnsVis)
  {
  blankOutScreensBtn.innerHTML = 
  "<button class=\"blankOutBtn green\" id=\"blankOutConfirm\">Confirm</button>"+
  "<button class=\"blankOutBtn red\" id=\"blankOutCancel\">Cancel</button>"
  }
  else
  {
    blankOutScreensBtn.innerHTML = "";
  }
}

function InitializeSubpageVariables(pageToInitialize)
{
  if(pageToInitialize == "ScreenSaver")
  {
    InitializeScreenSaverVariables();
  }
  if(pageToInitialize == "Scheduler")
  {
    InitializeSchedulerVariables();
  }
  if(pageToInitialize == "Home")
  {
    InitializeHomeVariables();
  }
  if(pageToInitialize == "Admin")
  {
    InitializeAdminVariables();
  }
  if(pageToInitialize == "PINPage")
  {
    InitializePINPageVariables();
  }
  if(pageToInitialize == "PINChange")
  {
    InitializePINChangeVariables();
  }
  if(pageToInitialize == "VideoMatrix")
  {
    InitializeVideoMatrixVariables();
    sendMessage("VideoMatrix:Connect");
  }
  if(pageToInitialize != "VideoMatrix")
  {
    sendMessage("VideoMatrix:Disconnect");
  }
}