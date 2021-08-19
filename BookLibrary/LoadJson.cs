using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BookLibrary
{
    class LoadJson <T>
    {
        public List<T> Items { get; set; }
        private StreamReader r;
        public LoadJson()
        {
            Items = new List<T>();
        }
        public List<T>LoadFromFile(string path)
        {
            r = new StreamReader(path);
            string json = r.ReadToEnd();
            Items = JsonConvert.DeserializeObject<List<T>>(json);
            return Items;
        }
    }
}