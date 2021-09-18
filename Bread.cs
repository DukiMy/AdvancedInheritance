namespace AdvancedInheritance
{
    class Bread : Item, IEdible
    {
        public int HealAmount { get; set; } = 50;
    }
}
