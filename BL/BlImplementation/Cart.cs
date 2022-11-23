using BlApi;
using BO;
using DalApi;
using System.Collections;

namespace BlImplementation;

internal class Cart : ICart
{

    private IDal Dal = new Dal.DalList();

    public int ConfirmOrder(BO.Cart val)
    {
        DO.Product tempProduct = new DO.Product()//copy to new obj with instock-1
        {
            //ID = productItem.ID,
            //Name = productItem.Name,
            //Price = productItem.Price,
            //Category = (DO.Enums.Category)productItem.Category,
            //InStock = productItem.InStock - 1,//dropping  1
        };
        Dal.Product.Update(tempProduct);//update product in dal
        throw new NotImplementedException();
    }

    public BO.Cart Create(BO.Cart CartBL, int OrderItemID)
    {
        IEnumerable<DO.Product> productList =Dal.Product.GetAll();

        //מחפש את הפריט בסל הקניות
        foreach (var item in CartBL.Items)
        {
            if (item.OrderItemID == OrderItemID)//found item
            {
                //מחפש את הפריט בשכבת הנתונים כדי למצוא כמות במלאי
                foreach (DO.Product productItem in productList)
                {
                    if (productItem.ID == OrderItemID)//fount product in dal
                        if (productItem.InStock > 0)//cheaking if instock
                        {
                            item.Amount++;//update current cart.
                            item.Price = item.Price+productItem.Price;
                        }
                        else
                            throw new Exception();
                    return CartBL;
                }
            }
        }
        //if item not in cart:
        foreach (DO.Product productItem in productList)//loop to find product in dal
        {
            if (productItem.ID == OrderItemID)//fount product in dal
                if (productItem.InStock > 0)//cheaking if instock
                {
                    //create a new order item
                    BO.OrderItem tempOrderItem = new BO.OrderItem()
                    {
                     Amount=1,
                     Name= productItem.Name,
                     OrderItemID= OrderItemID,
                     Price= productItem.Price,
                     ProductID= productItem.ID,
                     TotalPrice= productItem.Price
                    };
                    //update CartBL in BO, add he product and update total price 
                    CartBL.Items.Add(tempOrderItem);
                    CartBL.TotalPrice = CartBL.TotalPrice+ productItem.Price;
                }
                else
                    throw new Exception();
            return CartBL;
        }
        return CartBL;
    }

    public IEnumerator<DO.Product> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Cart val)
    {
        throw new NotImplementedException();
    }
}
