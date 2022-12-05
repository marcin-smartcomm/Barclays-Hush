function InitializeAdminVariables()
{
  backBtn = document.getElementById("backBtn")
  
  if(backBtn)
  {
    backBtn.addEventListener('click', function() {
      openSubpage("Home");
    });
  }

  document.getElementById("video-matrix-btn").addEventListener('click', function()
  {
    openSubpage("VideoMatrix");
  });
  
  document.getElementById("change-pin-btn").addEventListener('click', function()
  {
    changingPIN = true;
    openSubpage("PINChange");
  })
}