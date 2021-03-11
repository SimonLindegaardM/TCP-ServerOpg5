using System;

namespace TCP_ServerOpg5
{
    public class Beer
    {
        private int _id;
        private string _name;
        private double _price;
        private double _abv;

        public Beer(int id, string name, double price, double abv)
        {
            Id = id;
            Name = name;
            Price = price;
            Abv = abv;
        }

        public Beer()
        {

        }

        //public int Id { get; set; }
        public int Id
        {
            get => _id;
            set { _id = value; }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (value.Length < 4)
                {
                    throw new ArgumentException();
                }

                _name = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _price = value;
            }
        }

        public double Abv
        {
            get => _abv;
            set
            {
                if (value <= 0 || value >= 100)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _abv = value;
            }
        }

        public override string ToString()
        {
            return Id + " " + Name + " " + Price + " " + Abv;
        }
    }
}