namespace WCFArticle
{
    public class FileArticle
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public FileArticle()
        {

        }

        public FileArticle(string name, int quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return ($"{Name}; {Quantity}; {Price}");
        }
    }
}