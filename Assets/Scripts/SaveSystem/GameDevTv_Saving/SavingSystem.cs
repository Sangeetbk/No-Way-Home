using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace SGS.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void LoadLastScene(string saveFile)
        {

        }
        public void Save(string saveFile)
        {
           Dictionary<string,object> state = LoadFile(saveFile);  
           CaptureState(state);
           SaveFile(saveFile,state);
           Debug.Log("Game Saved");
        }


        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
            Debug.Log("Game Loaded");
        }


        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        private Dictionary<string,object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path)) 
            { 
                return new Dictionary<string, object>(); 
            }

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
        private void CaptureState(Dictionary<string, object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                string id = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath,saveFile + ".sav");
        }

    }  
}
