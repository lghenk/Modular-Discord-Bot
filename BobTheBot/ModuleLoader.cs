using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BobTheBot {
    class ModuleLoader {
        private string _modulePath;

        public ModuleLoader() {
            _modulePath = AppDomain.CurrentDomain.BaseDirectory + "Modules\\";

            if (!Directory.Exists(_modulePath)) {
                Console.WriteLine("ModuleLoader :: Module directory does not exist... Creating");
                Directory.CreateDirectory(_modulePath);
            } else {
                List<string> loadedModules = new List<string>();
                foreach (string module in BotMain.instance.settings.Options.Modules) {
                    Console.WriteLine($"ModuleLoader :: Attempting to load module {module}");

                    if (loadedModules.Contains(module)) {
                        Console.WriteLine($"ModuleLoader :: Unable to load module {module} because its already loaded");
                        continue; ;
                    }

                    if (File.Exists(_modulePath + module + ".dll")) {
                        Assembly DLL = Assembly.LoadFrom(_modulePath + module + ".dll");

                        Type classType = DLL.GetType(String.Format("{0}.{1}", module, "Main"));
                        if (classType != null) {
                            // Create class instance.
                            Activator.CreateInstance(classType);

                            Console.WriteLine($"ModuleLoader :: Successfully loaded module {module}");
                            loadedModules.Add(module);
                        } else {
                            Console.WriteLine($"ModuleLoader :: Failed Attempting to load module {module} Could not find Main class 'Main' in namespace {module}");
                        }
                    } else {
                        Console.WriteLine($"ModuleLoader :: Failed Attempting to load module {module} Could not find module");
                    }
                }
            }
        }
    }
}
