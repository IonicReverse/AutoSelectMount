using robotManager.Helpful;
using System.Linq;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AutoSelectMount
{
  internal class Helpers
  {
    public static void Log(string message)
    {
      Logging.Write("[AutoSelectMount] " + message, Logging.LogType.Normal, System.Drawing.Color.AliceBlue);
    }

    public static int HasMount()
    {
      if (AutoSelectMountSettings.CurrentSettings.AutoSelectGroundMount) { 
        var epicMount = Data.EpicGroundMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
        if (epicMount != 0)
           return Data.EpicGroundMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
        return Data.NormalGroundMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
      }

      if (AutoSelectMountSettings.CurrentSettings.AutoSelectFlyingMount)
      {
        var epicMount = Data.EpicFlyingMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
        if (epicMount != 0)
          return Data.EpicFlyingMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
        return Data.NormalFlyingMount.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
      }

      if (AutoSelectMountSettings.CurrentSettings.AutoSelectSpellMount)
        return Data.SpellMount.Where(x => SpellManager.ExistSpellBook(SpellManager.GetSpellInfo((uint)x).Name)).FirstOrDefault();

      return 0;
    }

    public static void SetMount()
    {
      var hasMount = HasMount();
      if (hasMount != 0)
      {
        if (ObjectManager.Me.Level >= 70 
          && AutoSelectMountSettings.CurrentSettings.AutoSelectFlyingMount 
          && wManager.wManagerSetting.CurrentSetting.FlyingMountName != ItemsManager.GetNameById(hasMount)) 
        {
          wManager.wManagerSetting.CurrentSetting.UseGroundMount = false;
          wManager.wManagerSetting.CurrentSetting.UseFlyingMount = true;
          wManager.wManagerSetting.CurrentSetting.FlyingMountName = ItemsManager.GetNameById(hasMount);
          wManager.wManagerSetting.CurrentSetting.Save();
        }

        if (ObjectManager.Me.Level >= 30 
          && AutoSelectMountSettings.CurrentSettings.AutoSelectGroundMount
          && wManager.wManagerSetting.CurrentSetting.GroundMountName != ItemsManager.GetNameById(hasMount))
        {
          wManager.wManagerSetting.CurrentSetting.UseGroundMount = true;
          wManager.wManagerSetting.CurrentSetting.UseFlyingMount = false;
          wManager.wManagerSetting.CurrentSetting.GroundMountName = ItemsManager.GetNameById(hasMount);
          wManager.wManagerSetting.CurrentSetting.Save();
        }

        if (ObjectManager.Me.Level >= 30
          && AutoSelectMountSettings.CurrentSettings.AutoSelectSpellMount
          && wManager.wManagerSetting.CurrentSetting.GroundMountName != SpellManager.GetSpellInfo((uint)hasMount).Name)
        {
          wManager.wManagerSetting.CurrentSetting.UseGroundMount = true;
          wManager.wManagerSetting.CurrentSetting.UseFlyingMount = false;
          wManager.wManagerSetting.CurrentSetting.GroundMountName = SpellManager.GetSpellInfo((uint)hasMount).Name;
          wManager.wManagerSetting.CurrentSetting.Save();
        }
      }
    }

  }
}
