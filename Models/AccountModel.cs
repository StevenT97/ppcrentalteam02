using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Models.FrameWork;

namespace Models
{
   public class AccountModel
    {
         PPC_RentalEntities db = null;
        public AccountModel()
        {
            db = new PPC_RentalEntities();
        }
        public long Insert(USER_TABLE entity)
        {
            db.USER_TABLE.Add(entity);
            db.SaveChangesAsync();
            return entity.id;
        }
        public USER_TABLE GetID(string userName)
        {
            return db.USER_TABLE.SingleOrDefault(x => x.Email == userName);

        }
        public int Login(string userName, string passWord)
        {

           // sing or find
            var res = db.USER_TABLE.SingleOrDefault(x => x.Email == userName);

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
