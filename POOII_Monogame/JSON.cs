using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace POOII_Monogame {

    public class TestObject {
        public int id { get; set; }
        public string name { get; set; }
    }

    internal class JSON {

        public static PlayerData CreatePlayerFromJSON() {
            //string filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "TestObject.json");
            string jsonText = File.ReadAllText("../../../TestObject.json");
            PlayerData playerData = JsonSerializer.Deserialize<PlayerData>(jsonText);
            return playerData;
        }

        public static void WriteJSONFile(PlayerData player) {
            string filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "TestObject.json");
            var jsonSerialized = JsonSerializer.Serialize(player);
            File.WriteAllText("../../../TestObject.json", jsonSerialized);
        }
    }
}