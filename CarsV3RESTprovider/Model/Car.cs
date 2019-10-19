namespace CarsV3RESTprovider.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Vendor { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return Id + " " + Vendor + " " + Model + " " + Price;
        }
    }
}
