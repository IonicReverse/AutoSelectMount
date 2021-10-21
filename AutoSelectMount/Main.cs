using System;
using System.ComponentModel;
using System.Threading;
using AutoSelectMount;
using wManager.Plugin;
using wManager.Wow.Helpers;

public class Main : IPlugin
{

  public static bool isRunning;
  private BackgroundWorker pulseThread;

  public void Start()
  {
    pulseThread = new BackgroundWorker();
    pulseThread.DoWork += Pulse;
    pulseThread.RunWorkerAsync();
  }

  public void Pulse(object sender, DoWorkEventArgs args)
  {
    try
    {
      while (isRunning)
      {
        if (Conditions.InGameAndConnectedAndAliveAndProductStarted)
        {
          Helpers.SetMount();
          Thread.Sleep(1000);
        }
      }
    }
    catch (Exception ex)
    {
      Helpers.Log("Something wrong.. " + ex);
    }
  }

  public void Dispose()
  {
    isRunning = false;
    Helpers.Log("Stopped");
  }

  public void Initialize()
  {
    try
    {
      isRunning = true;
      AutoSelectMountSettings.Load();
      Start();
    }
    catch (Exception ex)
    {
      Helpers.Log("Something wrong : " + ex);
    }
  }

  public void Settings()
  {
    AutoSelectMountSettings.Load();
    AutoSelectMountSettings.CurrentSettings.ToForm();
    AutoSelectMountSettings.CurrentSettings.Save();
  }

}