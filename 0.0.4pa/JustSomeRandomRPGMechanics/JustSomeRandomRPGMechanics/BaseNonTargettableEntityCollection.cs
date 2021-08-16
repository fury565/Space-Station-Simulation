using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    public static class BaseNonTargettableEntityCollection
    {
        static List<IEntity> lootTable = new List<IEntity>();
        public static void AddToLootTable(IEntity lootToAdd)
        {
            lootTable.Add(lootToAdd);
        }
        public static void ReadLootTableFromTextFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();
                
                while (line!=null)
                {
                    string[] splitted = line.Split(',');
                    switch (splitted[5])
                    {
                        case "0":
                            Item item = new Item(Int32.Parse(splitted[0]), splitted[1], splitted[2], Double.Parse(splitted[3]), Double.Parse(splitted[4]));
                            for(int i = 6; i < splitted.Length; i+=2)
                            {
                                item.AddAbility(Int32.Parse(splitted[i]), Int32.Parse(splitted[i + 1]));
                            }
                            lootTable.Add(item);
                            break;
                        case "1":
                            List<ItemEffect> effects = new List<ItemEffect>();
                            int offset = 0;
                            for(int i = 6; i < splitted.Length; i++)
                            {
                                offset++;
                                if (splitted[i] != "-1")
                                {
                                    string[] effectComponents = splitted[i].Split(':');
                                    effects.Add(new ItemEffect(effectComponents[0], effectComponents[0], Int32.Parse(effectComponents[1])));
                                }
                                else
                                    break;
                            }
                            lootTable.Add(new UsableItem(Int32.Parse(splitted[0]), splitted[1], splitted[2], Double.Parse(splitted[3]), Double.Parse(splitted[4]),effects));
                            break;
                        case "2":
                            lootTable.Add(new Container(Int32.Parse(splitted[0]), splitted[1], splitted[2], Double.Parse(splitted[3]), Double.Parse(splitted[4]), Double.Parse(splitted[6]), Double.Parse(splitted[7])));
                            break;
                    }
                    line = file.ReadLine();
                }
            }
        }
        public static string GetAllItems()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var e in lootTable)
            {
                sb.Append(e.GetEntityDescription());
            }
            return sb.ToString();
        }
        public static IEntity GetLootAtIndex(int index)
        {
            try
            {
                return lootTable[index];
            }
            catch (Exception)
            {
                Display.DisplayMessage("No item at index found");
                return lootTable[0];
            }
            
        }
    }
}
