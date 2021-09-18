namespace AdvancedInheritance
{
    class Mouse : Animal
    {
        public override string Reply()
        {
            return "Squeak squeak";
        }

        public Mouse()
        {
            base.Health = 30;
        }
    }
}
