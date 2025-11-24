using UnityEngine;

namespace SaveLoadPlayerPrefs
{
    public class SaveLoad
    {
        public bool PlayerLoadingBool<T>(T savestring)
        {
            return PlayerPrefs.GetString(savestring.ToString()) == "True";
        }

        public int PlayerLoadingInt<T>(T savestring)
        {
            return PlayerPrefs.GetInt(savestring.ToString());
        }

        public float PlayerLoadingFloat<T>(T savestring)
        {
            return PlayerPrefs.GetFloat(savestring.ToString());
        }

        public string PlayerLoadingString<T>(T savestring)
        {
            return PlayerPrefs.GetString(savestring.ToString());
        }

        public void PlayerSaveInt<T>(T savestring, int value)
        {
            PlayerPrefs.SetInt(savestring.ToString(), value);
            PlayerPrefs.Save();
        }

        public void PlayerSaveFloat<T>(T savestring, float value)
        {
            PlayerPrefs.SetFloat(savestring.ToString(), value);
            PlayerPrefs.Save();
        }

        public void PlayerSaveBool<T>(T savestring, bool value)
        {
            PlayerPrefs.SetString(savestring.ToString(), value.ToString());
            PlayerPrefs.Save();
        }

        public void PlayerSaveString<T>(T savestring, string value)
        {
            PlayerPrefs.SetString(savestring.ToString(), value.ToString());
            PlayerPrefs.Save();
        }

        public bool CheckKey<T>(T key)
        {
            return PlayerPrefs.HasKey(key.ToString());
        }
    }
}
