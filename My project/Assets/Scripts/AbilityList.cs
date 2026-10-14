using UnityEngine;

public class AbilityList
{
    public struct Ability
    {
        public string name;
        public string description;
        public int w;
        public int h;
        public float damageScale;

        public Ability(string _name, string _description, int _w, int _h, float _damageScale)
        {
            name = _name;
            description = _description;
            w = _w;
            h = _h;
            damageScale = _damageScale;
        }
    }

    public Ability melee_ab1 = new Ability("Forward Poke", "", 1, 4, 1.2f);
    public Ability melee_ab2 = new Ability("Forward Poke", "", 3, 1, 1.2f);
    public Ability melee_ab3 = new Ability("Sweet Sweep", "", 3, 1, 1.2f);
    public Ability melee_ab4 = new Ability("Forward Poke", "", 3, 10, 1.2f);
}
