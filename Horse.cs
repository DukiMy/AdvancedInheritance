namespace AdvancedInheritance
{
    class Horse : Animal
    {
        public override string Reply()
        {
            return "Neigh neeiiiigh";
        }

        public Horse()
        {
            base.Health = 125;
        }
    }
}
