﻿using DO;

namespace Dal;

public class DalProduct
{
    #region Create
    public int Create(Product myProduct)
    {
        int myID;
        Random rand = new Random();
        do
        {
            myID = rand.Next(100000, 999999);
        } while (existProductID(myID));

        myProduct.ID = myID;
        DataSource.ProductArr[DataSource.Config.ProductIndex++] = myProduct;
        Console.WriteLine("myProduct.ID: " + myProduct.ID);
        return myProduct.ID;
    }
    #endregion
    #region Update
    public void Update(Product myProduct)
    {

        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == myProduct.ID)
            {
                DataSource.ProductArr[i] = myProduct;
                return;
            }
        }
        throw new Exception("not exist product");
    }
    #endregion
    #region Delete
    public void Delete(int ProductId)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == ProductId)
            {
                DataSource.ProductArr[i] = DataSource.ProductArr[DataSource.Config.ProductIndex];
                DataSource.Config.ProductIndex--;
                return;
            }
        }
        throw new Exception("not exist product");
    }
    #endregion
    #region Get
    public Product Get(int ProductId)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            Console.Write(DataSource.ProductArr[i].Name, DataSource.ProductArr[i].ID);
            Product CurrentProduct = DataSource.ProductArr[i];
            if (CurrentProduct.ID == ProductId)
                return CurrentProduct;
        }
        throw new Exception("not exist product");
    }
    #endregion
    #region GetAll
    public Product[] GetAll()
    {
        int size = DataSource.Config.ProductIndex;
        Console.Write("size of ProductIndex in getall func");
        Console.Write(size);

        Product[] newProductArr = new Product[size];
        for (int i = 0; i < size; i++)
        {
            newProductArr[i] = DataSource.ProductArr[i];
        }
        return newProductArr;
    }
    #endregion

    private bool existProductID(int num)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == num)
                return true;
        }
        return false;
    }
}