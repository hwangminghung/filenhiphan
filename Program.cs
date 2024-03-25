using System;
using System.Collections.Generic;
using System.IO;

class Product
{
    public int ProductCode { get; set; }
    public string ProductName { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "products.bin";

        while (true)
        {
            Console.WriteLine("1. Thêm sản phẩm");
            Console.WriteLine("2. Hiển thị danh sách sản phẩm");
            Console.WriteLine("3. Tìm kiếm sản phẩm");
            Console.WriteLine("4. Thoát");
            Console.Write("Vui lòng chọn một tùy chọn: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddProduct(filePath);
                    break;
                case "2":
                    DisplayProducts(filePath);
                    break;
                case "3":
                    SearchProduct(filePath);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Tùy chọn không hợp lệ.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AddProduct(string filePath)
    {
        Console.WriteLine("Nhập thông tin sản phẩm:");

        Console.Write("Mã sản phẩm: ");
        int productCode = int.Parse(Console.ReadLine());

        Console.Write("Tên sản phẩm: ");
        string productName = Console.ReadLine();

        Console.Write("Hãng sản xuất: ");
        string manufacturer = Console.ReadLine();

        Console.Write("Giá: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Mô tả: ");
        string description = Console.ReadLine();

        Product product = new Product
        {
            ProductCode = productCode,
            ProductName = productName,
            Manufacturer = manufacturer,
            Price = price,
            Description = description
        };

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Append)))
        {
            writer.Write(product.ProductCode);
            writer.Write(product.ProductName);
            writer.Write(product.Manufacturer);
            writer.Write(product.Price);
            writer.Write(product.Description);
        }

        Console.WriteLine("Sản phẩm đã được thêm thành công.");
    }

    static void DisplayProducts(string filePath)
    {
        Console.WriteLine("Danh sách sản phẩm:");

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.OpenOrCreate)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int productCode = reader.ReadInt32();
                string productName = reader.ReadString();
                string manufacturer = reader.ReadString();
                decimal price = reader.ReadDecimal();
                string description = reader.ReadString();

                Console.WriteLine($"Mã sản phẩm: {productCode}");
                Console.WriteLine($"Tên sản phẩm: {productName}");
                Console.WriteLine($"Hãng sản xuất: {manufacturer}");
                Console.WriteLine($"Giá: {price}");
                Console.WriteLine($"Mô tả: {description}");
                Console.WriteLine();
            }
        }
    }

    static void SearchProduct(string filePath)
    {
        Console.Write("Nhập mã sản phẩm cần tìm kiếm: ");
        int searchProductCode = int.Parse(Console.ReadLine());

        bool found = false;

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.OpenOrCreate)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int productCode = reader.ReadInt32();
                string productName = reader.ReadString();
                string manufacturer = reader.ReadString();
                decimal price = reader.ReadDecimal();
                string description = reader.ReadString();

                if (productCode == searchProductCode)
                {
                    Console.WriteLine($"Mã sản phẩm: {productCode}");
                    Console.WriteLine($"Tên sản phẩm: {productName}");
                    Console.WriteLine($"Hãng sản xuất: {manufacturer}");
                    Console.WriteLine($"Giá: {price}");
                    Console.WriteLine($"Mô tả: {description}");
                    Console.WriteLine();

                    found = true;
                    break;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("Không tìm thấy sản phẩm có mã tương ứng.");
        }
    }
}