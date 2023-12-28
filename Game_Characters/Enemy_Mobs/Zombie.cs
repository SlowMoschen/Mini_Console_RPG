using Game_Characters;

namespace Zombie
{
    public class Zombie : Enemy
    {
        public Zombie(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            this.specialAttackCount = 2;
        }

        // Bite Attack - heals zombie for half the damage dealt
        public void bite(Character target)
        {
            if(target.isDefending) {
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;
                this.health += (this.attack * this.strength / target.armor) / 2;
            }
        }

        // Thrash Attack - deals damage to target and self
        public void thrash(Character target)
        {
            if(target.isDefending) {
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;
                this.health -= (this.attack * this.strength / target.armor) / 4;
            }
        }

        // Function to execute a random attack based on the attack count
        public override string executeMove(Character target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.bite(target);
                    move = "bite";
                    break;
                case 2:
                    this.thrash(target);
                    move = "thrash";
                    break;
                case 3:
                    this.Attack(target);
                    move = "attack";
                    break;
                case 4:
                    this.Defend(target);
                    move = "defend";
                    break;
            }
            return move;
        }

    }
}