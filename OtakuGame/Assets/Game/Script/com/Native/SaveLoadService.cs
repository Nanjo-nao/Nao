using UnityEngine;
using game;

namespace com
{
    public class SaveLoadService : MonoBehaviour
    {
        public static SaveLoadService instance;

        public enum StorageType
        {
            PlayerPrefs,
            EncryptedPrefs,
            EncryptedFile,
        }

        public StorageType storageType;
        public IStorageService storageService { get; private set; }

        private void Awake()
        {
            instance = this;
            switch (storageType)
            {
                case StorageType.EncryptedFile:
                    storageService = new EncryptedFileStorage();
                    break;

                case StorageType.EncryptedPrefs:
                    storageService = new EncryptedPrefsStorage();
                    break;

                case StorageType.PlayerPrefs:
                    storageService = new PlayerPrefsStorage();
                    break;
            }
        }

        private string _prefix = "";

        public void SetPrefix(string prefix1, string prefix2)
        {
            _prefix = StorageKey.SavePrefix + "_" + prefix1 + "_" + prefix2 + "_";
        }

        private string GetPrefixedKey(string key)
        {
            return _prefix + key;
        }

        private string GetSimpleKey(string prefix1, string prefix2)
        {
            return StorageKey.SavePrefix + "_" + prefix1 + "_" + prefix2;
        }

        private T LoadObject<T>(string key, T defaultValue)
        {
            string loaded = storageService.GetString(key, null);
            //Debug.Log("-----LoadObject " + key + " loaded:");
            //Debug.Log(loaded);
            return loaded == null ? defaultValue : JsonUtility.FromJson<T>(loaded);
        }
    }
}
