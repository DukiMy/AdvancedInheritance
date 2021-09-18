namespace AdvancedInheritance
{
    class Sword : Item, IUsable
    {
        public int Damage { get; set; } = 20;

        public Sword()
        {
            base.UsedSlots = 3;
        }
    }
}
