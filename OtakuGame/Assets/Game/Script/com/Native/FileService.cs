using UnityEngine;

namespace com
{
    public class FileService : MonoBehaviour
    {
        //download/preload/releaseˇ˘storageService
        public static FileService Instance;
        private void Awake()
        {
            Instance = this;
        }
    }


}
