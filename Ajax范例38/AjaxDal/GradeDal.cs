using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjaxDal
{
    public class GradeDal
    {
        /// <summary>
        /// 查询全部年级信息
        /// </summary>
        /// <returns></returns>
        public List<Grade> SelectAllGrade()
        {
            SchoolEntities entity = new SchoolEntities();
            List<Grade> list = new List<Grade>();
            list = entity.Grade.ToList();
            return list;
        }
    }
}
