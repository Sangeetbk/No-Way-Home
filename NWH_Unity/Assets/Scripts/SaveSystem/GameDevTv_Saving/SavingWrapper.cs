using UnityEngine;

namespace SGS.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);

            }
        }
    }
}
