let dayStates = [],
    onTimes = [],
    offTimes = [];


function InitializeSchedulerVariables()
{
  backBtn = document.getElementById("backBtn")
  
  if(backBtn)
  {
    backBtn.addEventListener('click', function() {
      openSubpage("Home");
    });
  }

  for(let i = 0; i < 7; i++)
  {
    document.getElementById(`day${i}-On-Off`).addEventListener('click', function()
    {
      sendMessage(`Scheduler:DayState:${i}:${document.getElementById(`day${i}-On-Off`).checked}`);
    })

    document.getElementById(`day${i}OnTime`).addEventListener('change', function()
    {
      sendMessage(`Scheduler:OnTime:${i}:${document.getElementById(`day${i}OnTime`).value}`);
    })

    document.getElementById(`day${i}OffTime`).addEventListener('change', function()
    {
      sendMessage(`Scheduler:OffTime:${i}:${document.getElementById(`day${i}OffTime`).value}`);
    })
  }

  FillData();
}


function ProcessScheduledDaysData(scheduleData)
{
  for(let i = 0; i < 7; i++)
  {
    dayStates[i] = scheduleData.states[i];
    onTimes[i] = scheduleData.onTimes[i];
    offTimes[i] = scheduleData.offTimes[i];
  }

  if(currentSubpage == "Scheduler")
  {
    FillData();
  }
}

function FillData()
{
  for(let i = 0; i < 7; i++)
  {
    let dayState = false;

    if(dayStates[i] == true)
      dayState = true;

    document.getElementById(`day${i}-On-Off`).checked = dayState;
    document.getElementById(`day${i}OnTime`).value = onTimes[i];
    document.getElementById(`day${i}OffTime`).value = offTimes[i];
  }
}