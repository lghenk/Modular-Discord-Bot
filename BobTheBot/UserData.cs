using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BobTheBot.database;
using Newtonsoft.Json;

namespace BobTheBot {
    public class UserData {

        public string dataPath;
        public Dictionary<ulong, UserModel> userData = new Dictionary<ulong, UserModel>();

        public UserData() {
            dataPath = AppDomain.CurrentDomain.BaseDirectory + "/userData/";

            if (!Directory.Exists(dataPath)) {
                Console.WriteLine("userData :: First Time Initialized... Created Userdata folder");
                Directory.CreateDirectory(dataPath);
            } else {
                Console.WriteLine($"userData :: Initialized found {Directory.GetFiles(dataPath).Length} data files");
            }
        }

        private UserModel LoadUser(ulong id) {

            // We want to cache it to prevent blocking the IO operations.
            // We should also get rid of it after X minutes of no activity of this user
            if (!userData.ContainsKey(id)) {
                if (File.Exists(dataPath + id + ".json")) {
                    Console.WriteLine("Load Json");

                    userData.Add(id, JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(dataPath + id + ".json")));
                    return userData[id];
                } else {
                    Console.WriteLine("Create New");

                    UserModel newModel = new UserModel();
                    newModel.id = id;
                    SaveModel(newModel);
                    return newModel;
                }
            } else {
                Console.WriteLine("Load Cache");

                return userData[id];
            }
        }

        public void SaveModel(UserModel model) {
            File.WriteAllText(dataPath + model.id + ".json", JsonConvert.SerializeObject(model));
        }

        public T GetValue<T>(ulong id, string key, T def = default(T)) {

            UserModel model = LoadUser(id);

            if (model.values.ContainsKey(key)) {
                return (T)model.values[key];
            }
            
            return default(T);
        }

        public void SetValue(ulong id, string key, object value) {
            UserModel model = LoadUser(id);

            if (model.values.ContainsKey(key)) {

                model.values[key] = value;
            } else {

                model.values.Add(key, value);
            }

            SaveModel(model);
        }

    }
}
