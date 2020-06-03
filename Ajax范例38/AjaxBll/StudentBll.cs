using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjaxDal;

namespace AjaxBll
{
    public class StudentBll
    {
        StudentDal sd = new StudentDal();
        /// <summary>
        /// 分页查询学生表数据
        /// </summary>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数据条数</param>
        /// <returns></returns>
        public List<Student> SelectPageList(int pageSize, int pageIndex, ref int total)
        {
            return sd.SelectPageList(pageSize, pageIndex, ref total);
        }
         /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public int DeleteStudent(string studentNo)
        {
            return sd.DeleteStudent(studentNo);
        }
        /// <summary>
        /// 修改学生的方法
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int UpdateStudent(Student stu)
        {
            return sd.UpdateStudent(stu);
        }
        /// <summary>
        /// 添加学生的方法
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int InsertStudent(Student stu)
        {
            return sd.InsertStudent(stu);
        }
        /// <summary>
        /// 根据学号查询1个学生的信息的方法
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public Student SelectOneStudent(string studentNo)
        {
            return sd.SelectOneStudent(studentNo);
        }
        /// <summary>
        /// 验证手机号码是否重复
       /// </summary>
       /// <param name="phone">手机号码</param>
       /// <param name="studentNo">学号，若添加时验证请传空</param>
       /// <returns>结果大于0验证失败，手机号码重复</returns>
        public int validatePhone(string phone, string studentNo)
        {
            return sd.validatePhone(phone, studentNo);
        }
    }
}
