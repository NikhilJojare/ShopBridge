using NUnit.Framework;
using ShopBridge.Controllers;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.Routing;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using System.Net;

namespace ShopBridgeTests
{
    [TestFixture]
    public class ItemControllerTests
    {
        private ItemMastersController itemMasterController;
        private const string ServiceBaseURL = "http://localhost:64258/";
        private List<ItemMaster> itemMasters;

        [SetUp]
        public void SetUp()
        {
            itemMasterController = new ItemMastersController();
        }



        /// <summary>
        /// Get all item list
        /// </summary>
        [Test]
        public void GetItemMasters()
        {
      
            // Act
            itemMasters = itemMasterController.GetItemMasters().ToList();

            // Assert
            Assert.IsTrue(itemMasters.Count() > 0);
        }

        /// <summary>
        /// Get particular item master details
        /// </summary>
        [Test]
        public void GetItemMaster()
        {
            // Act
          ItemMaster itemMaster = itemMasterController.GetItemMaster(1).Queryable.FirstOrDefault();

            // Assert
            Assert.AreEqual(1, itemMaster.ItemId);
        }

     
        /// <summary>
        /// Create Item Test
        /// </summary>
        [Test]
        public void CreateItem()
        {
            //Arrange
            var itemMasterController = new ItemMastersController()
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post
                    //,RequestUri = new Uri(ServiceBaseURL + "odata/ItemMasters")
                }
            };
            itemMasterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act
            ItemMaster itemMaster = new ItemMaster()
            {
                ItemName = "Lenovo 300 USB Keyboard",
                Price = 990,
                Description = "A keyboard & mouse combo that features a modern, space-saving design giving your desk a clean and stylish appeal",
                ImagePath = null,
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "Nikhil Jojare"
            };
            
            var response = itemMasterController.Post(itemMaster).Result;

            // Assert
            Assert.IsTrue(itemMaster.ItemId > 0);
        }

        /// <summary>
        /// Updates the item test.
        /// </summary>
        [Test]
        public void UpdateItemTest()
        {
            //Arrange
            var itemMasterController = new ItemMastersController()
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put
                }
            };
            itemMasterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act
            ItemMaster itemMaster = new ItemMaster()
            {   ItemId = 10,
                ItemName = "Lenovo USB Mouse",
                Price = 200,
                Description = "Lenove USB Mouse description",
                ImagePath = null,
                IsActive = true,
                Modified = DateTime.Now,
                ModifiedBy = "Nikhil Jojare"
            };
          
            
            var response = itemMasterController.Put(itemMaster.ItemId, itemMaster).Result;

            // Assert
            Assert.IsTrue(itemMaster.ItemId > 0);
        }

        /// <summary>
        /// Deletes permaneant the item test.
        /// </summary>
        [Test]
        public void DeletePermenantItemTest()
        {
            //Arrange
            var itemMasterController = new ItemMastersController()
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete
                }
            };
            itemMasterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act
            ItemMaster itemMaster = new ItemMaster()
            {
                ItemId = 9
            };

            var response = itemMasterController.Delete(itemMaster.ItemId).Result;

            // Assert
            Assert.IsTrue(itemMaster.ItemId > 0);
        }

        /// <summary>
        /// Temporaries the delete item. (IsActive = false)
        /// </summary>
        [Test]
        public void TemporaryDeleteItem()
        {
            //Arrange
            var itemMasterController = new ItemMastersController()
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put
                }
            };
            itemMasterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());


            // Act
            ItemMaster itemMaster = itemMasterController.GetItemMaster(10).Queryable.FirstOrDefault();
            itemMaster.IsActive = false;
            itemMaster.Modified = DateTime.Now;
            itemMaster.ModifiedBy = "Nikhil Jojare ";

            var response = itemMasterController.Put(itemMaster.ItemId, itemMaster).Result;

            // Assert
            Assert.IsTrue(itemMaster.ItemId > 0);
        }
    }

   
}




