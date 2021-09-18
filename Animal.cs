namespace AdvancedInheritance
{
    class Animal : IDamageable, IGreetable
    {
        public int Health { get; set; }

        public virtual string Reply()
        {
            return "...";
        }
    }
}
