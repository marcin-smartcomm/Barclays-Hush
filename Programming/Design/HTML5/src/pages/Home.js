let blankOutScreensBtn;
function InitializeHomeVariables()
{
  blankOutScreensBtn = document.getElementById("balnkOutBtnsContainer");
  document.getElementById("schedulerBtn").addEventListener('click', function() {
    //InSubpageManager
    openSubpage("Scheduler")
  })
  document.getElementById("blankScreensBtn").addEventListener('click', function() {
    //InSubpageManager
    ToggleBlankOutBtns();
  })
  document.getElementById("adminBtn").addEventListener('click', function() {
    //InSubpageManager
    openSubpage("Admin")
  })
}