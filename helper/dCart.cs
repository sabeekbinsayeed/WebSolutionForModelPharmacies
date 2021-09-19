using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace Helper
{
    public class dCart 
    {
        public static bool Check(string cartName)
        {
            List<CartItem> cart = (List<CartItem>)System.Web.HttpContext.Current.Session[cartName];

            if (cart != null){
                CartViewModel cartView = new CartViewModel(){CartItems = cart};
                return true;
            }

            return false;

        }
        public static List<CartItem> Get(string cartName)
        {
            List<CartItem> cart = (List<CartItem>)System.Web.HttpContext.Current.Session[cartName];
            if (cart != null){
                CartViewModel cartView = new CartViewModel(){ CartItems = cart};
                return cart;
            }
            return null;
        }


        /*
         *  Session empty hole product direct add hobe
         *  Session empty na hole 2ta case
         *    case1: jodi product ta age thekei thake then or quantity sudhu increase korbo
         *    case2: jodi na thake tahole just cart list ee add hobe
         */
        public static void Add(int productId, int quantity, int pricingId, string cartName)
        {
            string NCartId = productId.ToString() + "_" + pricingId.ToString();
            Product productModel = new Product();   
            if (System.Web.HttpContext.Current.Session[cartName] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem
                {
                    CartId = NCartId,
                    Product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId),
                    Pricing = Database.getContext().Pricing.SingleOrDefault(c => c.Id == pricingId),
                    Cart_Product_Quantity = quantity
                });
                System.Web.HttpContext.Current.Session[cartName] = cart;
            }
            else
            {
                List<CartItem> cart = Get(cartName);
                int index = isExist(NCartId, cartName);
                if (index != -1)
                {
                    cart[index].Cart_Product_Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        CartId = NCartId,
                        Product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId),
                        Pricing = Database.getContext().Pricing.SingleOrDefault(c => c.Id == pricingId),
                        Cart_Product_Quantity = quantity
                    }); ;
                }
                System.Web.HttpContext.Current.Session[cartName] = cart;
            }
        }

        public static void Remove(string cartId,string cartName)
        {
            List<CartItem> cart = Get(cartName);
            int index = isExist(cartId, cartName);
            cart.RemoveAt(index);
            System.Web.HttpContext.Current.Session[cartName] = cart;
        }


        public static void RemoveQty(string cartId,int qty, string cartName)
        {
            List<CartItem> cart = Get(cartName);
            int index = isExist(cartId, cartName);

            if ((cart[index].Cart_Product_Quantity - qty) > 0){
                cart[index].Cart_Product_Quantity -= qty;
                System.Web.HttpContext.Current.Session[cartName] = cart;
            }
            else{
                cart.RemoveAt(index);
            }
        }


        private static int isExist(string CartId , string cartName)
        {
            List<CartItem> cart = Get(cartName);

            if (cart.FindIndex(c => c.CartId == CartId) != null){
                return cart.FindIndex(c => c.CartId == CartId);
            }

            return -1;
        }

        public static void IncreaseQuantity(int productId, int quantity, int pricingId , string cartName)
        {
            string NCartId = productId.ToString() + "_" + pricingId.ToString();

            List<CartItem> cart = Get(cartName);
            int index = isExist(NCartId, cartName);
            if (index != -1){
                cart[index].Cart_Product_Quantity += quantity;
            }

        }





    }
}


