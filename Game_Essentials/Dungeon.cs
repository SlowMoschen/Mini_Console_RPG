using Zombie;
using Game_Essentials;
using Game_Characters;
using _Items;

namespace Dungeon_Generator {

    public class Dungeon {

        public Room[] rooms { get; set; }
        public int totlaRooms { get; set; }
        public Chest chest { get; set; }

        public Dungeon(string difficulty) {
            this.rooms = generateDungeon(difficulty);
            this.totlaRooms = this.rooms.Length;
            this.chest = new Chest(difficulty);
        }
            
            public static Room[] generateDungeon(string difficulty) {

                int rooms;
                switch (difficulty)
                {
                    case "Easy":
                        rooms = GameVariables.GameSettings.DungeonSettings.easyRooms;
                        break;
                    case "Medium":
                        rooms = GameVariables.GameSettings.DungeonSettings.mediumRooms;
                        break;
                    case "Hard":
                        rooms = GameVariables.GameSettings.DungeonSettings.hardRooms;
                        break;
                    default:
                        rooms = 1;
                        break;
                }
                Room[] dungeon = new Room[rooms];
    
                for (int i = 0; i < rooms; i++)
                {
                    dungeon[i] = new Room(i + 1, difficulty);
                }
    
                return dungeon;
            }

        public void openChest(Player player) {
            if(this.chest.items.Length > 0) {
                Console.WriteLine(" You found a chest!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
                Console.WriteLine(" You found " + this.chest.items.Length + " items!");
                foreach (Item item in this.chest.items)
                {
                    Console.WriteLine(" You found " + item.type + "!");
                    player.InventoryManager.addItem(item);
                }
            } else {
                Console.WriteLine(" You found a chest!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
                GameVariables.GameStats.totalGold += this.chest.gold;
                // GameVariables.GameLoopBooleans.isInMenu = true;
                // GameVariables.GameLoopBooleans.isInFight = false;
                // GameVariables.GameLoopBooleans.isDungeonCleared = true;
            }
        }
    }

    public class Room {

        public int roomID { get; set; }
        public Enemy[] roomEnemies { get; set; }

        public Room(int roomID, string difficulty) {
            this.roomID = roomID;
            this.roomEnemies = generateRoom(difficulty);
        }

        // Generate a room of enemies
        public static Enemy[] generateRoom(string difficulty) {
            Enemy[] enemies;
            switch (difficulty)
            {
                case "Easy":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.easyMobs);
                    break;
                case "Medium":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.mediumMobs);
                    break;
                case "Hard":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.hardMobs);
                    break;
                default:
                    enemies = generateMobs(1);
                    break;
            }
            return enemies;
        }

        // Generate a number of zombies
        public static Enemy[] generateMobs(int count)
        {
            Enemy[] enemies = new Enemy[count];
            string[] enemytypes = GameVariables.GameSettings.DungeonSettings.enemyTypes;
            Random random = new Random();
            int index = random.Next(enemytypes.Length);

            string enemyType = enemytypes[index];

            for (int i = 0; i < count; i++)
            {
                switch (enemyType)
                {
                    case "Spider":
                        enemies[i] = new _Spider.Spider(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Zombie":
                        enemies[i] = new Zombie.Zombie(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Goblin":
                        enemies[i] = new _Goblin.Goblin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Goblin.attack,
                            strength: GameVariables.EnemyStats.Goblin.strength,
                            armor: GameVariables.EnemyStats.Goblin.armor,
                            health: GameVariables.EnemyStats.Goblin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Goblin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Goblin.goldOnDefeat
                        );
                        break;
                    case "Assassin":
                        enemies[i] = new _Assassin.Assassin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Assassin.attack,
                            strength: GameVariables.EnemyStats.Assassin.strength,
                            armor: GameVariables.EnemyStats.Assassin.armor,
                            health: GameVariables.EnemyStats.Assassin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Assassin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Assassin.goldOnDefeat
                        );
                        break;
                    case "Stone Golem":
                        enemies[i] = new _StoneGolem.StoneGolem(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.StoneGolem.attack,
                            strength: GameVariables.EnemyStats.StoneGolem.strength,
                            armor: GameVariables.EnemyStats.StoneGolem.armor,
                            health: GameVariables.EnemyStats.StoneGolem.health,
                            experienceOnDefeat: GameVariables.EnemyStats.StoneGolem.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.StoneGolem.goldOnDefeat
                        );
                        break;
                }
            }

            return enemies;
        }
    }

    public class Chest {
        public Item[] items { get; set; }
        public int gold { get; set; }

        public Chest(string difficulty) {
            this.items = generateItems(difficulty);
            this.gold = generateGold(difficulty);
        }

        public static int generateGold(string difficulty) {
            int gold = 0;
            switch (difficulty)
            {
                case "Easy":
                    gold = GameVariables.GameSettings.DungeonSettings.easyGold;
                    break;
                case "Medium":
                    gold = GameVariables.GameSettings.DungeonSettings.mediumGold;
                    break;
                case "Hard":
                    gold = GameVariables.GameSettings.DungeonSettings.hardGold;
                    break;
                default:
                    gold = 0;
                    break;
            }
            return gold;
        }

        // genrates the Items based on
        public static Item[] generateItems(string difficulty) {
            int length = GameVariables.GameSettings.DungeonSettings.getChestItemsCount(difficulty);
            Item[] items = new Item[length];

            //25% Chance to generate Items
            if(GameVariables.getChance(25)) {
                for (int i = 0; i < length; i++)
                {
                    items[i] = generateItem(difficulty);
                }

                return items;
            }

            // If chance fails, return empty array
            return [];
        }

        public static Item generateItem(string difficulty) {
            string[] itemTypes = GameVariables.GameSettings.DungeonSettings.chestItems;
            Random random = new Random();
            int index = random.Next(itemTypes.Length);

            string itemType = itemTypes[index];

            switch (itemType)
            {
                case "Health Potion":
                    return new Potion(
                            name: "Endurance Potion",
                            type: "Endurance Potion",
                            description: "Increases the Player's endurance by " + GameVariables.GameSettings.endurancePotionEnduranceRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.endurancePotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.endurancePotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.endurancePotionEnduranceRating
                        );
                case "Endurance Potion":
                    return new Potion(
                            name: "Heal Potion", 
                            type: "Health Potion", 
                            description: "Heals the Player for " + GameVariables.GameSettings.healPotionHealRating + " health", 
                            price: GameVariables.GameSettings.ItemPrices.healPotionPrice, 
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.healPotionMaxQuantity, 
                            effectValue: GameVariables.GameSettings.healPotionHealRating
                        );
                case "Strength Potion":
                    return new Potion(
                            name: "Strength Potion",
                            type: "Strength Potion",
                            description: "Increases the Player's strength by " + GameVariables.GameSettings.strengthPotionStrengthRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.strengthPotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.strengthPotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.strengthPotionStrengthRating
                        );
                default:
                    return new Potion(
                            name: "Endurance Potion",
                            type: "Endurance Potion",
                            description: "Increases the Player's endurance by " + GameVariables.GameSettings.endurancePotionEnduranceRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.endurancePotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.endurancePotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.endurancePotionEnduranceRating
                        );
            }
        }
    }
}