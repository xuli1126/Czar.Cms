using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjaxDal;

namespace AjaxBll
{
    public class GradeBll
    {
        GradeDal gd = new GradeDal();
         /// <summary>
        /// 查询全部年级信息
        /// </summary>
        /// <returns></returns>
        public List<Grade> SelectAllGrade()
        {
            return gd.SelectAllGrade();
        }
    }
}
