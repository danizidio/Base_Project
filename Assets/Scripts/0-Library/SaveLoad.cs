using UnityEngine;

namespace SaveLoadPlayerPrefs
{
    public class SaveLoad
    {
        public bool PlayerLoadingBool(SaveStrings savestring)
        {
            return PlayerPrefs.GetString(savestring.ToString()) == "True";
        }

        public int PlayerLoadingInt(SaveStrings savestring)
        {
            return PlayerPrefs.GetInt(savestring.ToString());
        }

        public float PlayerLoadingFloat(SaveStrings savestring)
        {
            return PlayerPrefs.GetFloat(savestring.ToString());
        }

        public string PlayerLoadingString(SaveStrings savestring)
        {
            return PlayerPrefs.GetString(savestring.ToString());
        }

        public void PlayerSaveInt(SaveStrings savestring, int value)
        {
            PlayerPrefs.SetInt(savestring.ToString(), value);
            PlayerPrefs.Save();
        }

        public void PlayerSaveFloat(SaveStrings savestring, float value)
        {
            PlayerPrefs.SetFloat(savestring.ToString(), value);
            PlayerPrefs.Save();
        }

        public void PlayerSaveBool(SaveStrings savestring, bool value)
        {
            PlayerPrefs.SetString(savestring.ToString(), value.ToString());
            PlayerPrefs.Save();
        }

        public void PlayerSaveString(SaveStrings savestring, string value)
        {
            PlayerPrefs.SetString(savestring.ToString(), value.ToString());
            PlayerPrefs.Save();
        }
    }
}
