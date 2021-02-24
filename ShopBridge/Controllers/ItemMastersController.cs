#region ItemMasterController
/**************************************************************************************************
 * Type                     : Class File
 * File                     : ItemMasterController.cs
 * Purpose                  : This class is used for Master Details
 * Date                     : 23/02/2021
 * Last Updated             : 23/02/2021
 * Author                   : Nikhil Jojare.
 * Version                  : 1.0.0.0
 * Internal Dependencies    : N/A
 * External Dependencies    : N/A
 * Assembly                 :
 * Environment              : Microsoft Visual Studio.NET 2017
 * Framework                : Microsoft.Net 4.6.1
 *
 ****************************************************************************************************
 * Modification history:
 ****************************************************************************************************
 * Modified By          Modified On             Reason

 *****************************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
   
    public class ItemMastersController : ODataController
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        // GET: odata/ItemMasters
        [EnableQuery]
        public IQueryable<ItemMaster> GetItemMasters()
        {
            return db.ItemMasters;
        }

        // GET: odata/ItemMasters(5)
        [EnableQuery]
        public SingleResult<ItemMaster> GetItemMaster([FromODataUri] int key)
        {
            return SingleResult.Create(db.ItemMasters.Where(itemMaster => itemMaster.ItemId == key));
        }

        // PUT: odata/ItemMasters(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, ItemMaster itemMaster)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemMaster data = await db.ItemMasters.FindAsync(key);
            if (data == null)
            {
                return NotFound();
            }

            data.ItemName = itemMaster.ItemName;
            data.Price = itemMaster.Price;
            data.Description = itemMaster.Description;
            data.ImagePath = itemMaster.ImagePath;
            data.IsActive = itemMaster.IsActive;
            data.Modified = itemMaster.Modified;
            data.ModifiedBy = itemMaster.ModifiedBy;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(itemMaster);
        }

        // POST: odata/ItemMasters
        public async Task<IHttpActionResult> Post(ItemMaster itemMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                using (var db = new ShopBridgeEntities())
                {
                    db.ItemMasters.Add(itemMaster);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

            return Created(itemMaster);
        }

        // PATCH: odata/ItemMasters(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ItemMaster> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemMaster itemMaster = await db.ItemMasters.FindAsync(key);
            if (itemMaster == null)
            {
                return NotFound();
            }

            patch.Patch(itemMaster);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(itemMaster);
        }

        // DELETE: odata/ItemMasters(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ItemMaster itemMaster = await db.ItemMasters.FindAsync(key);
            if (itemMaster == null)
            {
                return NotFound();
            }

            db.ItemMasters.Remove(itemMaster);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemMasterExists(int key)
        {
            return db.ItemMasters.Count(e => e.ItemId == key) > 0;
        }
    }
}
