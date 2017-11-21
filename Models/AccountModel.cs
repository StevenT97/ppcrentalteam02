using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Models.FrameWork;
using PagedList;
using System.IO;
using System.Web;

namespace Models
{
   public class AccountModel
    {
        // PPC_RentalEntities db = null;
        DemoPPCRentalEntities db = null;
        public AccountModel()
        {
            db = new DemoPPCRentalEntities();
        }
        public long Insert(USER entity)
        {
            db.USERs.Add(entity);
            db.SaveChangesAsync();
            return entity.ID;
        }
        public long InsertProperty(PROPERTy entity)
        {
            if (entity.ImageFile == null)
            {
                entity.Images = "~/Images/ImagesNull.png";
            }
            //else if(entity.ImageFile2==null)
            //{
            //    entity.Avatar = "~/Images/AvatarNull.png";
            //}

            db.PROPERTies.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var property = db.PROPERTies.Find(id);
                db.PROPERTies.Remove(property);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public PROPERTy ViewDetail(int id)
        {
            return db.PROPERTies.Find(id);
        }
        public bool Update(PROPERTy entitys)
        {
            try
            {
                var property = db.PROPERTies.Find(entitys.ID);

                property.PropertyName = entitys.PropertyName;
                if (entitys.ImageFile==null)
                {

                }
                else
                {
                    property.Images = entitys.Images;
                }
               
                property.PropertyType_ID = entitys.PropertyType_ID;
                property.Content = entitys.Content;
                property.Street_ID = entitys.Street_ID;
                property.Ward_ID = entitys.Ward_ID;
                property.District_ID = entitys.District_ID;
                property.Price = entitys.Price;
                property.UnitPrice = entitys.UnitPrice;
                property.Area = entitys.Area;
                property.BedRoom = entitys.BedRoom;
                property.BathRoom = entitys.BathRoom;
                property.PackingPlace = entitys.PackingPlace;
                property.UserID = entitys.UserID;
                //property.Created_at = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                //property.Create_post = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                property.Status_ID = entitys.Status_ID;
                property.Note = entitys.Note;
                //property.Updated_at = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                property.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                property.Sale_ID = entitys.Sale_ID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
             
        }
        public IEnumerable<PROPERTy> ListAllPaging(int page, int pageSize)
        {
            return db.PROPERTies.OrderByDescending(x=> x.Created_at).ToPagedList(page, pageSize);
        }

        public USER GetID(string userName)
        {
            return db.USERs.SingleOrDefault(x => x.Email == userName);

        }
        public int Login(string userName, string passWord)
        {

           // sing or find
            var res = db.USERs.SingleOrDefault(x => x.Email == userName);

            if (res == null )
            {
                return 0;
            }
            else
            {
                if (res.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (res.Password == passWord)
                        return 1;
                    else
                        return -2;

                }
            }
          
        }
    }
}
