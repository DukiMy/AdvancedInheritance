namespace AdvancedInheritance
{
    class Axe : Item, IUsable
    {
        public int Damage { get; set; } = 15;

        public Axe()
        {
            base.UsedSlots = 2;
        }
    }
}
