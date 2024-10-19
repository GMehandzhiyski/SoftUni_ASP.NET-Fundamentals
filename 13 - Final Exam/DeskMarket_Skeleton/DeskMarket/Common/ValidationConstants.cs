namespace DeskMarket.Common
{
    public static class ValidationConstants
    {
        //Product
        //ProductName
        public const int ProductProductNameMinLenght = 2;
        public const int ProductProductNameMaxLenght = 60;

        //Description
        public const int ProductDescriptionMinLenght = 10;
        public const int ProductDescriptionMaxLenght = 250;

        //Price
        public const decimal ProductPriceMinLenght = 1.00m;
        public const decimal ProductPriceMaxLenght = 3000.00m;

        //Category
        //Name
        public const int CategoryNameMinLenght = 3;
        public const int CategoryNameMaxLenght = 20;

    }
}
