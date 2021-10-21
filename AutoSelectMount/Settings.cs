using robotManager.Helpful;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

[Serializable]
public class AutoSelectMountSettings : Settings
{


  [Setting]
  [DefaultValue(true)]
  [Category("Settings")]
  [DisplayName("Use Ground Mount")]
  [Description("Auto Select Ground Mount")]
  public bool AutoSelectGroundMount { get; set; }

  [Setting]
  [DefaultValue(false)]
  [Category("Settings")]
  [DisplayName("Use Flying Mount")]
  [Description("Auto Select Flying Mount")]
  public bool AutoSelectFlyingMount { get; set; }

  [Setting]
  [DefaultValue(false)]
  [Category("Settings")]
  [DisplayName("Use Spell Mount")]
  [Description("Auto Select Spell Mount")]
  public bool AutoSelectSpellMount { get; set; }

  public AutoSelectMountSettings()
  {
    AutoSelectGroundMount = true;
    AutoSelectFlyingMount = false;
    AutoSelectSpellMount = false;
  }

  public static AutoSelectMountSettings CurrentSettings { get; set; }

  public bool Save()
  {
    try
    {
      return Save(AdviserFilePathAndName("AutoSelectMount", ObjectManager.Me.Name + "." + Usefuls.RealmName));
    }
    catch (Exception e)
    {
      Logging.WriteError("[AutoSelectMountSettings] > Save() : " + e);
      return false;
    }
  }

  public static bool Load()
  {
    try
    {
      if (File.Exists(AdviserFilePathAndName("AutoSelectMount", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
      {
        CurrentSettings = Load<AutoSelectMountSettings>(AdviserFilePathAndName("AutoSelectMount", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        return true;
      }
      CurrentSettings = new AutoSelectMountSettings();
    }
    catch (Exception e)
    {
      Logging.WriteError("[AutoSelectMountSettings] > Load() : " + e);
    }
    return false;
  }
}